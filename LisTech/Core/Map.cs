using System.Drawing;
using System.Xml.Serialization;
using LisTech.Enums;
using LisTech.Tiles;
using LisTech.Tiles.Utils;
using SFML.System;

namespace LisTech.Core;

public class Map
{
    private static Map? _map;
    public static Map Instance => _map ??= new Map();

    private readonly Vector2u _size = new(20, 20); //horizontal, vertical

    public Vector2u Size => _size;

    public TileBase[,] TileMap { get; set; }


    public Map()
    {
        TileMap ??= new TileBase[Size.Y, Size.X];
        ClearMap();
    }

    public TileBase GetTile(int x, int y)
    {
        if (TileMap == null) throw new Exception();
        if (x >= Size.X || x < 0) throw new Exception();
        if (y >= Size.Y || y < 0) throw new Exception();

        return TileMap[y, x];
    }

    public TileBase GetTile(Vector2i pos)
    {
        if (TileMap == null) throw new Exception();
        if (pos.X >= Size.X || pos.X < 0) throw new Exception();
        if (pos.Y >= Size.Y || pos.Y < 0) throw new Exception();

        return TileMap[pos.Y, pos.X];
    }

    public void SetTile(int x, int y, TileIdEnum tileType)
    {
        if (TileMap == null) throw new Exception();
        if (x >= Size.X || x < 0) throw new Exception();
        if (y >= Size.Y || y < 0) throw new Exception();

        TileBase tile = TileFactory.Create(tileType);
        TileMap[y, x] = tile;
        tile.OnCreate(new Vector2i(x, y));
    }

    public void ClearMap()
    {
        for (int i = 0; i < Size.Y; i++)
        {
            for (int j = 0; j < Size.Y; j++)
            {
                TileMap[i, j] = TileFactory.Create(TileIdEnum.None);
            }
        }
    }

    public bool OnMap(int x, int y)
    {
        if (x >= Size.X || x < 0) return false;
        if (y >= Size.Y || y < 0) return false;

        return true;
    }

    public bool OnMap(Vector2i pos)
    {
        if (pos.X >= Size.X || pos.X < 0) return false;
        if (pos.Y >= Size.Y || pos.Y < 0) return false;

        return true;
    }
}
