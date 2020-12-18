#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


#endregion

#region Shorcuts
using T2 = Microsoft.Xna.Framework.Graphics.Texture2D;
using V2 = Microsoft.Xna.Framework.Vector2;
using Rec = Microsoft.Xna.Framework.Rectangle;
using C = Microsoft.Xna.Framework.Color;
using SE = Microsoft.Xna.Framework.Graphics.SpriteEffects;
using F = System.Single;//F = float
using SB = Microsoft.Xna.Framework.Graphics.SpriteBatch;
using GD = Microsoft.Xna.Framework.GraphicsDeviceManager;
using CM = Microsoft.Xna.Framework.Content.ContentManager;
#endregion

namespace MyGameXNA
{
    public enum SoundsE { GameOver, Teleport, Heartbeat, tick, WiningGame };
 public class SoundsEffectsGame
    {
        private Song SoundBackground;
        private List<SoundEffect> Sounds;
        public SoundsEffectsGame()
        {
            this.Sounds = new List<SoundEffect>();
            initLoad();     
        }
        public void initLoad()
        {
            SoundEffect GameOver = Tools.cm.Load<SoundEffect>("Sounds/GameOver");
            Sounds.Add(GameOver);
            SoundEffect Teleport = Tools.cm.Load<SoundEffect>("Sounds/Teleport");
            Sounds.Add(Teleport);
            SoundEffect Heart = Tools.cm.Load<SoundEffect>("Sounds/Heartbeat");
            Sounds.Add(Heart);
            SoundEffect tick = Tools.cm.Load<SoundEffect>("Sounds/Tick");
            Sounds.Add(tick);
            SoundEffect WiningGame = Tools.cm.Load<SoundEffect>("Sounds/WiningGame");
            Sounds.Add(WiningGame);
        }




        public List<SoundEffect> ListSoundsEffectsGame
        {
            get
            {
                return this.Sounds;
            }
            set
            {
                this.Sounds = value;
            }
        }





    }
}
