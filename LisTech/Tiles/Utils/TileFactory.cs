using LisTech.Enums;

namespace LisTech.Tiles.Utils;

public class TileFactory
{
    private static readonly TileBase NoneTile = new NoneTile();

    public static TileBase Create(TileIdEnum type)
    {
        TileBase tile = NoneTile;

        switch (type)
        {
            case TileIdEnum.None:
                tile = NoneTile;
                break;
            case TileIdEnum.EnergySource:
                tile = new EnergySourceTile();
                break;
            case TileIdEnum.Wire:
                tile = new WireTile();
                break;
            case TileIdEnum.Light:
                tile = new LightTile();
                break;
        }

        return tile;
    }
}


