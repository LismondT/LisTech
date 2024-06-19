using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LisTech.Enums;
using LisTech.MVC;
using LisTech.Core.Player.States;

namespace LisTech.Core.Player;


public class Player
{
    public IPlayerState State { set; get; }
    public TileIdEnum CreationTileType { set; get; } = TileIdEnum.None;

    public Player() => State = new CreatePlayerState();

    public void SetState(PlayerStateEnum state)
    {
        switch (state)
        {
            case PlayerStateEnum.Create:
                State = new CreatePlayerState();
                break;

        }
    }
}


