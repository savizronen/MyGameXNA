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
  public  class Character : AnimatedObj
    {
        #region data
        
        private Baseinput keys;
        private bool hasJumped;
        private AnimatedObj bullet, bullet2, magicPower, magicPower2;
        private List<Attack> CharacterAttacks;
        private List<AnimatedObj> CharacterAttacksToBotColid;
        bool AttackLorR = true;//true right false left
        float Attackx = 0, Attacky=0;
        bool IfCanFire=true;
        bool IfCanMagicSpeed = true;
        V2 velocity; 
        Stopwatch stopWatch;
        Stopwatch stopWatchSound;
        bool beat = true;
        TimeSpan ts;
        float speed=3;
        private Random random = new Random();
        private bool CharacterNotMove=false;
      private MadCharacterLife  MadEnargy;
      private MadCharacterLife MadLife;
      private int NumberCam;
      private V2 MiddleCharcter;

      #region bars life And enargy
        private Drawing playerOneFrameLife;
        private Drawing playerOneBarLife;
        private Drawing playerOneFrameEnargy;
        private Drawing playerOneBarEnargy;
      #endregion

        public Character(string name, States state, V2 pos, Rectangle rec, C color,
                                F rot, V2 org, V2 scale, SE se, F layer)
        : base(name, state, pos, rec, color,
                                    rot, org, scale, se, layer)
        {
            initBarslifeAndEnargy();
            initAttacks();
            this.hasJumped = false;
            stopWatch = new Stopwatch();
            stopWatchSound = new Stopwatch();
            this.CharacterAttacks = new List<Attack>();
            this.MiddleCharcter = new V2(pos.X, this.tex.Height / 2);
            Game1.CallUpdate += new SigUpdate(Update);
            Game1.CallDraw += new SigDraw(Draw);
        }

        public Character(Baseinput keys, string name, States state, V2 pos, Rectangle rec, C color,
                                F rot, V2 org, V2 scale, SE se, F layer,int NumberCam)
            : base(name, state, pos, rec, color,
                                     rot, org, scale, se, layer)
        {
            this.NumberCam = NumberCam;
            initBarslifeAndEnargy();
            initAttacks();
            this.keys = keys;
            this.hasJumped = false;
            stopWatch = new Stopwatch();
            stopWatchSound = new Stopwatch();
            this.CharacterAttacks = new List<Attack>();
            this.MiddleCharcter = new V2(pos.X, this.tex.Height / 2);
            Game1.CallUpdate += new SigUpdate(Update);
            Game1.CallDraw += new SigDraw(Draw);
        }


        public void initBarslifeAndEnargy()
        {

            //התמונות
            T2 Frame = Tools.cm.Load<T2>("MadCharacterLife/Frame");
            T2 barLife = Tools.cm.Load<T2>("MadCharacterLife/Bar");
            T2 barEnargy = Tools.cm.Load<T2>("MadCharacterEnergy/Bar");

  playerOneFrameLife = new Drawing(Frame, new V2(0, 0), new Rectangle(0, 0, Frame.Width, Frame.Height), C.White, 0, V2.Zero, new V2(0.35f, 0.35f), SE.None, 0);
             playerOneBarLife = new Drawing(barLife, new V2(0, 0), new Rectangle(0, 0, barLife.Width, barLife.Height), C.White, 0, V2.Zero, new V2(0.35f, 0.35f), SE.None, 0);
             MadLife = new MadCharacterLife(playerOneFrameLife, playerOneBarLife, 0,this.NumberCam);

             playerOneFrameEnargy = new Drawing(Frame, new V2(0, (barLife.Height * 0.35f) + 1), new Rectangle(0, 0, Frame.Width, Frame.Height), C.White, 0, V2.Zero, new V2(0.35f, 0.35f), SE.None, 0);
              playerOneBarEnargy = new Drawing(barEnargy, new V2(0, (barLife.Height * 0.35f) + 1), new Rectangle(0, 0, barEnargy.Width, barEnargy.Height), C.White, 0, V2.Zero, new V2(0.35f, 0.35f), SE.None, 0);
           MadEnargy = new MadCharacterLife(playerOneFrameEnargy, playerOneBarEnargy, 0, (int)(barLife.Height * 0.35f) + 1,this.NumberCam);
        }


     private void initAttacks ()
      {

          bullet = new AnimatedObj("player1", States.bullet, new V2(0, 0),
              new Rectangle(), C.Transparent, 0, V2.Zero, new V2(1), SE.None, 0);

           bullet2 = new AnimatedObj("player1", States.bullet2, new V2(0,0),
              new Rectangle(), C.Transparent, 0, V2.Zero, new V2(1), SE.None, 0);
      

         //more Speed
           magicPower = new AnimatedObj("player1", States.magicPower, new V2(0, 0),
            new Rectangle(), C.Transparent, 0, V2.Zero, new V2(1), SE.None, 0);

          //more Power Attck

           magicPower2 = new AnimatedObj("player1", States.MagicSpeed, new V2(0, 0),
            new Rectangle(), C.Transparent, 0, V2.Zero, new V2(1), SE.None, 0);
           CharacterAttacksToBotColid = new List<AnimatedObj>();
           CharacterAttacksToBotColid.Add(bullet2);
           CharacterAttacksToBotColid.Add(bullet);


      }

        #endregion

        #region Update
        public override void Update()
        {
            if (CharacterNotMove == false)
            {
                determined_state((int)(Game1.map.CheckIfCollisionWithGround(pos)));
                limitsINSide();
                limitsOutSide();
            }
            if (CharacterNotMove == true)
            {
                if (this.MadLife.IfTheMadeEnding()==false&&Game1.option==1)
                {
                    T2 GameOverTexture = Tools.cm.Load<T2>("Main/GameOver");
                    Drawing GameOver = new Drawing(GameOverTexture, new V2(Game1.cam.Position.X - 40, Game1.cam.Position.Y), new Rectangle(0, 0, GameOverTexture.Width, GameOverTexture.Height), C.White, 0, V2.Zero, new V2(1.3f, 1.3f), SE.None, 0);
                }
             
            }

          
           
        }

        private void determined_state(int y)
        {


            base.pos = new V2(pos.X, pos.Y + velocity.Y);
            if ((keys.rightPressed()))
            {
                base.pos.X += speed; //3f;
                velocity.X = 5f;
                if (flip)//בודק האם התבצע מהפך
                    place = 0;
                flip = false;
                base.change_state(States.Walk);
                base.se = SpriteEffects.None ;

                if (hasJumped != true)
                    base.pos = new V2(pos.X, y);



            }


            if ((keys.leftPressed()) )//
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

         


            if (keys.spacePressed() && hasJumped == false)
            {
                base.change_state(States.jumpUP);
                velocity.Y = -6f;
                hasJumped = true;
               // Game1.listSound[0].Play();
 
                
            }



            if (keys.rightPressed() && hasJumped == true)
            {
                base.change_state(States.jumpToSide);
                base.se = SpriteEffects.None;
            }

              if(keys.leftPressed() && hasJumped == true)
            {
                base.change_state(States.jumpToSide);
                base.se = SpriteEffects.FlipHorizontally;
            }




              if (this.MadLife.IfTheMadeEnding()==false&&Game1.option==1)
              {
      
                  Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.GameOver].Play();
                  CharacterNotMove = true;
              }
              stopWatchSound.Start();
            ts=stopWatchSound.Elapsed;
            if (this.MadLife.MadValue <= -530 && ts.Seconds == 0 && beat == true)
              {
 
                  Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.Heartbeat].Play();
                  stopWatchSound.Start();
                  beat = false;
              }
              if (ts.Seconds == 1)
              {
                  stopWatchSound.Reset();
                  stopWatchSound.Start();
                  beat = true;
              }





              if (keys.ZPressed() && IfCanFire == true && this.MadLife.IfTheMadeEnding())
            {
                
                SE SEft = new SE();
                IfCanFire = false;
                if (base.se == SpriteEffects.None)
                {
                    AttackLorR = true;
                    SEft = SE.None;
                }
                else
                {

                    AttackLorR = false;
                    SEft = SE.FlipHorizontally;

                }


                bullet2.COLOR = C.White;
                bullet2.SE = SEft;
                Attacky = (this.pos.Y - (this.tex.Height / 7));
                bullet2.POS = new V2(this.pos.X, (this.pos.Y - (this.tex.Height / 7)));
                 CharacterAttacks.Add(new Attack("bullet", 10f, bullet2));
                 this.MadEnargy.changeMadCharacterDecrease(30);
                base.change_state(States.AttackBall);

            }



              if (keys.XPressed() && IfCanFire == true && this.MadLife.IfTheMadeEnding())
            {


                SE SEft = new SE();
                IfCanFire = false;
                if (base.se == SpriteEffects.None)
                {
                    AttackLorR = true;
                    SEft = SE.None;
                }
                else
                {

                    AttackLorR = false;
                    SEft = SE.FlipHorizontally;

                }


                bullet.COLOR = C.White;
                bullet.SE = SEft;
                Attacky = (this.pos.Y - (this.tex.Height / 7));
                bullet.POS = new V2(this.pos.X, (this.pos.Y - (this.tex.Height / 7)));
                CharacterAttacks.Add(new Attack("bullet2", 10f, bullet));
                this.MadEnargy.changeMadCharacterDecrease(30);
                base.change_state(States.AttackBall);

            }


              if (keys.APressed() && IfCanMagicSpeed == true && this.MadLife.IfTheMadeEnding())
            {


                SE SEft = new SE();
                IfCanMagicSpeed = false;
                if (base.se == SpriteEffects.None)
                {
                    AttackLorR = true;
                    SEft = SE.None;
                }
                else
                {

                    AttackLorR = false;
                    SEft = SE.FlipHorizontally;

                }




                magicPower.COLOR = C.White;
                magicPower.SE =SEft;
                magicPower.POS=new V2(this.pos.X, (this.pos.Y - (this.tex.Height / 10)));
                CharacterAttacks.Add(new Attack("magicPower", 0f, magicPower, 2, 3, 20));
                this.MadEnargy.changeMadCharacterDecrease(50);
               this.speed = 4f;
           


            }
            if (keys.SPressed() && IfCanFire == true)
            {
              //  base.change_state(States.Boxing);



                SE SEft = new SE();
                IfCanFire = false;
                if (base.se == SpriteEffects.None)
                {
                    AttackLorR = true;
                    SEft = SE.None;
                }
                else
                {

                    AttackLorR = false;
                    SEft = SE.FlipHorizontally;

                }



                magicPower2.COLOR = C.White;
                magicPower2.SE = SEft;
                magicPower2.POS = new V2(this.pos.X, (this.pos.Y - (this.tex.Height / 7)));
                CharacterAttacks.Add(new Attack("magicPower2", 0f, magicPower2, 2, 3, 0));
                this.MadEnargy.changeMadCharacterDecrease(50);
            }




            if(CharacterAttacks.Count>0)
            {
                for (int z = 0; z < CharacterAttacks.Count; z++)
                {

                    if (CharacterAttacks[z].Effect == 2 || CharacterAttacks[z].Effect == 1)
                    {
                        Attackx = CharacterAttacks[z].AnimateBulletImage.POS.X;

                        if (AttackLorR == true)
                            Attackx += CharacterAttacks[z].BULLETSPEED;

                        if (AttackLorR == false)
                            Attackx -= CharacterAttacks[z].BULLETSPEED;


                        CharacterAttacks[z].AnimateBulletImage.POS = new V2(Attackx, Attacky);

                    }



                    if (CharacterAttacks[z].Effect == 3)
                    {
                        CharacterAttacks[z].AnimateBulletImage.POS = new V2(pos.X, pos.Y);
                    }

                }

            }


                if (CharacterAttacks.Count > 0)
                {


                    for (int i = 0; i < CharacterAttacks.Count; i++)
                    {
                        switch (CharacterAttacks[i].ATTACKNAME)
                        {
                            //magicPower //MagicSpeed
                            case "magicPower":
                            if (CharacterAttacks[i].GettimerTimeInseconds == CharacterAttacks[i].TIMEATTACK)
                            {
                             //   speed = 4;
                                CharacterAttacks[i].AnimateBulletImage.COLOR = C.Transparent;
                                //iskeyS = true;

                            }
                            if (CharacterAttacks[i].GettimerTimeInseconds == CharacterAttacks[i].EndACTIONTIME)
                            {
                                speed = 3;
                                CharacterAttacks.RemoveAt(i);
                                IfCanMagicSpeed = true;
                                

                            }


                            break;
   
                            default:
                            if (CharacterAttacks[i].GettimerTimeInseconds == CharacterAttacks[i].TIMEATTACK)
                            {
                                CharacterAttacks[i].AnimateBulletImage.COLOR = C.Transparent;
                                CharacterAttacks.RemoveAt(i);
                                IfCanFire = true;

                            }
                            break;
                         
                        }
           

                    }


                  
                }


              

            //if(Game1.map.isWallClose(this.MiddleCharcter,this.se))
            //{

            //    if (this.SE == SpriteEffects.None && this.State == States.Walk)
            //                this.POS = new V2(this.POS.X - (this.Speed * 2), this.POS.Y);
            //            if (this.SE == SpriteEffects.FlipHorizontally)
            //                this.POS = new V2(this.POS.X + (this.Speed * 2), this.POS.Y);
            //}









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


            if (!keys.leftPressed() && !keys.rightPressed() && !keys.spacePressed() && !keys.ZPressed() && !keys.XPressed())
            {

                 base.change_state(States.stand);
    
            }

        }

        #endregion







        private void limitsOutSide()
        {
            if (pos.X >= Game1.map.GetlimitXRight())
            {

                base.pos = new V2(Game1.map.GetlimitXRight(), pos.Y);
            }
            if (pos.X <= Game1.map.GetlimitXLeft())
            {
                base.pos = new V2(Game1.map.GetlimitXLeft(),pos.Y);
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



        public float Speed
        {
            get
            {
                return this.speed;
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
        public List<AnimatedObj> characterAttacksToBotColid
        {
            get
            {
                return this.CharacterAttacksToBotColid;
            }


        }



        public MadCharacterLife MADLife
        {
            get
            {
                return this.MadLife;
            }
            set
            {
                this.MadLife = value;
            }

        }

        public MadCharacterLife  MAdEnargy
        {
            get
            {
                return this.MadEnargy;
            }
            set
            {
               this.MadEnargy = value;
            }

        }
        public C SetMAdsColor
        {

            set
            {
                this.playerOneBarLife.COLOR = value;
                this.playerOneFrameLife.COLOR = value;
                this.playerOneFrameEnargy.COLOR = value;
                this.playerOneBarEnargy.COLOR = value;
            }

        }



        public bool CHaracterNotMove
        {

            get
            {
                return this.CharacterNotMove;
            }
            set
            {
                this.CharacterNotMove = value;
            }
        }

        public Baseinput KEYS
        {

            get
            {
                return this.keys;
            }
            set
            {
                this.keys = value;
            }
        }


        }



    }

