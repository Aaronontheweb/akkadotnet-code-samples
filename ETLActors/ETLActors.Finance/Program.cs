using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace ETLActors.Finance
{
    class Program
    {
        static int Main(string[] args)
        {
            return (int)HostFactory.Run(x =>
            {
                x.SetServiceName("ETL Marketing");
                x.SetDisplayName("Akka.NET ETL - Marketing");
                x.SetDescription("Akka.NET Streaming ETL Cluster Demo");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.Service<FinanceService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
