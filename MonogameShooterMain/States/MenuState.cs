﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonogameShooterMain.Sprites;
using MonogameShooterMain.Controls;


namespace MonogameShooterMain.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Shooter g, ContentManager content)
          : base(g, content)
        {
        }

        public override void LoadContent()
        {
            var buttonTexture = _content.Load<Texture2D>("Button");
            var buttonFont = _content.Load<SpriteFont>("Font");

            _components = new List<Component>()
      {
        new Sprite(_content.Load<Texture2D>("Background/MainMenu"))
        {
          Layer = 0f,
          Position = new Vector2(Shooter.ScreenWidth / 2, Shooter.ScreenHeight / 2),
        },
        new Buttons(buttonTexture, buttonFont)
        {
          Text = "1 Player",
          Position = new Vector2(Shooter.ScreenWidth / 2, 400),
          Click = new EventHandler(Button_1Player_Clicked),
          Layer = 0.1f
        },
        new Buttons(buttonTexture, buttonFont)
        {
          Text = "2 Players",
          Position = new Vector2(Shooter.ScreenWidth / 2, 440),
          Click = new EventHandler(Button_2Player_Clicked),
          Layer = 0.1f
        },
        new Buttons(buttonTexture, buttonFont)
        {
          Text = "Highscores",
          Position = new Vector2(Shooter.ScreenWidth / 2, 480),
          Click = new EventHandler(Button_Highscores_Clicked),
          Layer = 0.1f
        },
        new Buttons(buttonTexture, buttonFont)
        {
          Text = "Quit",
          Position = new Vector2(Shooter.ScreenWidth / 2, 520),
          Click = new EventHandler(Button_Quit_Clicked),
          Layer = 0.1f
        },
      };
        }

        private void Button_1Player_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _content)
            {
                PlayerCount = 1,
            });
        }

        private void Button_2Player_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new GameState(_game, _content)
            {
                PlayerCount = 2,
            });
        }

        private void Button_Highscores_Clicked(object sender, EventArgs args)
        {
            _game.ChangeState(new HighScoreState(_game, _content));
        }

        private void Button_Quit_Clicked(object sender, EventArgs args)
        {
            _game.Exit();
        }

        public override void Update(GameTime gt)
        {
            foreach (var component in _components)
                component.Update(gt);
        }

        public override void PostUpdate(GameTime gt)
        {

        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.FrontToBack);

            foreach (var component in _components)
                component.Draw(gt, sb);

            sb.End();
        }
    }
}
