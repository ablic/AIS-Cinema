using AIS_Cinema;
using AIS_Cinema.Models;
using System.Net.Http.Json;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Update = Telegram.Bot.Types.Update;

namespace Telegram_Bot
{
    internal class Bot
    {
        private const string StartCommand = "/start";
        private const string SessionsCommand = "/сеансы";
        private const string TicketsCommand = "/мои билеты";
        private const int TicketMessageLifetime = 1;

        private TelegramBotClient _botClient;
        private HttpClient _httpClient;

        public Bot(string botToken, string siteUrl)
        {
            _botClient = new TelegramBotClient(botToken);
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(siteUrl),
            };
        }

        public async Task StartReceivingAsync()
        {
            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            _botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await _botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();
        }

        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == UpdateType.Message && update.Message!.Type == MessageType.Text)
            {
                await HandleMessageAsync(update.Message, cancellationToken);
            }
            else if (update.Type == UpdateType.CallbackQuery)
            {
                await HandleCallbackQueryAsync(update.CallbackQuery!, cancellationToken);
            }
        }

        private async Task HandleMessageAsync(Message message, CancellationToken cancellationToken)
        {
            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{message.Text}' message in chat {chatId}.");

            switch (message.Text)
            {
                case StartCommand: await HandleStartCommandAsync(chatId, cancellationToken); break;
                case SessionsCommand: await HandleSessionsCommandAsync(chatId, cancellationToken); break;
                case TicketsCommand: await HandleTicketsCommandAsync(chatId, cancellationToken); break;
                default: 
                    await _botClient.SendTextMessageAsync(
                        chatId, 
                        "Неизвестная команда. Пожалуйста, используйте кнопки ниже для взаимодействия с ботом."); 
                    break;
            }
        }

        public async Task HandleStartCommandAsync(long chatId, CancellationToken cancellationToken)
        {
            var replyKeyboardMarkup = new ReplyKeyboardMarkup(new[]
            {
                new KeyboardButton[]
                {
                    TicketsCommand,
                    SessionsCommand,
                }
            })
            {
                ResizeKeyboard = true,
            };

            await _botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "Добро пожаловать! Используйте кнопки ниже, чтобы получить нужную вам информацию.",
                replyMarkup: replyKeyboardMarkup,
                cancellationToken: cancellationToken
            );
        }

        public async Task HandleSessionsCommandAsync(long chatId, CancellationToken cancellationToken)
        {
            InlineKeyboardButton[][] sessionDateButtons = new InlineKeyboardButton[7][];

            for (int i = 0; i < sessionDateButtons.Length; i++)
            {
                sessionDateButtons[i] = new InlineKeyboardButton[1];

                DateTime date = DateTime.Today.AddDays(i);
                sessionDateButtons[i][0] = InlineKeyboardButton.WithCallbackData(
                    date.FormatDateWithTodayTomorrow(),
                    SessionsCommand + date.ToString());
            }

            var inlineKeyboard = new InlineKeyboardMarkup(sessionDateButtons);

            await _botClient.SendTextMessageAsync(
                chatId,
                "Выберите дату:",
                replyMarkup: inlineKeyboard,
                cancellationToken: cancellationToken);
        }

        private async Task HandleTicketsCommandAsync(long chatId, CancellationToken cancellationToken)
        {
            var isAuthenticated = await _httpClient.GetFromJsonAsync<bool>($"api/telegramauth/isAuthenticated/{chatId}");

            if (isAuthenticated)
            {
                var tickets = await GetTicketsAsync(chatId);

                if (tickets.Count() > 0)
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(tickets.Select(ticket =>
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(
                            $"{ticket.Session.Movie.Name} - {ticket.Session.DateTime:g}",
                            TicketsCommand + ticket.Id.ToString())
                    }));

                    await _botClient.SendTextMessageAsync(
                        chatId,
                        "Ваши билеты:",
                        replyMarkup: inlineKeyboard,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    await _botClient.SendTextMessageAsync(
                        chatId,
                        "У вас нет приобретенных билетов.",
                        cancellationToken: cancellationToken);
                }
                
            }
            else
            {
                var authUrl = $"{Constants.MainUrl}/telegramBinding/login/{chatId}";
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    InlineKeyboardButton.WithUrl("Авторизоваться", authUrl)
                });

                await _botClient.SendTextMessageAsync(
                    chatId,
                    "Вы не авторизованы в системе. Пожалуйста, перейдите на наш сайт и авторизуйтесь, используя кнопку ниже.",
                    replyMarkup: inlineKeyboard);
            }
        }

        private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            long chatId = callbackQuery.Message!.Chat.Id;

            if (callbackQuery.Data.StartsWith(SessionsCommand))
            {
                DateTime date = DateTime.Parse( callbackQuery.Data.Replace(SessionsCommand, string.Empty) );
                await HandleSessionsCommandCallbackQueryAsync(chatId, date, cancellationToken);
            }
            else if (callbackQuery.Data.StartsWith(TicketsCommand))
            {
                int ticketId = int.Parse( callbackQuery.Data.Replace(TicketsCommand, string.Empty) );
                await HandleTicketsCommandCallbackQueryAsync(chatId, ticketId, cancellationToken);
            }
        }

        private async Task HandleSessionsCommandCallbackQueryAsync(long chatId, DateTime date, CancellationToken cancellationToken)
        {
            var sessions = await GetSessionsAsync(date);

            if (sessions.Count() > 0)
            {
                foreach (var session in sessions)
                {
                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        InlineKeyboardButton.WithUrl("Приобрести билеты", $"{Constants.MainUrl}/orders/selectSeats/{session.Id}"),
                    });

                    await _botClient.SendTextMessageAsync(
                        chatId: chatId, 
                        text: FormatSessionInfo(session),
                        parseMode: ParseMode.MarkdownV2,
                        replyMarkup: inlineKeyboard);
                }
            }
            else
            {
                await _botClient.SendTextMessageAsync(chatId, "Нет сеансов в выбранную дату.");
            }
                
        }

        private async Task HandleTicketsCommandCallbackQueryAsync(long chatId, int ticketId, CancellationToken cancellationToken)
        {
            var ticket = await GetTicketByIdAsync(chatId, ticketId);

            if (ticket == null)
            {
                await _botClient.SendTextMessageAsync(chatId, "Билет не найден.", cancellationToken: cancellationToken);
                return;
            }

            using (var stream = new MemoryStream(ticket.GetQrCode()))
            {
                var inputOnlineFile = new InputFileStream(stream, "qr.jpg");
                var message = await _botClient.SendPhotoAsync(
                    chatId,
                    inputOnlineFile,
                    caption: $"Билет на фильм {ticket.Session.Movie.Name}. Это сообщение будет удалено через {TicketMessageLifetime} мин.",
                    cancellationToken: cancellationToken);

                var timer = new Timer(async state =>
                {
                    try
                    {
                        await _botClient.DeleteMessageAsync(chatId, message.MessageId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при удалении сообщения: {ex.Message}");
                    }
                }, null, TicketMessageLifetime * 1000 * 60, Timeout.Infinite);
            }
        }

        private Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }

        private async Task<IEnumerable<Session>> GetSessionsAsync(DateTime date)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Session>>($"api/sessions/{date.ToString("yyyy-MM-dd")}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<IEnumerable<Ticket>> GetTicketsAsync(long chatId)
        {
            try
            {
                string userId = await GetUserIdByChatIdAsync(chatId);
                return await _httpClient.GetFromJsonAsync<IEnumerable<Ticket>>($"api/tickets/{userId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<Ticket> GetTicketByIdAsync(long chatId, int ticketId)
        {
            try
            {
                string userId = await GetUserIdByChatIdAsync(chatId);
                return await _httpClient.GetFromJsonAsync<Ticket>($"api/tickets/{userId}/{ticketId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<string> GetUserIdByChatIdAsync(long chatId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/telegramAuth/getUserId/{chatId}");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private string FormatSessionInfo(Session session)
        {
            return $"*Дата и время:* {EscapeMarkdownV2(session.DateTime.ToString("dd.MM.yyyy HH:mm"))}\n" +
                      $"*Название фильма:* {EscapeMarkdownV2(session.Movie.Name)}\n" +
                      $"*Стоимость билета:* {session.MinPrice:F2} руб\n" +
                      //$"*Количество доступных мест:* {}\n" +
                      $"*Номер зала:* {session.HallId}";
        }

        private string EscapeMarkdownV2(string text)
        {
            var escapeChars = new[] { '_', '*', '[', ']', '(', ')', '~', '`', '>', '#', '+', '-', '=', '|', '{', '}', '.', '!' };
            var escapedText = new StringBuilder(text.Length);

            foreach (var c in text)
            {
                if (escapeChars.Contains(c))
                {
                    escapedText.Append('\\');
                }
                escapedText.Append(c);
            }
            return escapedText.ToString();
        }
    }
}
