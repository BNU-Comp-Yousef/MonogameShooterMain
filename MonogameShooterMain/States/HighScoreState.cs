using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameShooterMain.Controls;
using MonogameShooterMain.Managers;

namespace MonogameShooterMain.States
{
    public class HighScoreState : State
    {
        private List<Component> _components;

        private SpriteFont _font;

        private ScoreManager _scoreManager;

        public HighScoreState(Shooter g, ContentManager content)
          : base(g, content)
        {
        }

        public override void LoadContent()
        {
            _font = _content.Load<SpriteFont>("Font");

            _scoreManager = ScoreManager.Load();

            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            _components = new List<Component>()
      {
        new Controls.Buttons(buttonTexture, buttonFont)
        {
          Text = "Main Menu",
          Position = new Vector2(Shooter.ScreenWidth / 2, 560),
          Click = new EventHandler(Button_MainMenu_Clicked),
          Layer = 0.1f
        },
      };
        }

        private void Button_MainMenu_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _content));
        }

        public override void Update(GameTime gt)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Clicked(this, new EventArgs());

            foreach (var component in _components)
                component.Update(gt);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.FrontToBack);

            foreach (var component in _components)
                component.Draw(gt, sb);

            sb.End();

            sb.Begin(SpriteSortMode.FrontToBack);

            sb.DrawString(_font, "Highscores:\n" + string.Join("\n", _scoreManager.HighScores.Select(c => c.PlayerName + ": " + c.value)), new Vector2(400, 100), Color.Red);

            sb.End();
        }
    }
}
