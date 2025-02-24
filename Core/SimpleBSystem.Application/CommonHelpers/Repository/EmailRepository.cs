using SimpleBSystem.Application.CommonHelpers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.CommonHelpers.Repository
{
    public class EmailRepository : IEmailSender
    {
        public async Task SendBookingConfirmationConsoleEmailAsync(int bookingId)
        {
            Console.WriteLine($"EMAIL SENT TO admin@admin.com FOR CREATED BOOKING WITH ID {bookingId}");
            await Task.CompletedTask;
        }
    }
}
