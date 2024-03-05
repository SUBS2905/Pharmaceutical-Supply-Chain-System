using PharmaceuticalSupplyChainSystem.Services.Interfaces;
using System.Net;
using System.Net.Mail;

namespace PharmaceuticalSupplyChainSystem.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendMail(string to, string subject, string message)
        {
            var from = _config["Brevo:Email"];
            var password = _config["Brevo:Password"];

            var client = new SmtpClient("smtp-relay.brevo.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(from, password)
            };

            await client.SendMailAsync(new MailMessage(from, to, subject, message));

        }
    }
}
