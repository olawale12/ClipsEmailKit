using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClipsEmailKit.interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsyn(Message message, string body, bool sslStatus = true);
        void SendEmail(Message message, string body, bool sslStatus = true);
    }
}
