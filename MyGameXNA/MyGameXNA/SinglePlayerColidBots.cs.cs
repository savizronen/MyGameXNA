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
    //public enum GiftsColids {ColidPresent,ColidLife};//התנגשות עם התקפות 
    //public enum atc { IceAttack, FireAttack};//התנגשות עם התקפות 
    class Colides//to Bots (to single player)
    {
        private List<AnimatedObj> Attacks;
        private List<AnimatedObj> Teleports;
        private List<pixelCollision> CollisionWithBotAtc;
        private List<pixelCollision> CollisionBotWithGifts;
        private List<pixelCollision> CollisionBotWithTeleports;
        private AICharacter Bot;
        private Random random = new Random();
        Stopwatch stopWatch;
        TimeSpan ts;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="Attacks"></param>
        /// <param name="A"></param>
        public Colides(List<AnimatedObj> Attacks, List<AnimatedObj> Teleports, AICharacter A)
        {
            this.Attacks = Attacks;
            this.Teleports = Teleports;
            this.CollisionWithBotAtc = new List<pixelCollision>();
            this.CollisionBotWithGifts = new List<pixelCollision>();
            this.CollisionBotWithTeleports = new List<pixelCollision>();
            Bot = A;
            stopWatch = new Stopwatch();
            InitColids();
            Game1.CallUpdate += new SigUpdate(Update);
        }


        private void InitColids()
        {

            for (int i = 0; i < this.Attacks.Count; i++)
            {
                this.CollisionWithBotAtc.Add(new pixelCollision(this.Bot, this.Attacks[i])); 
            }

            CollisionBotWithGifts.Add(new pixelCollision(this.Bot, Game1.present.B_AnimatedObj));
            CollisionBotWithGifts.Add(new pixelCollision(this.Bot, Game1.GiftLife.B_AnimatedObj));

            for (int i = 0; i < this.Teleports.Count; i++)
            {
                this.CollisionBotWithTeleports.Add(new pixelCollision(this.Bot, Teleports[i]));
            }


        }

        public virtual void Update()
        {


            if ((this.CollisionBotWithTeleports[0].ISCollision || this.CollisionBotWithTeleports[2].ISCollision))
            
            {
                int randNumber = random.Next(1, 3);
                if (randNumber == 1)
                {
                    this.Bot.POS = new V2(this.Teleports[1].POS.X + 40, this.Teleports[1].POS.Y);
                    this.Bot.AiNumber = 1;
                    
                }
                else
                {
                    this.Bot.POS = new V2(this.Teleports[3].POS.X - 40, this.Teleports[3].POS.Y);
                    this.Bot.AiNumber = 1;
                }
            }


            if ((this.CollisionBotWithTeleports[1].ISCollision || this.CollisionBotWithTeleports[3].ISCollision))
            {
                int randNumber = random.Next(1, 3);
                if (randNumber == 1)
                {
                    this.Bot.POS = new V2(this.Teleports[0].POS.X + 40, this.Teleports[0].POS.Y);
                    this.Bot.AiNumber = 1;
                    if (Game1.GiftLife.B_AnimatedObj.POS.X > 813 && Game1.GiftLife.B_AnimatedObj.POS.X < 2130 && Game1.GiftLife.B_AnimatedObj.POS.Y <= 272)
                    {
                        this.Bot.AiNumber = 3;
                    }

                }
                else
                {
                    this.Bot.POS = new V2(this.Teleports[2].POS.X - 40, this.Teleports[2].POS.Y);
                    this.Bot.AiNumber = 1;
                    if (Game1.GiftLife.B_AnimatedObj.POS.X > 813 && Game1.GiftLife.B_AnimatedObj.POS.X < 2130 && Game1.GiftLife.B_AnimatedObj.POS.Y <= 272)
                    {
                        this.Bot.AiNumber = 3;
                    }
                   
                }
            }


            if (this.CollisionBotWithGifts[(int)GiftsColids.ColidPresent].ISCollision)
            {
                Game1.present.B_AnimatedObj.COLOR = C.Transparent;
                Game1.present.B_AnimatedObj.POS = new V2(0, 0);
                this.Bot.AiNumber = 1;
            }
            if (this.CollisionBotWithGifts[(int)GiftsColids.ColidLife].ISCollision)
            {
                Game1.GiftLife.B_AnimatedObj.COLOR = C.Transparent;
                Game1.GiftLife.B_AnimatedObj.POS = new V2(0, 0);
                this.Bot.THEBotLife.AddingLife(50);
                this.Bot.AiNumber = 1;
            }


            if (this.CollisionWithBotAtc[(int)atc.IceAttack].ISCollision)
            {
                Bot.change_state(States.IceMen);
                Bot.Repeat = false;
                Bot.HaveColidNotMove = true;
                Bot.THEBotLife.DecreaseLife(5);
                stopWatch.Start();
            }



            if (this.CollisionWithBotAtc[(int)atc.FireAttack].ISCollision)
            {
                Bot.change_state(States.FireMen);
                Bot.Repeat = false;
                Bot.HaveColidNotMove = true;
                Bot.THEBotLife.DecreaseLife(5);
                stopWatch.Start();

            }

            ts = stopWatch.Elapsed;
            if (ts.Seconds == 2)
            {
                Attacks[(int)atc.IceAttack].change_state(States.bullet2);
                Bot.HaveColidNotMove = false;
                Bot.Repeat = true;
                stopWatch.Reset();
                stopWatch.Start();
            }


            if (Bot.THEBotLife.GetBotLifeValue == 0)
            {
                Bot.COLOR = C.Transparent;
                Bot.HaveColidNotMove = true;
                
            }

        }













    }
}
