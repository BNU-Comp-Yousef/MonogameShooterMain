using System;

namespace MonogameShooterMain
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new ShooterGame())
                game.Run();
        }
    }
}
