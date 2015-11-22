using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Traveler.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMail(string to, string from, string subject, string body) {
            Debug.WriteLine($"Sending Mail: To: {to}, With The Subject: {subject}, From: {from}");
            return true;
        }
    }
}
