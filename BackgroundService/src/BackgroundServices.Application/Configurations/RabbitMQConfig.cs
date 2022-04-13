using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundServices.Application.Configurations
{
    public class RabbitMQConfig
    {
        public string Queue { get; set; }
        public string HostName { get; set; }
    }
}
