using AIS_Cinema;
using AIS_Cinema.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using QRCoder;
using System.Net.Http.Json;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Update = Telegram.Bot.Types.Update;

namespace Telegram_Bot
{
    internal class Bot
    {
        private const string StartCommand = "/start";
        private const string TicketsCommand = "/tickets";
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

        private async Task HandleTicketsCommandAsync(long chatId, CancellationToken cancellationToken)
        {
            var isAuthenticated = await _httpClient.GetFromJsonAsync<bool>($"api/telegramAuth/isAuthenticated/{chatId}");

            if (isAuthenticated)
            {
                var tickets = await GetTicketsAsync(chatId);

                var inlineKeyboard = new InlineKeyboardMarkup(tickets.Select(ticket =>
                    new[]
                    {
                            InlineKeyboardButton.WithCallbackData(
                                $"{ticket.Session.Movie.Name} - {ticket.Session.DateTime:g}",
                                ticket.Id.ToString())
                    }));

                await _botClient.SendTextMessageAsync(
                    chatId,
                    "Ваши билеты:",
                    replyMarkup: inlineKeyboard,
                    cancellationToken: cancellationToken);
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

        private async Task HandleCallbackQueryAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            long chatId = callbackQuery.Message!.Chat.Id;
            var ticket = await GetTicketByIdAsync(chatId, int.Parse(callbackQuery.Data));

            if (ticket == null)
            {
                await _botClient.SendTextMessageAsync(chatId, "Билет не найден.", cancellationToken: cancellationToken);
                return;
            }

            using (var stream = new MemoryStream(ticket.GetQrCode()))
            {
                var inputOnlineFile = new InputFileStream(stream, "qr.jpg");
                var message = await _botClient.SendPhotoAsync(
                    callbackQuery.Message!.Chat.Id,
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

        private async Task<IEnumerable<Ticket>> GetTicketsAsync(long chatId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<Ticket>>($"api/tickets/{chatId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }

        private async Task<Ticket> GetTicketByIdAsync(long chatId, int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Ticket>($"api/tickets/{chatId}/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return null;
            }
        }
    }
}
