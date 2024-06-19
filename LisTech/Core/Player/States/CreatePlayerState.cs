using LisTech.Enums;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace LisTech.Core.Player.States;

public class CreatePlayerState : IPlayerState
{
    private bool _mouseLeftPressed;
    private Vector2i _prevPos;
    private Vector2i _curPos;

    public CreatePlayerState()
    {
        Vector2i mousePos = Mouse.GetPosition(Game.View?.Window);
        _prevPos = mousePos;
        _curPos = mousePos;
    }

    public void MouseLeftPressed()
    {
        _mouseLeftPressed = true;
    }

    public void MouseLeftReleased()
    {
        _mouseLeftPressed = false;
    }

    public void OnUpdate()
    {
        _prevPos = _curPos;
        _curPos = Mouse.GetPosition(Game.View?.Window);

        Draw();
    }

    private void Draw()
    {
        if (!_mouseLeftPressed) { return; }

        Map map = Map.Instance;
        TileIdEnum tileId = Game.Controler?.Player.CreationTileType ?? TileIdEnum.None;

        Vector2i point0 = map.WindowToMapCoord(_prevPos);
        Vector2i point1 = map.WindowToMapCoord(_curPos);

        int x0 = point0.X;
        int y0 = point0.Y;
        int x1 = point1.X;
        int y1 = point1.Y;

        int dx = Math.Abs(x1 - x0);
        int sx = x0 < x1 ? 1 : -1;

        int dy = -Math.Abs(y1 - y0);
        int sy = y0 < y1 ? 1 : -1;

        int e = dx + dy;

        while (true)
        {
            if (!map.OnMap(x0, y0)) { continue; }
            map.SetTile(x0, y0, tileId);

            if (x0 == x1 && y0 == y1) break;

            int e2 = e * 2;

            if (e2 >= dy)
            {
                if (x0 == x1) break;
                e += dy;
                x0 += sx;
            }

            if (e2 <= dx)
            {
                if (y0 == y1) break;
                e += dx;
                y0 += sy;
            }
        }
    }
}