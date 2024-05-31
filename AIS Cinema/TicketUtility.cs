using AIS_Cinema.Models;

namespace AIS_Cinema
{
    public static class TicketUtility
    {
        public static byte[] GetQrCode(this Ticket ticket)
        {
            string data = string.Join(' ', ticket.Id, ticket.RowNumber, ticket.SeatNumber);
            return QRCodeGenerator.GenerateQRCode(data);
        }
    }
}
