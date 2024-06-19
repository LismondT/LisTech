
using SFML.System;

namespace LisTech.Core
{
    public static class GlobalVariables
    {
        public static Vector2u ScreenSize { get; set; } = new Vector2u(900, 900);

        public static uint TileSize { get; set; } = 32;
    }
}
