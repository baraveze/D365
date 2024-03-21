using System;

namespace Aplicación_de_consola_D365
{
    class Program
    {
        static void Main(string[] args)
        {

            System.Console.WriteLine("Hello");

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
