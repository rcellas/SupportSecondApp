using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace SupportSecondApp.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Solo queremos evitar el error de compilación
            return Task.CompletedTask;
        }
    }
}