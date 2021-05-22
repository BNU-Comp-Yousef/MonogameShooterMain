using System;
using System.Collections.Generic;
using System.Text;
using MonogameShooterMain.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooterMain.Managers
{
    public class AnimationsManager : ICloneable
    {
        private Animation _animation;

        private float _timer;

        public Animation CurrentAnimation
        {
            get
            {
                return _animation;
            }
        }

        public float Layer { get; set; }

        public Vector2 Origin { get; set; }

        public Vector2 Position { get; set; }

        public float Rotation { get; set; }

        public float Scale { get; set; }

        public AnimationsManager(Animation animation)
        {
            _animation = animation;

            Scale = 1f;
        }

        public void Draw(SpriteBatch sb)
        {
            sb.Draw(
                _animation.Texture, Position, new Rectangle(
                    _animation.CurrentFrame * _animation.FrameWidth, 0,
                    _animation.FrameWidth, _animation.FrameHeight),
                Color.White, Rotation, Origin, Scale, SpriteEffects.None, Layer);
        }

        public void Play(Animation animation)
        {
            if (_animation == animation)
                return;

            _animation = animation;

            _animation.CurrentFrame = 0;

            _timer = 0;
        }

        public void Update(GameTime gt)
        {
            _timer += (float)gt.ElapsedGameTime.TotalSeconds;

            if(_timer > _animation.FrameSpeed)
            {
                _timer = 0f;

                _animation.CurrentFrame++;

                if (_animation.CurrentFrame >= _animation.FrameCount)
                    _animation.CurrentFrame = 0;
            }
        }

        public object Clone()
        {
            var animationsManager = this.MemberwiseClone() as AnimationsManager;

            animationsManager._animation = animationsManager._animation.Clone() as Animation;

            return animationsManager;
        }
    }
}
