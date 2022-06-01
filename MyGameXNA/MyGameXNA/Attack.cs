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
   public class Attack
    {
       private string AttackName;
       private float bulletSpeed;
       private AnimatedObj bulletImage;
       private Stopwatch timer;
       private int EndActionTime;
       private int timeAttack;
       /// <summary>
       /// 1.standingSteel
       /// 2.Move
       /// 3.PlayerPosMov
       /// </summary>
       private int effect;
       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="AttackName"></param>
       /// <param name="bulletSpeed"></param>
       /// <param name="bullet"></param>
       public Attack(string AttackName, float bulletSpeed, AnimatedObj bullet)
       {
           this.AttackName = AttackName;
           this.bulletSpeed = bulletSpeed;
           this.bulletImage = bullet;
           timer = new Stopwatch();
           timer.Start();
           timeAttack = 2;
           this.effect = 2;
           this.EndActionTime = 0;
           
       }
       /// <summary>
       /// ctor
       /// </summary>
       /// <param name="AttackName"></param>
       /// <param name="bulletSpeed"></param>
       /// <param name="bullet"></param>
       /// <param name="TimeAttack"></param>
       /// <param name="effect"></param>
       /// <param name="EndActionTime"></param>
       public Attack(string AttackName, float bulletSpeed, AnimatedObj bullet, int TimeAttack, int effect, int EndActionTime)
       {
           this.AttackName = AttackName;
           this.bulletSpeed = bulletSpeed;
           this.bulletImage = bullet;
           timer = new Stopwatch();
           timer.Start();
           timeAttack = TimeAttack;
           this.effect = effect;
           this.EndActionTime = EndActionTime;

       }



       #region Get/Set


       public  string ATTACKNAME
       {
           get
           {
               return this.AttackName;
           }
           set
           {
               this.AttackName = value;
           }
       }


       public int EndACTIONTIME
       {
           get
           {
               return this.EndActionTime;
           }
           set
           {
               this.EndActionTime = value;
           }
       }

       public int Effect
       {
           get
           {
               return this.effect;
           }
           set
           {
               this.effect = value;
           }
       }

        public int TIMEATTACK
       {
           get
           {
               return this.timeAttack;
           }
           set
           {
               this.timeAttack = value;
           }
       }
        //public long TIMEACTION
        //{
        //    get
        //    {
        //        return this.ActionTime.ElapsedMilliseconds / 1000;
        //    }
        //}













       public long GettimerTimeInseconds 
       {
           get
           {
               return this.timer.ElapsedMilliseconds / 1000;
           }

       }

       public float BULLETSPEED
       {
           get
           {
               return this.bulletSpeed;
           }
           set
           {
               this.bulletSpeed = value;
           }
       }
       public AnimatedObj AnimateBulletImage
       {
           get
           {
               return this.bulletImage;
           }
           set
           {
               this.bulletImage = value;
           }
       }





       #endregion
    }
}
