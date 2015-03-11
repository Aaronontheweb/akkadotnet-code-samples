using System;
using Akka.Actor;
using Topshelf;


namespace ETLActors
{
    class Program
    {
        static int Main(string[] args)
        {
            return (int) HostFactory.Run(x =>
            {
                x.SetServiceName("ETL");
                x.SetDisplayName("Akka.NET ETL - Central");
                x.SetDescription("Akka.NET Streaming ETL Cluster Demo");

                x.UseAssemblyInfoForServiceInfo();
                x.RunAsLocalSystem();
                x.StartAutomatically();
                x.Service<CentralETLService>();
                x.EnableServiceRecovery(r => r.RestartService(1));
            });
        }
    }
}
