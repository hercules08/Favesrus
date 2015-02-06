using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL.Interfaces
{
    public interface IEmailService
    {
        bool SendEmail();
        bool SendEmail(string from, string subject, string message, string to);
        bool SendEmail(MailMessage message);
        MailMessage GenerateMessageWithThisHTML(string html, bool bodyIsHTML, string toAddress);
    }
}
