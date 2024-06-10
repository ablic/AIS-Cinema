using AIS_Cinema.Models;
using MimeKit.Utils;
using MimeKit;
using MailKit.Net.Smtp;

namespace AIS_Cinema.Services
{
    public class TicketEmailSender
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
            message.Subject = $"Ваши билеты на фильм {session.Movie.Name}";

            var builder = new BodyBuilder
            {
                HtmlBody = GenerateEmailBody(session, tickets)
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

        private string GenerateEmailBody(Session session, List<Ticket> tickets)
        {
            var body = $"<h1>Ваши билеты на фильм \"{session.Movie.Name}\"</h1>" +
                       $"<p>Дата и время: {session.DateTime:dd MMMM yyyy HH:mm}</p>" +
                       "<ul>";

            foreach (var ticket in tickets)
            {
                body += $"<li>Ряд {ticket.RowNumber}, Место {ticket.SeatNumber}\n" +
                        $"<img src=\"cid:qrCode_{ticket.RowNumber}_{ticket.SeatNumber}.png\" alt=\"QR Code\" /></li>";
            }

            body += "</ul>";

            return body;
        }
    }
}
