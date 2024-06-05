// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AIS_Cinema.Models;
using AIS_Cinema.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Areas.Identity.Pages.Account.Manage
{
    public class TicketsModel : PageModel
    {
        private readonly AISCinemaDbContext _context;
        private readonly UserManager<Visitor> _userManager;

        public TicketsModel(UserManager<Visitor> userManager, AISCinemaDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IList<UserTicket> Tickets { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        private async Task LoadAsync(Visitor user)
        {
            Tickets = await GetTicketsForUserAsync(user.Id);
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Не удалось загрузить пользователя с ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        private async Task<List<UserTicket>> GetTicketsForUserAsync(string userId)
        {
            var user = await _userManager.GetUserAsync(User);
            var email = await _userManager.GetEmailAsync(user);
            var tickets = await _context.Tickets
                .Include(t => t.Session)
                .ThenInclude(s => s.Movie)
                .Where(t => t.OwnerEmail == email)
                .Select(t => new UserTicket
                {
                    SessionDateTimeStr = DateTimeUtility.FormatDateTime(t.Session.DateTime),
                    MovieName = t.Session.Movie.Name,
                    RowAndSeatStr = TicketFormatter.FormatTicket(t),
                    Price = t.Price,
                    QrCode = t.GetQrCode(),
                })
                .ToListAsync();

            return tickets;
        }
    }
}
