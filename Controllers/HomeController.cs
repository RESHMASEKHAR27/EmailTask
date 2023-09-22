using EmailTask.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Org.BouncyCastle.Crypto.Macs;
using System.Diagnostics;


namespace EmailTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(StudentModel std)
        {
            if(!ModelState.IsValid)
            {
                return View(std);
            }
            //create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("scopeindia27@gmail.com"));
            email.To.Add(MailboxAddress.Parse(std.To));

            email.Subject = $"Subject:{std.Subject}";

            //html Email

            email.Body = new TextPart(TextFormat.Plain)
            {
                Text =  $"To:{std.To}" + "\n" + $"Subject:{std.Subject}" + "\n"
                + $"{std.Body}"
            };

            var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com",587,SecureSocketOptions.StartTls);
            smtp.Authenticate("scopeindia27@gmail.com", "sjzd vjot kjvt mfnv");
            smtp.Send(email);
            smtp.Disconnect(true);

           ViewBag.Message = "A mail has been successfully sent";
            return RedirectToAction("Privacy");

        }

        public IActionResult Privacy()
        {
           // ViewBag.Content = "A mail has been successfully sent";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}