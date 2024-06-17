using AIS_Cinema.Models;
using Newtonsoft.Json;

namespace AIS_Cinema
{
    public static class TicketUtility
    {
        public static byte[] GetQrCode(this Ticket ticket)
        {
            string data = JsonConvert.SerializeObject(new
            {
                ticket.Id,
                ticket.SessionId,
                ticket.Price,
                ticket.OwnerEmail
            });

            return QRCodeGenerator.GenerateQRCode(data);
        }
    }
}
