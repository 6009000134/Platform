using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyPlatform.Common
{
    public class NetWorkUtils
    {
        public void GetLocalIpAddres()
        {
            string localHostName = Dns.GetHostName();
            IPAddress[] address=Dns.GetHostAddresses(localHostName);
            foreach (IPAddress ip in address)
            {

            }
        }
    }
}
