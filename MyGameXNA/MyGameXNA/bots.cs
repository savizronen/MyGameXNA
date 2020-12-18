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
using System.Diagnostics;
#endregion



namespace MyGameXNA
{
    class bots
    {
        List<AICharacter> Thebots;
        Stopwatch stopWatch;
        TimeSpan ts;
        int count;
        public bots()
        {
            Thebots = new List<AICharacter>();
             count = 0;
            stopWatch = new Stopwatch();
            stopWatch.Start();
            Game1.CallUpdate += new SigUpdate(Update);
        }
        public virtual void Update()
        {

            ts=stopWatch.Elapsed;
            if (ts.Seconds > 10)
            {
                stopWatch.Reset();
                stopWatch.Restart();
            }
            if (ts.Seconds == 10)
             {
                 Thebots.Add(new AICharacter("player3", States.stand, new V2(2119, 174), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, Game1.mypic, Game1.mypic.characterAttacksToBotColid));
                 new Colides(Game1.mypic.characterAttacksToBotColid,Game1.teleports, Thebots[count]);
                 count++;
                 stopWatch.Reset();
                 stopWatch.Restart();

            }

        }





    }
}
