using LisTech.Enums;
using LisTech.Tiles.Utils;
using SFML.System;

namespace LisTechTests
{
    [TestClass]
    public class TilesHelperTests
    {
        [TestMethod]
        public void GetRelativePositionTest()
        {
            Vector2i start = new Vector2i(0, 0);

            Vector2i center = new Vector2i(0, 0);

            Vector2i right = new Vector2i(1, 0);
            Vector2i rightUp = new Vector2i(1, -1);
            Vector2i rightDown = new Vector2i(1, 1);
            
            Vector2i left = new Vector2i(-1, 0);
            Vector2i leftUp = new Vector2i(-1, -1);
            Vector2i leftDown = new Vector2i(-1, 1);

            Vector2i up = new Vector2i(0, -1);
            Vector2i down = new Vector2i(0, 1);

            TileAround forCenter = TileAround.Center;
            
            TileAround forRight = TileAround.Right;
            TileAround forRightUp = TileAround.Right | TileAround.Up;
            TileAround forRightDown = TileAround.Right | TileAround.Down;
            
            TileAround forLeft = TileAround.Left;
            TileAround forLeftUp = TileAround.Left | TileAround.Up;
            TileAround forLeftDown = TileAround.Left | TileAround.Down;

            TileAround forUp = TileAround.Up;
            TileAround forDown = TileAround.Down;

            Assert.IsTrue(TilesHelper.GetRelativePosition(start, center) == forCenter, "Center");

            Assert.IsTrue(TilesHelper.GetRelativePosition(start, right) == forRight, "Right");
            Assert.IsTrue(TilesHelper.GetRelativePosition(start, rightUp) == forRightUp, "RightUp");
            Assert.IsTrue(TilesHelper.GetRelativePosition(start, rightDown) == forRightDown, "RightDown");
            
            Assert.IsTrue(TilesHelper.GetRelativePosition(start, left) == forLeft, "Left");
            Assert.IsTrue(TilesHelper.GetRelativePosition(start, leftUp) == forLeftUp, "LeftUp");
            Assert.IsTrue(TilesHelper.GetRelativePosition(start, leftDown) == forLeftDown, "LeftDown");

            Assert.IsTrue(TilesHelper.GetRelativePosition(start, up) == forUp, "Up");
            Assert.IsTrue(TilesHelper.GetRelativePosition(start, down) == forDown, "Down");
        }
    }
}