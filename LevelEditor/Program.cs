using System;
using System.Xml;
using Microsoft.Xna.Framework.Content;

namespace LevelEditor
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {


            using (var game = new Game1())
                game.Run();
        }
    }
}
