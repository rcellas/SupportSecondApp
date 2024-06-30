using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace SupportSecondApp.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Implementa tu lógica de envío de correos aquí
            return Task.CompletedTask;
        }
    }
}