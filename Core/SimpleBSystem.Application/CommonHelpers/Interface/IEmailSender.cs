using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBSystem.Application.CommonHelpers.Interface
{
    public interface IEmailSender
    {
        Task SendBookingConfirmationConsoleEmailAsync(int bookingId);
    }
}
