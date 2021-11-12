using System;

namespace ArmyGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ArmyGame())
                game.Run();
        }
    }
}
