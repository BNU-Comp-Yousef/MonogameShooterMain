using System;
using System.Collections.Generic;
using System.Text;

namespace MonogameShooterMain.Sprites
{
    public interface CollidableI
    {
        void CollideOn(Sprite sp);
    }
}
