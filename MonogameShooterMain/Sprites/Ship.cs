using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MonogameShooterMain.Sprites
{
    public class Ship : Sprite, CollidableI
    {
        public int Health { get; set; }

        public Bullets Bullet { get; set; }

        public float Speed;

        public Ship(Texture2D texture) : base(texture)
        {
        }

        protected void Shoot(float speed)
        {
            //You cant shoot if you haven't added a bullet prefab.
            if (Bullet == null)
                return;

            var bullet = Bullet.Clone() as Bullets;
            bullet.Position = this.Position;
            bullet.Colour = this.Colour;
            bullet.Layer = 0.1f;
            bullet.LifeSpan = 5f;
            bullet.Velocity = new Vector2(speed, 0f);
            bullet.Parent = this;

            Children.Add(bullet);
        }

        public virtual void CollideOn(Sprite sp)
        {
            throw new NotImplementedException();
        }
    }
}
