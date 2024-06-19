using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LisTech.Core.Player.States;

public interface IPlayerState
{
    public virtual void MouseLeftPressed() { }
    public virtual void MouseLeftReleased() { }
    public virtual void MouseRightPressed() { }
    public virtual void MouseRightReleased() { }
    public virtual void OnUpdate() { }
}
