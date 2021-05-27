using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooterMain.States
{
    //This class loads up all the game states relevant to the order the code is set up in.
    public abstract class State
    {
        protected ShooterGame _game;

        protected ContentManager _content;

        public State(ShooterGame game, ContentManager content)
        {
            _game = game;

            _content = content;
        }

        public abstract void LoadContent();

        public abstract void Update(GameTime gt);

        public abstract void PostUpdate(GameTime gt);

        public abstract void Draw(GameTime gt, SpriteBatch sb);
    }
}
