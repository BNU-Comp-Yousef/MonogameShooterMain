using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace MonogameShooterMain.Managers
{
    public class SoundManager
    {
        private static readonly Dictionary<string, SoundEffect> SoundEffects =
            new Dictionary<string, SoundEffect>();

        public static void LoadContent(ContentManager content)
        {
            SoundEffects.Add("flame", content.Load<SoundEffect>("Sound/flame"));
        }

        public static SoundEffect GetSoundEffect(string effect)
        {
            return SoundEffects[effect];
        }
    }
}
