using LisTech.Core;
using LisTech.Core.SFML;
using LisTech.Enums;
using LisTech.Tiles;
using LisTech.Tiles.Utils;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace LisTech.MVC;

public class GameView
{
    public RenderWindow Window { get; set; }

    //public TileBase TileInfo { private get; set; }

    public GameView(GameModel model)
    {
        GameModel = model;
        //TileInfo = TileFactory.Create(TileType.None);

        VideoMode videoMode = new(900, 900);
        string title = "LisTech";

        Window = new RenderWindow(videoMode, title);
        Window.SetFramerateLimit(60);

        ScreenDrawer = new ScreenDrawer(Window);
    }

    private GameModel GameModel { get; set; }

    private ScreenDrawer ScreenDrawer { get; set; }

    public void Render()
    {
        ScreenDrawer.StartDrawing();
        DrawMap();
        DrawInfo();
        ScreenDrawer.EndDrawing();
    }

    public void DrawMap()
    {
        Map map = Map.Instance;

        for (int i = 0; i < map.Size.Y; i++)
        {
            for (int j = 0; j < map.Size.X; j++)
            {
                TileBase tile = map.GetTile(j, i);

                if (tile.GetTileType() == TileIdEnum.None) continue;

                ScreenDrawer.DrawCell(j, i, tile);
            }
        }
    }

    public void DrawInfo()
    {
        if (!Window.HasFocus()) return;

        Vector2u mapSize = Map.Instance.Size;

        int mouseX = Mouse.GetPosition(Window).X;
        int mouseY = Mouse.GetPosition(Window).Y;
        Vector2u screenSize = Window.Size;

        int mapX = (int)(mouseX * mapSize.X / screenSize.X);
        int mapY = (int)(mouseY * mapSize.Y / screenSize.Y);

        if (!Map.Instance.OnMap(mapX, mapY)) return;

        TileBase tile = Map.Instance.GetTile(mapX, mapY);
        //TileInfo = tile;

        TileIdEnum type = tile.GetTileType();
        char icon = tile.GetIcon();
        string info = tile.GetInfo();

        ScreenDrawer.SetFontSize(26);
        ScreenDrawer.DrawText(0, 0, $"TileType: \"{type}\" | icon: \"{icon}\" | info: \"{info}\"");
        
        if (TilesHelper.IsEnergyTile(type))
        {
            EnergyTileBase energyTile = (EnergyTileBase)tile;

            int power = energyTile.GetPower();
            ScreenDrawer.DrawText(0, 1, $"Power: \"{power}\"");
        }

        ScreenDrawer.SetDefaultFontSize();
    }
}
