using LisTech.Core;
using LisTech.Enums;
using SFML.System;
using System.Collections.Generic;

namespace LisTech.Tiles.Utils;

public class TilesFinder
{
    enum Visible
    {
        Around,
        Cross
    }

    public static List<Vector2i> FindTilesAround(Vector2i pos, TileIdEnum type)
    {
        List<Vector2i> points = [];
        Map map = Map.Instance;
        Vector2u size = map.Size;

        for (int i = pos.Y - 1; i <= pos.Y + 1; i++)
        {
            for (int j = pos.X - 1; j <= pos.X + 1; j++)
            {
                if (i == pos.Y && j == pos.X) continue;
                if (i < 0 || i >= size.Y) continue;
                if (j < 0 || j >= size.X) continue;

                TileIdEnum ctype = map.GetTile(j, i).GetTileType();

                if (ctype == type)
                {
                    points.Add(new Vector2i(j, i));
                }
            }
        }

        return points;
    }

    public static List<Vector2i> FindTilesCross(Vector2i pos, TileIdEnum type)
    {
        Vector2i[] tilesPos = [
            pos - new Vector2i(1, 0),
            pos + new Vector2i(1, 0),
            pos - new Vector2i(0, 1),
            pos + new Vector2i(0, 1),
            ];

        List<Vector2i> result = new();

        foreach (Vector2i tilePos in tilesPos)
        {
            if (!Map.Instance.OnMap(tilePos)) continue;

            TileIdEnum ctype = Map.Instance.GetTile(tilePos).GetTileType();
            if (ctype != type) continue;
            
            result.Add(tilePos);
        }

        return result;
    }

    //Возвращает все точки тайлов электрики вокруг точки pos
    public static List<Vector2i> FindEnergyTilesAround(Vector2i pos)
    {
        List<Vector2i> points = [];
        Map map = Map.Instance;
        Vector2u size = map.Size;

        for (int i = pos.Y - 1; i <= pos.Y + 1; i++)
        {
            for (int j = pos.X - 1; j <= pos.X + 1; j++)
            {
                if (i == pos.Y && j == pos.X) continue;
                if (i < 0 || i >= size.Y) continue;
                if (j < 0 || j >= size.X) continue;

                TileIdEnum type = map.GetTile(j, i).GetTileType();

                if (TilesHelper.IsEnergyTile(type))
                {
                    points.Add(new Vector2i(j, i));
                }
            }
        }

        return points;
    }
}
