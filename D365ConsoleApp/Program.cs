using System;

namespace Aplicación_de_consola_D365
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("Hello");

            string url = "https://organization.crm.dynamics.com";
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
