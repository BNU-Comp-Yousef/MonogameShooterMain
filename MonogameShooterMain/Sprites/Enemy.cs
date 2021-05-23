using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooterMain.Sprites
{
    public class Enemy : Ship
    {
        private float _timer;

        public float ShootTimer = 1.75f;
        public Enemy(Texture2D texture) : base(texture)
        {
            Speed = 2f;
        }

        public override void Update(GameTime gt)
        {
            _timer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (_timer >= ShootTimer)
            {
                Shoot(-5f);
                _timer = 0;
            }

            Position += new Vector2(-Speed, 0);
            // If the enemy goes too far to left side of the screen.
            if (Position.X < -_texture.Width)
                IsRemoved = true;
        }

        public override void CollideOn(Sprite sp)
        {
            // If we crash into a player that is still alive.
            if (sp is MainPlayer && !((MainPlayer)sp).IsDead)
            {
                ((MainPlayer)sp).Score.value++;

                // This will remove the ship completely.
                IsRemoved = true;
            }

            // This code was made in the case we hit a bullet that belongs to a player.
            if (sp is Bullets && sp.Parent is MainPlayer)
            {
                Health--;

                if (Health <= 0)
                {
                    IsRemoved = true;

                    ((MainPlayer)sp.Parent).Score.value++;
                }
            }
        }
    }
}
