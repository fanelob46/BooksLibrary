using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmaillConfigure.Models
{
    public class EmailConfiguration
    {
        public string From { get; set; } = null!;
        public string SmtServer { get; set; } = null!;
        public int Port { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
