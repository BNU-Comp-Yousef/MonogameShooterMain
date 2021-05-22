using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooterMain.States
{
    public abstract class State
    {
        protected Shooter _game;

        protected ContentManager _content;

        public State(Shooter g, ContentManager content)
        {
            _game = g;

            _content = content;
        }

        public abstract void LoadContent();

        public abstract void Update(GameTime gt);

        public abstract void PostUpdate(GameTime gt);

        public abstract void Draw(GameTime gt, SpriteBatch sb);
    }
}
