using AIS_Cinema.Models;

namespace AIS_Cinema
{
    public static class TicketFormatter
    {
        public static string FormatTicket(Ticket ticket)
        {
            return $"{ticket.RowNumber}-й ряд, {ticket.SeatNumber}-е место";
        }
    }
}
