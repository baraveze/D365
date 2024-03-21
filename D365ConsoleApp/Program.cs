namespace Aplicación_de_consola_D365
{
    using System;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Tooling.Connector;
    class Program
    {
        static void Main(string[] args)
        {     
            string url = "https://organization.crm.dynamics.com";
            string clientId = "dfd45868-59d1-4259-b231-a551108d2ad2";
            string clientSecret = "o4r8Q~K9llAAgyx-B4TN3q4bJF3ar1kMm.LWWbae";
            string userName = "user@org.onmicrosoft.com";
            string password = "Password";
            string appId = "51f81489-12ee-4a9e-aaae-a2591f45987d";
            string redirectUri = "app://58145B91-0C36-4500-8554-080854F2AC97";
            //"Password01";
            //  RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
            // LoginPrompt=Never;
            //RequireNewInstance = True
            //AppId = dfd45868 - 59d1 - 4259 - b231 - a551108d2ad2;


            //string connectionString = $@"Url = {url};AuthType = ClientSecret;ClientId = {clientId};ClientSecret = {clientSecret};";

            string connectionString = $@"AuthType=OAuth;Username{userName};Password = {password}; Url = {url};AppId={appId};RedirectUri = {redirectUri};LoginPrompt=Auto";

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
