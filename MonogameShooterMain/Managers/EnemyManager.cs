using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonogameShooterMain.Sprites;

namespace MonogameShooterMain.Managers
{
    public class EnemyManager
    {
        private float _timer;

        private List<Texture2D> _textures;

        public bool CanAdd { get; set; }

        public Bullets Bullet { get; set; }

        public int EnemiesMax { get; set; }

        public float SpawnTimer { get; set; }

        public EnemyManager(ContentManager content)
        {
            _textures = new List<Texture2D>()
            {
                content.Load<Texture2D>("Ships/Enemy"),
                content.Load<Texture2D>("Ships/Enemy1"),
            };

            EnemiesMax = 10;

            SpawnTimer = 2.5f;
        }

        public void Update(GameTime gt)
        {
            _timer += (float)gt.ElapsedGameTime.TotalSeconds;

            CanAdd = false;

            if(_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0f;
            }
        }

        public Enemy GetEnemy()
        {
            var texture = _textures[ShooterGame.Random.Next(0, _textures.Count)];

            return new Enemy(texture)
            {
                Colour = Color.Red,
                Bullet = Bullet,
                Health = 5,
                Layer = 0.2f,
                Position = new Vector2(ShooterGame.ScreenWidth + texture.Width, 
                ShooterGame.Random.Next(0, ShooterGame.ScreenHeight)),
                Speed = 2 + (float)ShooterGame.Random.NextDouble(),
            };
        }
    }
}
