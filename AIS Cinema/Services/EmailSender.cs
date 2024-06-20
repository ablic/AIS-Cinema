using AIS_Cinema.Models;
using MimeKit.Utils;
using MimeKit;
using MailKit.Net.Smtp;

namespace AIS_Cinema.Services
{
    public class EmailSender
    {
        private readonly string _smtpServer = "smtp.gmail.com";
        private readonly int _smtpPort = 465;
        private readonly string _smtpUser = "ablichenkov@gmail.com";
        private readonly string _smtpPass = "ebjeoeyfluuhzldw";

        public async Task SendTicketsAsync(string recipientEmail, Session session, List<Ticket> tickets)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cinema", _smtpUser));
            message.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
            message.Subject = $"Ваши билеты на фильм \"{session.Movie.Name}\"";

            var builder = new BodyBuilder
            {
                HtmlBody = GenerateTicketEmailBody(session, tickets)
            };

            foreach (var ticket in tickets)
            {
                var qrCodeImage = builder.LinkedResources.Add(
                    $"qrCode_{ticket.RowNumber}_{ticket.SeatNumber}.png",
                    ticket.GetQrCode());

                qrCodeImage.ContentId = MimeUtils.GenerateMessageId();
            }

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, true);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendSessionCancelledNotificationAsync(string recipientEmail, Session session)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cinema", _smtpUser));
            message.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
            message.Subject = $"Сеанс фильма \"{session.Movie.Name}\" был отменен";

            var builder = new BodyBuilder
            {
                HtmlBody = GenerateCancellationEmailBody(session)
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, true);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task SendRefundRequestNotificationAsync(string recipientEmail, Ticket ticket)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Cinema", _smtpUser));
            message.To.Add(new MailboxAddress(recipientEmail, recipientEmail));
            message.Subject = $"Запрос на возврат билета на фильм \"{ticket.Session.Movie.Name}\"";

            var builder = new BodyBuilder
            {
                HtmlBody = GenerateRefundRequestEmailBody(ticket)
            };

            message.Body = builder.ToMessageBody();

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, true);
                await client.AuthenticateAsync(_smtpUser, _smtpPass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private string GenerateTicketEmailBody(Session session, List<Ticket> tickets)
        {
            var body = $"<h1>Ваши билеты на фильм \"{session.Movie.Name}\"</h1>" +
                       $"<p>Дата и время: {session.DateTime:dd MMMM yyyy HH:mm}</p>" +
                       "<ul>";

            foreach (var ticket in tickets)
            {
                body += $"<li>Ряд {ticket.RowNumber}, Место {ticket.SeatNumber}</li>";
            }

            body += "</ul>";

            return body;
        }

        private string GenerateCancellationEmailBody(Session session)
        {
            return $"<h1>Сеанс фильма \"{session.Movie.Name}\" был отменен</h1>" +
                   $"<p>Дата и время сеанса: {session.DateTime:dd MMMM yyyy HH:mm}</p>" +
                   "<p>Пожалуйста, свяжитесь с нами для получения информации о возврате средств.</p>";
        }

        private string GenerateRefundRequestEmailBody(Ticket ticket)
        {
            return $"<h1>Запрос на возврат билета</h1>" +
                   $"<p>Вы запросили возврат билета на фильм \"{ticket.Session.Movie.Name}\"</p>" +
                   $"<p>Дата и время сеанса: {ticket.Session.DateTime:dd MMMM yyyy HH:mm}</p>" +
                   $"<p>Ряд: {ticket.RowNumber}, Место: {ticket.SeatNumber}</p>" +
                   "<p>Мы свяжемся с вами для дальнейших инструкций.</p>";
        }
    }
}
