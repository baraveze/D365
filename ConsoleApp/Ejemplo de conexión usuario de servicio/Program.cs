// Ejemplo realizado por Ezequiel Matias Baravalle, 
//visitá mi blog: https://ezequielbaravalle.blogspot.com/

namespace Ejemplo_de_conexión_usuario_de_servicio
{
    using System;
    using Microsoft.Crm.Sdk.Messages;
    using Microsoft.Xrm.Tooling.Connector;
    class Program
    {
        static void Main(string[] args)
        {
          
            //Se coloca la URL del entorno D365 al cual deseamos conectarnos
            string url_entorno = "https://entornoD365.crm2.dynamics.com";
            // Es el ID de la aplicación que se registró en Azure y se asoció al entorno D365
            string clientId = "dfd45868-71f3-4259-b231-g773308d2ad2"; // Este valor proporcionado es un ejemplo
            // Es el secreto generado para la aplicación en azure
            string clientSecret = "o4rrt~K9ggAAgyx-B5TN3q4cJF3br1kMm.LWWbae"; // Este valor proporcionado es un ejemplo
            //Con los datos anteriores, ya se puede armar la cadena de conexión hacia D365
            string conectionString = $@"Url = {url_entorno};AuthType = ClientSecret;ClientId = {clientId};ClientSecret = {clientSecret};";

            using (var svc = new CrmServiceClient(conectionString))
            {

                if (svc.IsReady)
                {
                    Console.WriteLine("¡Felicitaciones, te has conectado correctamente a tu entorno de D365!");

                    Console.WriteLine("Obteniendo tu user id...");

                    WhoAmIRequest request = new WhoAmIRequest();

                    WhoAmIResponse response = (WhoAmIResponse)svc.Execute(request);

                    Console.WriteLine("Tu UserId es {0}", response.UserId);

                    Console.WriteLine("Para salir, presioná cualquier tecla.");
                }
                else Console.WriteLine("Error, no fue posible conectarnos con tu entorno de D365.");
                Console.ReadLine();
            }
        }
    }
}
