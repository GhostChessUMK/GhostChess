using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

namespace GhostChess.Console
{
    class Program
    {
        static HubConnection connection;

        static async Task Main(string[] args)
        {
            connection = new HubConnectionBuilder()
              .WithUrl("https://localhost:5000/chess?Password=P@ssw0rd&Board=true")
              .Build();

            connection.On<string, string>("Move", (from, to) =>
            {
                System.Console.WriteLine($"MOVING {from} -> {to}");
            });

            await connection.StartAsync();

            System.Console.WriteLine("Client started... Press any key to close the connection");
            System.Console.ReadLine();
            await connection.DisposeAsync();
            System.Console.WriteLine("Client is shutting down...");
        }
    }
}
