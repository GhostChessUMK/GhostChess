namespace GhostChess.Web.State
{
    public static class Game
    {
        public static string Password { get; set; }
        public static int PlayerAmount { get; set; } = 0;
        public static User User1 { get; set; } = new User();
        public static User User2 { get; set; } = new User();
    }

    public class User
    {
        public string ConnectionId { get; set; } = "";
    }
}
