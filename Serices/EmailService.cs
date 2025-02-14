using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace WebApi.Services
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string html);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(string from, string to, string subject, string html)
        {
            
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            
            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("Smtp").GetSection("Host").Value,Int32.Parse(_configuration.GetSection("Smtp").GetSection("Port").Value), SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("Smtp").GetSection("Username").Value, _configuration.GetSection("Smtp").GetSection("Password").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}