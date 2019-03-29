using Microsoft.AspNetCore.SignalR.Client;
using RPiSingalR_ReedDemo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RPiSingalRReedDemo
{
    public class Demo
    {
        private static HubConnection connection;
        private static bool[,] circuitBoard;

        public async Task MainAsync()
        { 
            Setup setup = new Setup();
            setup.Init();

            connection = new HubConnectionBuilder()
                .WithUrl("https://umkboardtest.azurewebsites.net/board")
                .Build();

            await connection.StartAsync();
            circuitBoard = await connection.InvokeAsync<bool[,]>("Init");

            while(true)
            {
                circuitBoard = setup.GetOutput();
                Print();
                await connection.InvokeAsync<bool[,]>("Update", circuitBoard);
                await Task.Delay(1000);
            }

            //await Task.Delay(-1);
        }

        private static void Print()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Console.Write($"{circuitBoard[i, j]} ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
