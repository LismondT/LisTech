using LisTech.Core;
using LisTech.Tiles.Utils;
using System.Drawing;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using LisTech.Core.Player;
using LisTech.Enums;
using LisTech.Core.SFML;

namespace LisTech.MVC;

public class GameControler
{
    private GameModel GameModel { get; }
    private GameView GameView { get; }

    public Player Player { get; }

    public GameControler(GameModel model, GameView view)
    {
        GameModel = model;
        GameView = view;

        Player = new Player();

        RenderWindow window = GameView.Window;

        window.Closed += (_, __) => window.Close();
        window.MouseButtonPressed += Window_MouseButtonPressed;
        window.MouseButtonReleased += Window_MouseButtonReleased;
        window.MouseMoved += Window_MouseMoved;
        window.KeyPressed += Window_KeyPressed;
        window.Resized += Window_Resized;
    }


    public void MainLoop()
    {
        RenderWindow window = GameView.Window;
        while (window.IsOpen)
        {
            window.DispatchEvents();
            Player.State.OnUpdate();
            UpdateTiles();
            GameView.Render();
        }
    }


    private void UpdateTiles()
    {
        var size = Map.Instance.Size;
        var map = Map.Instance.TileMap;

        for (int i = 0; i < size.Y; i++)
        {
            for (int j = 0; j < size.X; j++)
            {
                Vector2i pos = new(j, i);
                map[i, j].Update(pos);
            }
        }
    }




    #region Events

    private void Window_MouseButtonPressed(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button == Mouse.Button.Left) Player.State.MouseLeftPressed();
        if (e.Button == Mouse.Button.Right) Player.State.MouseRightPressed();
    }


    private void Window_MouseButtonReleased(object? sender, MouseButtonEventArgs e)
    {
        if (e.Button == Mouse.Button.Left) Player.State.MouseLeftReleased();
        if (e.Button == Mouse.Button.Right) Player.State.MouseRightReleased();
    }


    private void Window_MouseMoved(object? sender, MouseMoveEventArgs e)
    {
 
    }


    private void Window_KeyPressed(object? sender, KeyEventArgs e)
    {
        Keyboard.Key key = e.Code;

        if (key == Keyboard.Key.Num0) Player.CreationTileType = TileIdEnum.None;
        if (key == Keyboard.Key.Num1) Player.CreationTileType = TileIdEnum.Wire;
        if (key == Keyboard.Key.Num2) Player.CreationTileType = TileIdEnum.EnergySource;
        if (key == Keyboard.Key.Num3) Player.CreationTileType = TileIdEnum.Light;
        if (key == Keyboard.Key.Num4) Player.CreationTileType = TileIdEnum.Wall;

        if (key == Keyboard.Key.C) Map.Instance.ClearMap();
    }

    private void Window_Resized(object? sender, SizeEventArgs e)
    {
        
    }

    #endregion
}
