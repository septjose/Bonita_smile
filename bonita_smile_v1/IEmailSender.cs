using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonita_smile_v1
{
    interface IEmailSender
    {
        Task SendEmailAsync(string emailTo, string subject, string message);
    }
}
