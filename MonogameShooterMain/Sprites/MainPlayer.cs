using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonogameShooterMain.Models;


namespace MonogameShooterMain.Sprites
{
    public class MainPlayer : Ship
    {
        private KeyboardState _currentKey;

        private KeyboardState _previousKey;

        private float _shootTimer = 0;

        public bool IsDead
        {
            get
            {
                return Health <= 0;
            }
        }

        public Input Input { get; set; }

        public Score Score { get; set; }

        public MainPlayer(Texture2D texture)
          : base(texture)
        {
            Speed = 3f;
        }

        public override void Update(GameTime gt)
        {
            if (IsDead)
                return;

            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            var velocity = Vector2.Zero;
            _rotation = 0;

            if (_currentKey.IsKeyDown(Input.Up))
            {
                velocity.Y = -Speed;
                _rotation = MathHelper.ToRadians(-15);
            }
            else if (_currentKey.IsKeyDown(Input.Down))
            {
                velocity.Y += Speed;
                _rotation = MathHelper.ToRadians(15);
            }

            if (_currentKey.IsKeyDown(Input.Left))
            {
                velocity.X -= Speed;
            }
            else if (_currentKey.IsKeyDown(Input.Right))
            {
                velocity.X += Speed;
            }

            _shootTimer += (float)gt.ElapsedGameTime.TotalSeconds;
            // We can hold down the shoot button with this command.
            if (_currentKey.IsKeyDown(Input.Shoot) && _shootTimer > 0.25f)
            {
                Shoot(Speed * 2);
                _shootTimer = 0f;
            }

            Position += velocity;
            // Will keep the ship between the 2 points.
            // Position = Vector2.Clamp(Position, new Vector2(80, 0), new Vector2(Game1.ScreenWidth / 4, Game1.ScreenHeight));
        }

        public override void Draw(GameTime gt, SpriteBatch sb)
        {
            if (IsDead)
                return;

            base.Draw(gt, sb);
        }

        public override void CollideOn(Sprite sp)
        {
            if (IsDead)
                return;

            if (sp is Bullets && ((Bullets)sp).Parent is Enemy)
                Health--;

            if (sp is Enemy)
                Health -= 3;
        }
    }
}
