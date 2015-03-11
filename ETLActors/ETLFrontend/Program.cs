using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ETLFrontend
{
    class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.SetServiceName("ETLFrontend");
                x.SetDisplayName("Akka.NET ETL - Frontend");
                x.SetDescription("Akka.NET Streaming ETL Cluster Demo");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.Service<ETLFrontendService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
