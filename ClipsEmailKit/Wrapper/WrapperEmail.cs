using ClipsEmailKit.interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClipsEmailKit.Wrapper
{
    public class WrapperEmail : IWrapperEmail
    {
        private IEmailSender _emailSend;

        private readonly EmailConfiguration _emailConfig;

        public WrapperEmail(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }


        public IEmailSender emailSend
        {
            get
            {
                if (_emailSend == null)
                {
                    _emailSend = new EmailManager(_emailConfig);
                }
                return _emailSend;
            }

        }
    }
}
