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
using System.Diagnostics;
using System.Threading;


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
    class MultiPlayerThePlayerColid
    {
        private List<AnimatedObj> Attacks;
        private List<AnimatedObj> Teleports;
        private Character Player;
        private Random random = new Random();
        private List<pixelCollision> CollisionWithAtc;
        private List<pixelCollision> CollisionBotWithGifts;
        private List<pixelCollision> CollisionBotWithTeleports;
        Stopwatch stopWatch;
        TimeSpan ts;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="Attacks"></param>
        /// <param name="A"></param>
        public MultiPlayerThePlayerColid(List<AnimatedObj> Attacks, List<AnimatedObj> Teleports, Character Player)
        {
            this.Attacks = Attacks;
            this.Teleports = Teleports;
            this.Player = Player;
            stopWatch = new Stopwatch();
            this.CollisionWithAtc = new List<pixelCollision>();
            this.CollisionBotWithGifts = new List<pixelCollision>();
            this.CollisionBotWithTeleports = new List<pixelCollision>();
            InitColids();
            Game1.CallUpdate += new SigUpdate(Update);
        }


        private void InitColids()
        {

            for (int i = 0; i < this.Attacks.Count; i++)
            {
                this.CollisionWithAtc.Add(new pixelCollision(this.Player, this.Attacks[i])); 
            }

            CollisionBotWithGifts.Add(new pixelCollision(this.Player, Game1.present.B_AnimatedObj));
            CollisionBotWithGifts.Add(new pixelCollision(this.Player, Game1.GiftLife.B_AnimatedObj));

            for (int i = 0; i < this.Teleports.Count; i++)
            {
                this.CollisionBotWithTeleports.Add(new pixelCollision(this.Player, Teleports[i]));
            }


        }

        public virtual void Update()
        {


            if ((this.CollisionBotWithTeleports[0].ISCollision || this.CollisionBotWithTeleports[2].ISCollision) &&this.Player.KEYS.upPressed())
            
            {
                Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.Teleport].Play();
                int randNumber = random.Next(1, 3);
                if (randNumber == 1)
                {
                    this.Player.POS = new V2(this.Teleports[1].POS.X + 40, this.Teleports[1].POS.Y);
                    
                    
                }
                else
                {
                    this.Player.POS = new V2(this.Teleports[3].POS.X - 40, this.Teleports[3].POS.Y);
               
                }
            }


            if ((this.CollisionBotWithTeleports[1].ISCollision || this.CollisionBotWithTeleports[3].ISCollision) && this.Player.KEYS.upPressed())
            {
                Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.Teleport].Play();
                int randNumber = random.Next(1, 3);
                if (randNumber == 1)
                {

                    this.Player.POS = new V2(this.Teleports[0].POS.X + 40, this.Teleports[0].POS.Y);
        

                }
                else
                {
                    this.Player.POS = new V2(this.Teleports[2].POS.X - 40, this.Teleports[2].POS.Y);
          

                   
                }
            }


            if (this.CollisionBotWithGifts[(int)GiftsColids.ColidPresent].ISCollision)
            {
                Game1.present.B_AnimatedObj.COLOR = C.Transparent;
                Game1.present.B_AnimatedObj.POS = new V2(0, 0);
                this.Player.MAdEnargy.changeMadCharacterAdding(50);
             
            }
            if (this.CollisionBotWithGifts[(int)GiftsColids.ColidLife].ISCollision)
            {
                Game1.GiftLife.B_AnimatedObj.COLOR = C.Transparent;
                Game1.GiftLife.B_AnimatedObj.POS = new V2(0, 0);
                this.Player.MADLife.changeMadCharacterAdding(50);
    
            }


            if (this.CollisionWithAtc[(int)atc.IceAttack].ISCollision)
            {
                this.Player.change_state(States.IceMen);
                this.Player.Repeat = false;
                this.Player.CHaracterNotMove = true;
                this.Player.MADLife.changeMadCharacterDecrease(20);
                stopWatch.Start();
            }



            if (this.CollisionWithAtc[(int)atc.FireAttack].ISCollision)
            {
                this.Player.change_state(States.FireMen);
                this.Player.Repeat = false;
                this.Player.CHaracterNotMove = true;
                this.Player.MADLife.changeMadCharacterDecrease(20);
                stopWatch.Start();

            }

            ts = stopWatch.Elapsed;
            if (ts.Seconds == 2)
            {
                Attacks[(int)atc.IceAttack].change_state(States.bullet2);
                this.Player.CHaracterNotMove = false;
                this.Player.Repeat = true;
                stopWatch.Reset();
                stopWatch.Start();
            }
            //if (Bot.THEBotLife.GetBotLifeValue == 0)
            //{
            //    Bot.COLOR = C.Transparent;
            //    Bot.HaveColidNotMove = true;
                
            //}

        }
    }
}
