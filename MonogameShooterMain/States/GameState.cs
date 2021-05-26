using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameShooterMain.Managers;
using MonogameShooterMain.Sprites;
using System.Collections.Generic;
using System.Linq;

namespace MonogameShooterMain.States
{
    public class GameState : State
    {
        private EnemyManager _enemyManager;

        private SpriteFont _font;

        private List<MainPlayer> _players;

        private ScoreManager _scoreManager;

        private List<Sprite> _sprites;

        public int PlayerCount;

        public GameState(Shooter game, ContentManager content)
          : base(game, content)
        {
        }
        // LoadContent command will load all the game commands when called including explosions and animations.
        public override void LoadContent()
        {
            var bulletTexture = _content.Load<Texture2D>("Bullet");
            

            _font = _content.Load<SpriteFont>("Font");

            _scoreManager = ScoreManager.Load();

            _sprites = new List<Sprite>()
        {
        new Sprite(_content.Load<Texture2D>("Background/Game"))
        {
          Layer = 0.0f,
          Position = new Vector2(Shooter.ScreenWidth / 2, Shooter.ScreenHeight / 2),
        }
        };
            var bulletPrefab = new Bullets(bulletTexture)
            {
                Explosion = new Explosion(new Dictionary<string, Models.Animation>()
            {
              { "Explode", new Models.Animation(_content.Load<Texture2D>("Explosion"), 3) { FrameSpeed = 0.1f, } }
            })
                {
                    Layer = 0.5f,
                }
            };;
            if (PlayerCount ==1)
            {
                AddPlayer(1);
            } //Lambda Code used so continuation occurs when specific code is called.
             _players = _sprites.Where(c => c is MainPlayer).Select(c => (MainPlayer)c).ToList();

            _enemyManager = new EnemyManager(_content)
            {
                Bullet = bulletPrefab,
            };
        }
        // Added AddPlayer command to change player settings depending on which player is playing.
         public  void AddPlayer(int PlayerNum)
        {
            var playerTexture = _content.Load<Texture2D>("Ships/Player");
            var bulletTexture = _content.Load<Texture2D>("Bullet");
            var bulletPrefab = new Bullets(bulletTexture);

            _sprites.Add(new MainPlayer(playerTexture)
            {
                Colour =PlayerNum==1? Color.Blue : Color.Green,
                Position =PlayerNum==1? new Vector2(100, 200) : new Vector2(100, 50),
                Layer =PlayerNum==1? 0.3f : 0.4f,
                Bullet = bulletPrefab,
                Input = new Models.Input()
                {
                    Up =PlayerNum==1? Keys.W : Keys.Up,
                    Down =PlayerNum==1? Keys.S : Keys.Down,
                    Left = PlayerNum == 1 ? Keys.A : Keys.Left,
                    Right = PlayerNum == 1 ? Keys.D : Keys.Right,
                    Shoot = PlayerNum == 1 ? Keys.Space : Keys.Enter,
                },
                Health = 10,
                Score = new Models.Score()
                {
                    PlayerName =PlayerNum==1? "Player 1" : "Player 2",
                    value = 0,
                },
            });
        }
        // update code updates the load times and movement for all ships.
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                _game.ChangeState(new MenuState(_game, _content));

            foreach (var sprite in _sprites)
                sprite.Update(gameTime);

            _enemyManager.Update(gameTime);
            if (_enemyManager.CanAdd && _sprites.Where(c => c is Enemy).Count() < _enemyManager.EnemiesMax)
            {
                _sprites.Add(_enemyManager.GetEnemy());
            }
        }
        // PostUpdate updates all after sprite movement.
        public override void PostUpdate(GameTime gameTime)
        {
            var collidableSprites = _sprites.Where(c => c is CollidableI);

            foreach (var spriteA in collidableSprites)
            {
                foreach (var spriteB in collidableSprites)
                {
                    // Don't do anything if they're the same sprite!
                    if (spriteA == spriteB)
                        continue;

                    if (!spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
                        continue;

                    if (spriteA.Intersects(spriteB))
                        ((CollidableI)spriteA).CollideOn(spriteB);
                }
            }

            // Add the children sprites to the list of sprites (ie bullets)
            int spriteCount = _sprites.Count;
            for (int i = 0; i < spriteCount; i++)
            {
                var sprite = _sprites[i];
                foreach (var child in sprite.Children)
                    _sprites.Add(child);

                sprite.Children = new List<Sprite>();
            }

            for (int i = 0; i < _sprites.Count; i++)
            {
                if (_sprites[i].IsRemoved)
                {
                    _sprites.RemoveAt(i);
                    i--;
                }
            }

            // If all the players are dead, we save the scores, and return to the highscore state
            if (_players.All(c => c.IsDead))
            {
                foreach (var player in _players)
                    _scoreManager.Add(player.Score);

                ScoreManager.Save(_scoreManager);

                _game.ChangeState(new HighScoreState(_game, _content));
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack);

            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            spriteBatch.Begin();

            float x = 10f;
            foreach (var player in _players)
            {
                spriteBatch.DrawString(_font, "Player: " + player.Score.PlayerName, new Vector2(x, 10f), Color.White);
                spriteBatch.DrawString(_font, "Health: " + player.Health, new Vector2(x, 30f), Color.White);
                spriteBatch.DrawString(_font, "Score: " + player.Score.value, new Vector2(x, 50f), Color.White);

                x += 150;
            }
            spriteBatch.End();
        }
    }
}
