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
  public  class AICharacter : AnimatedObj
    {
        protected bool hasJumped;
        protected Character Player;
        V2 velocity; //מהירות
        float speed = 5;
        //private Random random = new Random();
        Stopwatch stopWatch;
        Stopwatch AItimer;
        int AInumber=1;
        TimeSpan ts;
        private bool haveColidNotMove;
        private BotLife TheBotLife;
        private List<pixelCollision> colidesTeleportsAICharacters;


       public AICharacter(string name, States state, V2 pos, Rectangle rec, C color,
     F rot, V2 org, V2 scale, SE se, F layer, Character Player, List<AnimatedObj> CharacterAttacksToBot)
            : base(name, state, pos, rec, color, rot, org, scale, se, layer)
        {
            this.Player = Player;
            this.hasJumped = false;
            Game1.CallUpdate += new SigUpdate(Update);
            Game1.CallDraw += new SigDraw(Draw);
            this.TheBotLife = new BotLife();
            this.haveColidNotMove = false;
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }






        public virtual void Update()
        {

            if(this.haveColidNotMove!=true)
            determined_state((int)(Game1.map.CheckIfCollisionWithGround(base.pos)));

            limitsINSide();
            limitsOutSide();
        }



        private void determined_state(int y)
        {
            base.pos = new V2(pos.X, pos.Y + velocity.Y);


            if (AInumber==1)
            {
                #region  go after the player
                
                if (Game1.map.CheckIfTheBotNeedtoJump(Player.POS, this.pos, this.se) == true && hasJumped == false)
                {

                    base.change_state(States.jumpUP);
                    velocity.Y = -6f;
                    hasJumped = true;
                    //  Game1.SoundsEffectsG[]


                }
                if (Player.POS.X > this.pos.X)
                {
                    base.pos.X += speed; //3f;
                    velocity.X = 5f;
                    if (flip)//בודק האם התבצע מהפך
                        place = 0;
                    flip = false;
                    base.change_state(States.Walk);
                    base.se = SpriteEffects.None;

                    if (hasJumped != true)
                        base.pos = new V2(pos.X, y);



                }

                if (Player.POS.X < this.pos.X)//
                {
                    base.pos.X += -speed;//3f
                    velocity.X = -5f;
                    if (!flip)
                        place = 0;
                    flip = true;
                    base.change_state(States.Walk);
                    base.se = SpriteEffects.FlipHorizontally;

                    if (hasJumped != true)
                        base.pos = new V2(pos.X, y);

                }


                if (Math.Abs(Player.POS.X - pos.X) < 10 && Math.Abs(Player.POS.Y - pos.Y) <= 3)
                {

                    base.SE = SpriteEffects.FlipHorizontally;
                    base.change_state(States.BoxingBot);
                    Game1.mypic.MADLife.changeMadCharacterDecrease(4);


                }
                if (this.TheBotLife.GetBotLifeValue <= 50)
                {
                    AInumber = 3;
                }


                #endregion
            }

            if (Player.POS.X == this.pos.X && Player.POS.Y != this.pos.Y)
                {
                    AInumber = 2;
                
                }

            if (AInumber == 2)
            {
                #region  go the teleport
                if ((Math.Abs(Game1.teleports[1].POS.X - pos.X) < (Math.Abs(Game1.teleports[3].POS.X - pos.X))))
                {
                    if (Game1.teleports[1].POS.X < this.pos.X)
                    {
                        base.pos.X += -speed;//3f
                        velocity.X = -5f;
                        base.change_state(States.Walk);
                        base.se = SpriteEffects.FlipHorizontally;

                        if (hasJumped != true)
                            base.pos = new V2(pos.X, y);
                    }
                }
                else
                {
                    if (Game1.teleports[3].POS.X > this.pos.X)
                    {
                        base.pos.X += speed; //3f;
                        velocity.X = 5f;
                        base.change_state(States.Walk);
                        base.se = SpriteEffects.None;

                        if (hasJumped != true)
                            base.pos = new V2(pos.X, y);
                    }
                }
                #endregion
            }



            if (AInumber == 3)
            {
                #region  go after the GiftLife
                if (Game1.GiftLife.B_AnimatedObj.POS.X != 0)
                {//813 2130
                    if (Game1.GiftLife.B_AnimatedObj.POS.X > 813 && Game1.GiftLife.B_AnimatedObj.POS.X < 2130 && Game1.GiftLife.B_AnimatedObj.POS.Y <= 272)
                    {
                        AInumber = 2;
                    }
                    if (Game1.map.CheckIfTheBotNeedtoJump(Game1.present.B_AnimatedObj.POS, this.pos, this.se) == true && hasJumped == false)
                    {

                        base.change_state(States.jumpUP);
                        velocity.Y = -6f;
                        hasJumped = true;
                        //  Game1.SoundsEffectsG[]


                    }
                    if (Game1.GiftLife.B_AnimatedObj.POS.X > this.pos.X)
                    {
                        base.pos.X += speed; //3f;
                        velocity.X = 5f;
                        if (flip)//בודק האם התבצע מהפך
                            place = 0;
                        flip = false;
                        base.change_state(States.Walk);
                        base.se = SpriteEffects.None;

                        if (hasJumped != true)
                            base.pos = new V2(pos.X, y);



                    }

                    if (Game1.GiftLife.B_AnimatedObj.POS.X < this.pos.X)//
                    {
                        base.pos.X += -speed;//3f
                        velocity.X = -5f;
                        if (!flip)
                            place = 0;
                        flip = true;
                        base.change_state(States.Walk);
                        base.se = SpriteEffects.FlipHorizontally;

                        if (hasJumped != true)
                            base.pos = new V2(pos.X, y);

                    }


                    if (Math.Abs(Player.POS.X - pos.X) < 10 && Math.Abs(Player.POS.Y - pos.Y) <= 3)
                    {

                        base.SE = SpriteEffects.FlipHorizontally;
                        base.change_state(States.BoxingBot);
                        Game1.mypic.MADLife.changeMadCharacterDecrease(4);


                    }
                }
                else
                {
                    AInumber = 1;
                }
                #endregion

            }


            if (Player.POS.X > this.pos.X && hasJumped == true)
            {
                base.change_state(States.jumpToSide);
            }

            if (Player.POS.X < this.pos.X && hasJumped == true)
            {
                base.change_state(States.jumpToSide);
            }


            if (hasJumped == true)
            {
                float i = 1;
                velocity.Y += 0.15f * i;


            }


            if (pos.Y > y)
            {
                hasJumped = false;


            }


            if (hasJumped == false)
            {

                velocity.Y = 0f;

            }


        }


        private void limitsOutSide()
        {
            if (pos.X >= Game1.map.GetlimitXRight())
            {

                base.pos = new V2(Game1.map.GetlimitXRight(), pos.Y);
            }
            if (pos.X <= Game1.map.GetlimitXLeft())
            {
                base.pos = new V2(Game1.map.GetlimitXLeft(), pos.Y);
            }
        }
        private void limitsINSide()
        {


            for (int i = 0; i < Game1.map.GetFaillCharacter().Count; i++)
            {
                if (Game1.map.GetFaillCharacter()[i].x == pos.X && Game1.map.GetFaillCharacter()[i].y == pos.Y && (hasJumped != true))
                {
                    hasJumped = true;
                }

            }

        }


        public override void Draw()
        {

            base.Draw();
        }


        public bool HasJumped
        {
            get
            {
                return this.hasJumped;
            }
            set
            {
                this.hasJumped = value;
            }
        }
        public V2 Velocity
        {
            get
            {
                return this.velocity;
            }
            set
            {
              this.velocity = value;
            }
        }

        public bool HaveColidNotMove
        {
            get
            {
                return this.haveColidNotMove;
            }
            set
            {
                this.haveColidNotMove = value;
            }
        }


          
        public  BotLife THEBotLife
        {
            get
            {
                return this.TheBotLife;
            }
            set
            {
                this.TheBotLife = value;
            }
        }

        public int AiNumber
        {
            get
            {
                return this.AInumber;
            }
            set
            {
                this.AInumber = value;
            }
        }




    }
}
