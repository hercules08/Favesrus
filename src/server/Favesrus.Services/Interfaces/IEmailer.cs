using System.Net.Mail;

namespace Favesrus.Services.Interfaces
{
    public interface IEmailer
    {
        bool SendEmail();
        bool SendEmail(string from, string subject, string message, string to);
        bool SendEmail(MailMessage message);
        MailMessage GenerateMessageWithThisHTML(string html, bool bodyIsHTML, string toAddress);
    }
}
