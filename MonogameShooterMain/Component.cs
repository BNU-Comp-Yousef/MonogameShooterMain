using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooterMain
{
    public abstract class Component
    {
        public abstract void Draw(GameTime gt, SpriteBatch sb);

        public abstract void Update(GameTime gt);
    }
}
