using Favit.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Favit.BLL.Services
{
    public class EmailService:IEmailService
    {
        private string _toAddresses;
        private string _fromAddress;
        private string _message;
        private string _subject;
        private string _password;
        private NetworkCredential _credential;
        private MailMessage _mailMessage;
        private SmtpClient _smtpClient;

        public EmailService()
        {
            SetupSmtp();
        }

        private SmtpClient SetupSmtp()
        {

            _smtpClient = new SmtpClient();
            //_smtpClient.Host = Constants.GMAIL_SERVER;
            //_smtpClient.Port = Constants.GMAIL_PORT;
            //_smtpClient.UseDefaultCredentials = false;
            //_smtpClient.EnableSsl = true;
            //_smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //_smtpClient.Credentials = new NetworkCredential(_fromAddress, _password);

            return _smtpClient;
        }

        public bool SendEmail()
        {
            try
            {
                _smtpClient.Send(_mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool SendEmail(string from, string subject, string message, string to)
        {
            try
            {
                var mailMessage = new MailMessage(@from, to, subject, message) { IsBodyHtml = true };
                _smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
        }

        public bool SendEmail(MailMessage message)
        {
            try
            {
                _smtpClient.Send(message);
            }
            catch (Exception e)
            {
                throw new Exception("Email wasn't send", e);
                return false;
            }

            return true;
        }

        public MailMessage GenerateMessageWithThisHTML(string html, bool bodyIsHTML, string toAddress)
        {

            //_fromAddress = Constants.EMAIL_ADDRESS;
            //_subject = "Welcome to CutUp.";
            //_message = html;

            //if (string.IsNullOrEmpty(_toAddresses))
            //{
            //    _toAddresses = toAddress ?? Constants.EMAIL_ADDRESS;
            //}
            //else if (_toAddresses[_toAddresses.Length - 1] == ',')
            //{
            //    string tempAddresses = _toAddresses.Remove(_toAddresses.Length - 1);
            //    _toAddresses = tempAddresses;
            //}

            //_mailMessage = new MailMessage(_fromAddress, _toAddresses, _subject, _message);
            //_mailMessage.IsBodyHtml = bodyIsHtml;

            return _mailMessage;
        }
    }
}
