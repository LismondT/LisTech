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

    public GameView(GameModel model)
    {
        GameModel = model;
        Vector2u screenSize = GlobalVariables.ScreenSize;

        VideoMode videoMode = new(screenSize.X, screenSize.Y);
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

        //Обводка клетки
        Vector2f cellSize = (Vector2f)ScreenDrawer.CellSize;
        RectangleShape shape = new RectangleShape();
        shape.Size = cellSize;
        shape.FillColor = Color.Transparent;
        shape.OutlineColor = Color.Green;
        shape.OutlineThickness = 1;
        shape.Position = new Vector2f(mapX * cellSize.X, mapY * cellSize.Y);

        Window.Draw(shape);
    }

    public void OnResize() => ScreenDrawer.OnResize();
}
