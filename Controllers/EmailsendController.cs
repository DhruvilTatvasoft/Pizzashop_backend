using Microsoft.AspNetCore.Mvc;
using WebApi.Services;

namespace learn_dotnet
{
    public class EmailsendController : Controller
    {
        private readonly EmailService _emailService;
        public EmailsendController(EmailService emailService){
            _emailService = emailService;
        }
        public async Task<IActionResult> SendEmail()
    }
}

