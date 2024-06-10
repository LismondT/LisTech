using LisTech.Enums;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisTech.Tiles.Utils;

public class TilesHelper
{
    private static readonly HashSet<TileIdEnum> _energyTiles =
    [
        TileIdEnum.EnergySource,
        TileIdEnum.Wire,
        TileIdEnum.Light,
    ];


    /// <summary>
    /// Возращает позицию в виде флагов TileAround точки end относительно start.
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    public static TileAround GetRelativePosition(Vector2i start, Vector2i end)
    {
        TileAround flag;

        int xpos = end.X - start.X;
        int ypos = end.Y - start.Y;

        bool onOneLineX = start.X == end.X;
        bool onOneLineY = start.Y == end.Y;
        bool startRight = start.X > end.X;
        bool startUp = start.Y < end.Y;

        bool isCenter = start == end;
        bool isUnreachable = ((xpos < -1) || (xpos > 1))
                          || ((ypos < -1) || (ypos > 1));

        TileAround xFlag = onOneLineX ? 0 : (startRight ? TileAround.Left : TileAround.Right);
        TileAround yFlag = onOneLineY ? 0 : (startUp ? TileAround.Down : TileAround.Up);
        TileAround centerFlag = isCenter ? TileAround.Center : 0;
        TileAround unreachableFlag = isUnreachable ? TileAround.Unreachable : 0;

        flag = xFlag | yFlag | centerFlag | unreachableFlag;

        return flag;
    }

    public static bool IsEnergyTile(TileIdEnum tileType) => _energyTiles.Contains(tileType);
}
