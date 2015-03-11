using System;
using Akka.Actor;


namespace ETLActors
{
    class Program
    {
        public static ActorSystem EtlActorSystem;

        static void Main(string[] args)
        {
            EtlActorSystem = ActorSystem.Create("EtlActorSystem");
            
            Console.WriteLine("hello!");

            EtlActorSystem.AwaitTermination();

        }
    }
}
