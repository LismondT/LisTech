using LisTech.Core;
using LisTech.Enums;
using LisTech.Tiles.Utils;
using SFML.System;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace LisTech.Tiles;

#region TileBases



public abstract class TileBase
{
    public abstract TileIdEnum GetTileType();
    public abstract char GetIcon();
    public abstract string GetInfo();

    public virtual void OnCreate(Vector2i pos) { }
    public virtual void Update(Vector2i pos) { }

    public virtual void OnClick(bool isRight) { }
    public virtual void OnButtonPressed(char button) { }
    public virtual void OnTouch(Vector2i pos, Vector2i tpos, object touchedTile, TileIdEnum type) { }
}



public abstract class EnergyTileBase : TileBase
{
    private int _power = 0;

    public int GetPower() => _power;
    public int SetPower(int power) => _power = power;

    public override void OnCreate(Vector2i pos)
    {
        List<Vector2i> energyTiles = TilesFinder.FindEnergyTilesAround(pos);

        int maxPower = GetPower();

        foreach (Vector2i tilePos in energyTiles)
        {
            EnergyTileBase tile = (EnergyTileBase)Map.Instance.GetTile(tilePos);

            if (tile.GetPower() > maxPower) { maxPower = tile.GetPower(); }
        }
        SetPower(maxPower - 1);

        foreach (Vector2i tilePos in energyTiles)
        {
            EnergyTileBase tile = (EnergyTileBase)Map.Instance.GetTile(tilePos);

            if (tile.GetPower() < GetPower() - 1)
            {
                tile.OnTouch(tilePos, pos, (object)this, GetTileType());
            }
        }

    }

    public override void OnTouch(Vector2i pos, Vector2i tpos, object touchedTile, TileIdEnum type)
    {
        if (TilesHelper.IsEnergyTile(type))
        {
            Map map = Map.Instance;
            EnergyTileBase tile = (EnergyTileBase)touchedTile;
            List<Vector2i> points = TilesFinder.FindEnergyTilesAround(pos);

            int power = tile.GetPower();
            SetPower(power - 1);


#if DEBUG
            Console.WriteLine($"OnTouch - pos: {pos}, tpos: {tpos}, TileType: {type}, electric around: {points.Count}");
            Console.WriteLine($"MyType: {GetTileType()}, Power: {GetPower()})");
#endif


            foreach (Vector2i p in points)
            {
                EnergyTileBase tileToTouch = (EnergyTileBase)map.GetTile(p);

                if (tileToTouch.GetPower() < GetPower() - 1)
                {
                    tileToTouch.OnTouch(p, pos, (object)this, GetTileType());
                }
            }
        }
    }
}



#endregion

#region BaseTiles



public class NoneTile : TileBase
{
    public override TileIdEnum GetTileType() => TileIdEnum.None;
    public override char GetIcon() => ' ';
    public override string GetInfo() => "Ничего";
}



#endregion

#region EnergyTiles



public class EnergySourceTile : EnergyTileBase
{
    public EnergySourceTile()
    {
        SetPower(900);
    }

    public override TileIdEnum GetTileType() => TileIdEnum.EnergySource;
    public override char GetIcon() => 'E';
    public override string GetInfo() => "Источник энергии. Проводит электричество по проводам";


    public override void Update(Vector2i pos)
    {
        var map = Map.Instance;
        List<Vector2i> points = TilesFinder.FindEnergyTilesAround(pos);

        foreach (Vector2i p in points)
        {
            EnergyTileBase tile = (EnergyTileBase)map.GetTile(p);

            if (tile.GetPower() < GetPower() - 1)
            {
                tile.OnTouch(p, pos, (object)this, GetTileType());
            }
        }
    }
}



public class WireTile : EnergyTileBase
{
    private char _icon;

    private const char _defaultIcon = ' ';

    Dictionary<int, char> _tilemap = new()
    {
        { 0, '▀' }, //Single
        { 1, '▀' }, //Up
        { 2, '▀' }, //Right
        { 3, '╚' }, //Up + Right
        { 4, '▀' }, //Down
        { 5, '║' }, //Up + Down
        { 6, '╔' }, //Down + Right
        { 7, '╠' }, // Down + Up + Right
        { 8, '▀' }, //Left
        { 9, '╝' }, //Up + Left
        { 10, '═' }, //Left + Right
        { 11, '╩' }, //Left + Up + Right
        { 12, '╗' }, //Down + Left
        { 13, '╣' }, //Down + Left + Up
        { 14, '╦' }, //Left + Down + Right
        { 15, '╬' }, //Center
    };

    public override TileIdEnum GetTileType() => TileIdEnum.Wire;
    public override char GetIcon() => _icon;
    public override string GetInfo() => "Провода";

    public override void OnCreate(Vector2i pos)
    {
        base.OnCreate(pos);
    }

    public override void Update(Vector2i pos)
    {
        base.Update(pos);

        UpdateIcon(pos);
    }

    private void UpdateIcon(Vector2i pos)
    {
        List<Vector2i> wiresPos = TilesFinder.FindTilesCross(pos, TileIdEnum.Wire);

        int tileIndex = 0;
        
        foreach (Vector2i w in wiresPos)
        {
            TileAround relPos = TilesHelper.GetRelativePosition(pos, w);
            tileIndex += (int)relPos;
        }

        _icon = _tilemap[tileIndex];
    }
}



public class LightTile : EnergyTileBase
{
    public override TileIdEnum GetTileType() => TileIdEnum.Light;
    public override char GetIcon() => _curIcon;
    public override string GetInfo() => "Лампочка. Светится, когда подключенна к электричеству";

    private char _curIcon;

    private const char LightOnIcon = 'O';
    private const char LightOffIcon = '-';

    public LightTile()
    {
        _curIcon = LightOffIcon;
    }

    public override void OnCreate(Vector2i pos)
    {
        base.OnCreate(pos);
        if (GetPower() <= 0)
        {
            _curIcon = LightOffIcon;
        }
        else
        {
            _curIcon = LightOnIcon;
        }
    }

    public override void OnTouch(Vector2i pos, Vector2i tpos, object touchedTile, TileIdEnum type)
    {
        Console.WriteLine("Light is touched");
        base.OnTouch(pos, tpos, touchedTile, type);


        if (TilesHelper.IsEnergyTile(type))
        {
            if (GetPower() <= 0)
            {
                _curIcon = LightOffIcon;
            }
            else
            {
                _curIcon = LightOnIcon;
            }
        }
    }
}



#endregion