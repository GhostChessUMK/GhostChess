using GhostChess.Web.State;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace GhostChess.Web.Hubs
{
    public class ChessHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var t = Context.GetHttpContext().Request.Query["Password"];
            if (t != Game.Password)
            {
                Context.Abort();
                return;
            }

            if (Context.GetHttpContext().Request.Query["Board"] == "true")
            {
                return;
            }

            Game.PlayerAmount++;

            if (Game.User1.ConnectionId == "")
            {
                Game.User1.ConnectionId = Context.ConnectionId;
            }
            else if (Game.User2.ConnectionId == "")
            {
                Game.User2.ConnectionId = Context.ConnectionId;
            }

            if (Game.PlayerAmount == 2)
            {
                await Clients.Client(Game.User1.ConnectionId).SendAsync("StartAndPlay");
                await Clients.Client(Game.User2.ConnectionId).SendAsync("StartAndWait");
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            if (Context.ConnectionId == Game.User1.ConnectionId || Context.ConnectionId == Game.User2.ConnectionId)
            {
                Game.PlayerAmount--;

                if (Game.User1.ConnectionId == Context.ConnectionId)
                {
                    Game.User1.ConnectionId = "";
                }

                if (Game.User2.ConnectionId == Context.ConnectionId)
                {
                    Game.User2.ConnectionId = "";
                }

                await Clients.All.SendAsync("End");
            }

            await Clients.Caller.SendAsync("End");
        }

        public async Task Move(string from, string to)
        {
            await Clients.AllExcept(Context.ConnectionId).SendAsync("Move", from, to);
        }

        public async Task Checkmate(bool callerWins, string from, string to)
        {
            if (callerWins)
            {
                await Clients.Caller.SendAsync("YouWin");
                await Clients.AllExcept(Context.ConnectionId).SendAsync("YouLoose");
            }
            else
            {
                await Clients.Caller.SendAsync("YouLoose");
                await Clients.AllExcept(Context.ConnectionId).SendAsync("YouWin");
            }
        }
    }
}
