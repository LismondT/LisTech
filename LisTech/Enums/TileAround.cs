using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisTech.Enums;

[Flags]
public enum TileAround
{
    Up = 1 << 0,
    Right = 1 << 1,
    Down = 1 << 2,
    Left = 1 << 3,

    RightUp = 1 << 4,
    RightDown = 1 << 5,
    LeftUp = 1 << 6,
    LeftDown = 1 << 7,

    Center = 1 << 8,
    Unreachable = 1 << 9,
}
