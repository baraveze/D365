using System;

namespace Aplicación_de_consola_D365
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Client;
    using Microsoft.Crm.Sdk.Messages;
    using System.Net;
    using System.ServiceModel.Description;
    using Microsoft.Xrm.Tooling.Connector;
    using static System.Net.WebRequestMethods;
    using System.Security.Policy;
    class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("Hello");

            string url = "https://organization.crm.dynamics.com";
            string clientId = "dfd45868-59d1-4259-b231-a551108d2ad2";
            string appId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
            string connectionString = $@"";

            using (var svc = new CrmServiceClient(connectionString))
            {

                if (svc.IsReady)
                {
                    WhoAmIRequest request = new WhoAmIRequest();

                    WhoAmIResponse response = (WhoAmIResponse)svc.Execute(request);

                    Console.WriteLine("Your UserId is {0}", response.UserId);

                    Console.WriteLine("Press any key to exit.");
                }
                else Console.WriteLine("There is a problem connecting to D365.");
                Console.ReadLine();
            }
        }
    }
}
