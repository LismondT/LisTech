using LisTech.Enums;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace LisTech.Core.Player;

public enum PlayerStateEnum
{
    Create,
    Use,
    Info
}

public interface IPlayerState
{
    public virtual void MouseLeftPressed() { }
    public virtual void MouseLeftReleased() { }
    public virtual void MouseRightPressed() { }
    public virtual void MouseRightReleased() { }
    public virtual void OnUpdate() { }
}


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

        RenderWindow window = Game.View?.Window ?? throw new Exception();
        Vector2u mapSize = Map.Instance.Size;
        Vector2u screenSize = window.Size;
        TileIdEnum tileId = Game.Controler?.Player.CreationTileType ?? TileIdEnum.None;


        int x0 = (int)(_prevPos.X * mapSize.X / screenSize.X);
        int y0 = (int)(_prevPos.Y * mapSize.Y / screenSize.Y);
        int x1 = (int)(_curPos.X * mapSize.X / screenSize.X);
        int y1 = (int)(_curPos.Y * mapSize.Y / screenSize.Y);

        x0 = (int)Math.Min(x0, Map.Instance.Size.X - 1);
        x0 = (int)Math.Max(x0, 0);
        y0 = (int)Math.Min(y0, Map.Instance.Size.Y - 1);
        y0 = (int)Math.Max(y0, 0);

        x1 = (int)Math.Min(x1, Map.Instance.Size.X - 1);
        x1 = (int)Math.Max(x1, 0);
        y1 = (int)Math.Min(y1, Map.Instance.Size.Y - 1);
        y1 = (int)Math.Max(y1, 0);

        int dx = Math.Abs(x1- x0);
        int sx = x0 < x1 ? 1 : -1;

        int dy = Math.Abs(y1- y0);
        int sy = y0 < y1 ? 1 : -1;

        int e = dx + dy;

        while (true)
        {
            if (!Map.Instance.OnMap(x0, y0)) { continue; }
            Map.Instance.SetTile(x0, y0, tileId);

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


        ////int mouseX = Mouse.GetPosition(window).X;
        ////int mouseY = Mouse.GetPosition(window).Y;

        ////int mapX = (int)(mouseX * mapSize.X / screenSize.X);
        ////int mapY = (int)(mouseY * mapSize.Y / screenSize.Y);

        //int prevMapX = (int)(_prevPos.X * mapSize.X / screenSize.X);
        //int prevMapY = (int)(_prevPos.Y * mapSize.Y / screenSize.Y);

        //int curMapX = (int)(_curPos.X * mapSize.X / screenSize.X);
        //int curMapY = (int)(_curPos.Y * mapSize.Y / screenSize.Y);

        //int dx = curMapX - prevMapX;
        //int dy = curMapY - prevMapY;

        //float yc = dy / dx;
        //int ycc = 0;
        //for (int x = x1; x <= x2; x++)
        //{
        //    int y = y2 + (int)(yc * ycc);
        //    ycc++;
        //    // Assuming that the round function finds 
        //    // closest integer to a given float.

        //}
    }
}