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
    class Gifts
    {
        private AnimatedObj Present;
        private AnimatedObj Life;
        private Random rand;
        private int limitXRight;
        private V2 velocityPresent; //מהירות
        private V2 velocityLife; //מהירות
        private bool HasFailledPresent;
        private bool HasFailledLife;
        private int randNumber;
        private int randNumber2;
        private Stopwatch stopWatch;
        private TimeSpan ts;
        /// <summary>
        /// ctor
        /// </summary>
        public Gifts()
        {
            this.limitXRight = Game1.map.GetlimitXRight();
            this.Present = new AnimatedObj("Gifts", States.GiftEnergyInTheAIr, new V2(0, 0), new Rectangle(), C.Transparent, 0, V2.Zero, new V2(1.2f, 1.2f), SE.None,0);
            this.Life = new AnimatedObj("Gifts", States.GiftLife, new V2(0, 0), new Rectangle(), C.Transparent, 0, V2.Zero, new V2(1.2f, 1.2f), SE.None, 0);
            this.HasFailledPresent = false;
            this.HasFailledLife = false;
            this.velocityPresent = new V2();
            this.velocityLife= new V2();
            this.rand = new Random();
            stopWatch = new Stopwatch();
            stopWatch.Start();
            Game1.CallUpdate += new SigUpdate(Update);
        }



       


        public virtual void Update()
        {
            Present.POS = new V2(Present.POS.X, Present.POS.Y + velocityPresent.Y);
            Life.POS = new V2(Life.POS.X, Life.POS.Y + velocityLife.Y);
            ts = stopWatch.Elapsed;
            if(ts.Seconds == 5)
            {
                this.randNumber = rand.Next(0, this.limitXRight);
                this.Present.POS = new V2(randNumber,0);
                this.Present.COLOR = C.White;
                velocityPresent.Y = -6f;
                HasFailledPresent = true;
                ts = stopWatch.Elapsed;

            }
            if (ts.Seconds == 8)
            {
                this.randNumber2 = rand.Next(0, this.limitXRight);
                this.Life.POS = new V2(randNumber2, 0);
                this.Life.COLOR = C.White;
                velocityLife.Y = -6f;
                HasFailledLife = true;
                ts = stopWatch.Elapsed;

            }




            if (ts.Seconds >= 10)
            {
                stopWatch.Reset();
                stopWatch.Start();
            }


            if (HasFailledLife == true || HasFailledPresent==true)
            {
                float i = 1;
                if(HasFailledLife==true)
                velocityLife.Y += 0.15f * i;
                if (HasFailledPresent == true)
                 velocityPresent.Y += 0.15f * i;
        
            }


            if (this.Present.POS.Y > Game1.map.GetIndexHeights(randNumber) && HasFailledPresent == true || this.Life.POS.Y > Game1.map.GetIndexHeights(randNumber2) && HasFailledLife == true)
                {
                    if (HasFailledLife == true)
                        HasFailledLife = false;
                    if (HasFailledPresent == true)
                        HasFailledPresent = false;

                }


            if (HasFailledLife == false || HasFailledPresent == false)
                {

                    if (HasFailledLife == false)
                        velocityLife.Y = 0f;
                    if (HasFailledPresent == false)
                        velocityPresent.Y = 0f;
                } 
          
            






















        }



        public AnimatedObj PRESENT
        {
            get
            {
                return this.Present;
            }
            set
            {
                this.Present = value;
            }

        }

        public AnimatedObj LIFE
        {
            get
            {
                return this.Life;
            }
            set
            {
                this.Life = value;
            }

        }







    }
}
