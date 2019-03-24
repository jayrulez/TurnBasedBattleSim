namespace TurnBasedBattleSim
{
    class TurnBasedBattleSimApp
    {
        static void Main(string[] args)
        {
            using (var game = new Xenko.Engine.Game())
            {
                game.Run();
            }
        }
    }
}
