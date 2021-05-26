using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonogameShooterMain.Sprites
{
    //This class sets up all the bullet movements when shooting from the ship sprite.
    public class Bullets : Sprite, CollidableI
    {
        private float _timer;

        public Explosion Explosion;

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
            // Bullets won't collide with eachother.
            if (sp is Bullets)
                return;

            // Enemies won't be able to shoot eachother.
            if (sp is Enemy && this.Parent is Enemy)
                return;

            // Players won't be able to shoot eachother. 
            if (sp is MainPlayer && this.Parent is MainPlayer)
                return;

            // Bullets won't hit player if they're dead.
            if (sp is MainPlayer && ((MainPlayer)sp).IsDead)
                return;

            if (sp is Enemy && this.Parent is MainPlayer)
            {
                IsRemoved = true;
                AddExplosion();
            }

            if (sp is MainPlayer && this.Parent is Enemy)
            {
                IsRemoved = true;
                AddExplosion();
            }

            // throw new NotImplementedException();
        }

        public void AddExplosion()
        {
            if (Explosion == null)
                return;

            var explosion = Explosion.Clone() as Explosion;
            explosion.Position = this.Position;

            Children.Add(explosion);
        }
    }
}
