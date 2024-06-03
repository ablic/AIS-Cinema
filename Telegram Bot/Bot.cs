using API_Models;
using System.Net.Http.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegram_Bot
{
    internal class Bot
    {
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
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
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
            // Only process Message updates: https://core.telegram.org/bots/api#message
            if (update.Message is not { } message)
                return;
            // Only process text messages
            if (message.Text is not { } messageText)
                return;

            var chatId = message.Chat.Id;

            Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

            if (update.Message.Text == "/tickets")
            {
                var isAuthenticated = await _httpClient.GetFromJsonAsync<bool>($"api/telegramAuth/{chatId}");

                if (isAuthenticated)
                {
                    var tickets = await GetTicketsAsync(chatId);

                    foreach (var ticket in tickets)
                    {
                        using (var stream = new MemoryStream(ticket.QrCode))
                        {
                            var inputOnlineFile = new InputFileStream(stream, "qr.jpg");
                            await _botClient.SendPhotoAsync(chatId, inputOnlineFile);
                        }
                    }
                }
                else
                {
                    var authUrl = $"{Constants.MainUrl}/telegram/login/{chatId}";
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

        private async Task<Session[]> GetSessionsAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Session[]>("api/sessions");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<Ticket[]> GetTicketsAsync(long chatId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Ticket[]>($"api/tickets/{chatId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
