using LisTech.Tiles;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisTech.Core.SFML;

public class ScreenDrawer
{
    private RenderWindow _window;
    private Text _text;

    private Vector2u _cellSize;

    public ScreenDrawer(RenderWindow window)
    {
        _window = window;
        _text = new()
        {
            Font = new Font("Resources/unifont.otf"),
            FillColor = Color.White
        };

        OnResize();
    }

    public void StartDrawing()
    {
        _window.Clear(Color.Black);
    }

    public void EndDrawing()
    {
        _window.Display();
    }

    public void DrawCell(int x, int y, TileBase tile)
    {
        char icon = tile.GetIcon();
        float xpos = x * _cellSize.X;
        float ypos = y * _cellSize.Y;

        _text.DisplayedString = $"{icon}";
        _text.Position = new Vector2f(xpos, ypos);

        _window.Draw(_text);
    }

    public void DrawText(int x, int y, string text)
    {
        float xpos = x * _cellSize.X;
        float ypos = y * _cellSize.Y;

        _text.Position = new Vector2f(xpos, ypos);

        _text.DisplayedString = text;
        _window.Draw(_text);
    }

    public void SetFontSize(uint size) => _text.CharacterSize = size;
    public void SetDefaultFontSize() => OnResize();

    private void OnResize()
    {
        Vector2u windowSize = _window.Size;
        Vector2u mapSize = Map.Instance.Size;

        uint hCellSize = windowSize.X / mapSize.X;
        uint vCellSize = windowSize.Y / mapSize.Y;

        _cellSize = new Vector2u(hCellSize, vCellSize);
        //ToDo
        _text.CharacterSize = hCellSize;
    }
}
