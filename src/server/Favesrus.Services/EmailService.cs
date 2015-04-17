using CutUp.Services.Interfaces;
using Favesrus.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Services
{
    public class EmailService:IEmailer
    {
        private string _toAddresses;
        private string _fromAddress;
        private string _message;
        private string _subject;
        private MailMessage _mailMessage;
        private SmtpClient _smtpClient;

        public EmailService()
        {
            _fromAddress = Constants.EMAIL_ADDRESS;
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
            _fromAddress = Constants.EMAIL_ADDRESS;
            _subject = "Welcome to CutUp.";
            _message = html;

            if (string.IsNullOrEmpty(_toAddresses))
            {
                _toAddresses = toAddress ?? Constants.EMAIL_ADDRESS;
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
