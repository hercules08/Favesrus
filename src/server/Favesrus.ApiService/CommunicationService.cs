using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.ApiService
{
    public interface ICommunicationService
    {
        string SendNewsletterSignUpConfirm(IdentityMessage message);
        Task<string> SendNewsletterSignUpConfirmAsync(IdentityMessage message);
        Task<string> WriteUsAsync(IdentityMessage message);
    }

    public class CommunicationService:ICommunicationService
    {


        public string SendNewsletterSignUpConfirm(IdentityMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<string> SendNewsletterSignUpConfirmAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }

        public Task<string> WriteUsAsync(IdentityMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
