using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BakuSpirtis.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string emailTo, string html, string content, string userName);
    }
}
