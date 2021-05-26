using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonogameShooterMain.Sprites;
using System.Collections.Generic;

namespace MonogameShooterMain.Managers
{
    //This class calls the enemy sprites and adds their textures as well as all the movement and set up they need.
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
                content.Load<Texture2D>("Ships/Enemy_1"),
                content.Load<Texture2D>("Ships/Enemy_2"),
            };

            EnemiesMax = 10;

            SpawnTimer = 2.5f;
        }

        public void Update(GameTime gt)
        {
            _timer += (float)gt.ElapsedGameTime.TotalSeconds;

            CanAdd = false;

            if (_timer > SpawnTimer)
            {
                CanAdd = true;

                _timer = 0f;
            }
        }
        // This function calls an enemy with all the variables it needs.
        public Enemy GetEnemy()
        {
            var texture = _textures[Shooter.Random.Next(0, _textures.Count)];

            return new Enemy(texture)
            {
                Colour = Color.Red,
                Bullet = Bullet,
                Health = 5,
                Layer = 0.2f,
                Position = new Vector2(Shooter.ScreenWidth + texture.Width,
                Shooter.Random.Next(0, Shooter.ScreenHeight)),
                Speed = 2 + (float)Shooter.Random.NextDouble(),
                //ShootingTimer = 1.5f + (float)Shooter.Random.NextDouble(),
            };
        }
    }
}
