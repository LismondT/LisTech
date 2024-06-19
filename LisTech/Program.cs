using LisTech.Core;
using LisTech.Enums;
using LisTech.MVC;

namespace LisTech;

internal class Program
{
    static void Main(string[] args)
    {
        Game.Init();
        Game.Controler?.MainLoop();
    }
}