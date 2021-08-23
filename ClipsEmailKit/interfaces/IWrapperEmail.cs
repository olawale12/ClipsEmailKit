using System;
using System.Collections.Generic;
using System.Text;

namespace ClipsEmailKit.interfaces
{
    public interface IWrapperEmail
    {
        IEmailSender emailSend { get; }
    }
}
