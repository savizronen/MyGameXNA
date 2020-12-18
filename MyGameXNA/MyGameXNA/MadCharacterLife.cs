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
 public class MadCharacterLife
    {
  
        public Drawing Frame;
        public Drawing Bar;
        public float MinValue;
        public float MadValue=0;
        private int Y;
        private int X;
        private int CamNumber;

     /// <summary>
        /// ctor MadCharacterLife
     /// </summary>
     /// <param name="Frame"></param>
     /// <param name="Bar"></param>
     /// <param name="MinValue"></param>
     /// <param name="Y"></param>
        public MadCharacterLife(Drawing Frame, Drawing Bar, float MinValue, int Y, int NumberCam)
        {
            this.Frame = Frame;
            this.Bar = Bar;
            this.Y = Y;
            this.X = 0;
            this.CamNumber = NumberCam;
            Game1.CallUpdate += new SigUpdate(Update);

        }
     /// <summary>
        /// ctor MadCharacterLife
     /// </summary>
     /// <param name="Frame"></param>
     /// <param name="Bar"></param>
     /// <param name="MinValue"></param>
     /// <param name="Y"></param>
     /// <param name="X"></param>
        public MadCharacterLife(Drawing Frame, Drawing Bar, float MinValue, int Y,int X,int NumberCam)
        {
            this.Frame = Frame;
            this.Bar = Bar;
            this.Y = Y;
            this.X = X;
            this.CamNumber = NumberCam;
            Game1.CallUpdate += new SigUpdate(Update);

        }


        public MadCharacterLife(Drawing Frame, Drawing Bar, float MinValue, int NumberCam)
        {
            this.Frame = Frame;
            this.Bar = Bar;
            this.Y = 0;
            this.X = 0;
            this.CamNumber = NumberCam;
            Game1.CallUpdate += new SigUpdate(Update);

        }

        public virtual void Update()
        {
            if (CamNumber == 1)
            {
                Frame.POS = new V2(Game1.cam.Position.X + this.X, Game1.cam.Position.Y + this.Y);
                Bar.POS = new V2(Game1.cam.Position.X + this.X, Game1.cam.Position.Y + this.Y);
            }
            if (CamNumber == 2)
            {
                Frame.POS = new V2(Game1.cam2.Position.X + this.X, Game1.cam2.Position.Y + this.Y);
                Bar.POS = new V2(Game1.cam2.Position.X + this.X, Game1.cam2.Position.Y + this.Y);
            }
        }




     /// <summary>
     /// Decrease life or enargy to  MadCharacter
     /// </summary>
     /// <param name="Enargy"></param>
        public void changeMadCharacterDecrease(int Enargy)
        {//Bar.POS.X>=
            if (this.MadValue >= -Bar.TEX.Width)
            {
                Bar.REC = new Rectangle(Bar.REC.X, Bar.REC.Y, Bar.REC.Width - Enargy, Bar.REC.Height);
                Bar.POS = new V2(BAR.POS.X - Enargy, BAR.POS.Y);
                MadValue -= Enargy;
            }
            if (this.MadValue < -Bar.TEX.Width)
            {

                MadValue = -Bar.TEX.Width;
            }




        }
     /// <summary>
        /// Adding life or enargy to  MadCharacter
     /// </summary>
     /// <param name="Enargy"></param>
        public void changeMadCharacterAdding(int Enargy)
        {
        //    if (Bar.POS.X !=0)
             if(0 > this.MadValue)
            {
                Bar.REC = new Rectangle(Bar.REC.X, Bar.REC.Y, Bar.REC.Width + Enargy, Bar.REC.Height);
                BAR.POS = new V2(BAR.POS.X - Enargy, BAR.POS.Y);
                MadValue += Enargy;

            }
            if (this.MadValue > 0)
            {

                MadValue = 0;
            }


        }
     /// <summary>
        ///  if the  MadCharacter is ending
     /// </summary>
     /// <returns></returns>
        public bool IfTheMadeEnding()
        {
            if (this.MadValue > -Bar.TEX.Width)
            {
                return true;
            }
            return false;
        }








        #region Get/Set
        public Drawing FRAME
        {
            get
            {
                return this.Frame;
            }
            set
            {
                this.Frame = value;
            }
        }
        public Drawing BAR
        {
            get
            {
                return this.Bar;
            }
            set
            {
                this.Bar = value;
            }
        }
        public float MINVALUE
        {
            get
            {
                return this.MinValue;
            }
            set
            {
                this.MinValue = value;
            }
        }
     

        #endregion











    }
}
