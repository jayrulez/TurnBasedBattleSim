namespace TurnBasedBattleSim
{
    class TurnBasedBattleSimApp
    {
        static void Main(string[] args)
        {
            using (var game = new Stride.Engine.Game())
            {
                game.Run();
            }
        }
    }
}
