using Favesrus.Core;
using Favesrus.Core.Logging;
using Favesrus.Core.TypeMapping;
using System;
using System.Net.Mail;

namespace Favesrus.Services
{
    public interface IEmailService
    {
        bool SendEmail();
        bool SendEmail(string from, string subject, string message, string to);
        bool SendEmail(MailMessage message);
        MailMessage GenerateMessageWithThisHTML(string html, bool bodyIsHTML, string toAddress);
    }

    public class EmailService:BaseService,IEmailService
    {
        private string _toAddresses;
        private string _fromAddress;
        private string _message;
        private string _subject;
        private MailMessage _mailMessage;
        private SmtpClient _smtpClient;

        public EmailService(ILogManager logManager, IAutoMapper mapper)
            :base(logManager, mapper)
        {
            _fromAddress = FavesrusConstants.EMAIL_ADDRESS;
            SetupSmtp();
        }

        private SmtpClient SetupSmtp()
        {

            _smtpClient = new SmtpClient();

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

        public bool SendEmail(string @from, string subject, string message, string to)
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

        public MailMessage GenerateMessageWithThisHTML(string html, bool bodyIsHtml = false, string toAddress = null)
        {
            _fromAddress = FavesrusConstants.EMAIL_ADDRESS;
            _subject = "Welcome to Favesrus.";
            _message = html;

            if (string.IsNullOrEmpty(_toAddresses))
            {
                _toAddresses = toAddress ?? FavesrusConstants.EMAIL_ADDRESS;
            }
            else if (_toAddresses[_toAddresses.Length - 1] == ',')
            {
                string tempAddresses = _toAddresses.Remove(_toAddresses.Length - 1);
                _toAddresses = tempAddresses;
            }

            _mailMessage = new MailMessage(_fromAddress, _toAddresses, _subject, _message);
            _mailMessage.IsBodyHtml = bodyIsHtml;

            return _mailMessage;
        }

        public void AddRecipent(string recipentEmailAddress)
        {
            _toAddresses = _toAddresses + recipentEmailAddress + ",";
        }
    }
}
