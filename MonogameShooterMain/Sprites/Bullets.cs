using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MonogameShooterMain.Sprites
{
    public class Bullets : Sprite, CollidableI
    {
        private float _timer;

        public float LifeSpan { get; set; }

        public Vector2 Velocity { get; set; }

        public Bullets(Texture2D texture) : base(texture)
        {

        }

        public override void Update(GameTime gt)
        {
            _timer += (float)gt.ElapsedGameTime.TotalSeconds;

            if (_timer >= LifeSpan)
                IsRemoved = true;
            Position += Velocity;
        }

        public void CollideOn(Sprite sp)
        {
            throw new NotImplementedException();
        }
    }
}
