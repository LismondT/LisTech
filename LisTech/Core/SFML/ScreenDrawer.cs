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

    public Vector2u CellSize { get => _cellSize; set => _cellSize = value; }

    public ScreenDrawer(RenderWindow window)
    {
        _window = window;
        _text = new()
        {
            Font = new Font("Resources/EversonMonoBold.ttf"),
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
        float xpos = x * CellSize.X;
        float ypos = y * CellSize.Y;

        _text.DisplayedString = $"{icon}";
        _text.Position = new Vector2f(xpos, ypos);
        _text.FillColor = tile.GetColor();

        _window.Draw(_text);

        _text.FillColor = Color.White;
    }

    public void DrawText(int x, int y, string text)
    {
        float xpos = x * CellSize.X;
        float ypos = y * CellSize.Y;

        _text.Position = new Vector2f(xpos, ypos);

        _text.DisplayedString = text;
        _window.Draw(_text);
    }

    public void SetFontSize(uint size) => _text.CharacterSize = size;
    public void SetDefaultFontSize() => OnResize();

    public void OnResize()
    {
        Vector2u windowSize = _window.Size;
        Vector2u mapSize = Map.Instance.Size;

        uint hCellSize = windowSize.X / mapSize.X;
        uint vCellSize = windowSize.Y / mapSize.Y;

        CellSize = new Vector2u(hCellSize, vCellSize);
        //ToDo
        _text.CharacterSize = hCellSize;
    }
}
