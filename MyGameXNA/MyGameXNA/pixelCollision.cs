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

  public class pixelCollision
    {

        private Rectangle player;
        private Rectangle enemy;
        private AnimatedObj a;
        private AnimatedObj b;
        private bool isCollision;

         public pixelCollision(AnimatedObj a, AnimatedObj b)
        {
            this.a = a;
            this.b = b;
            this.isCollision = false;
            Game1.CallUpdate += new SigUpdate(Update);
           

        }

        #region Update
        public void Update()
        {
            player = new Rectangle((int)a.POS.X, (int)a.POS.Y, a.PAGE.tex.Width, a.PAGE.tex.Height);
            enemy = new Rectangle((int)b.POS.X, (int)b.POS.Y, b.PAGE.tex.Width, b.PAGE.tex.Height);
            if (Math.Sqrt(Math.Pow((player.X - enemy.X), 2) + Math.Pow((player.Y - enemy.Y), 2)) <30)
            {
                if (player.Intersects(enemy))
                {
                    if (CheckPixelCollision() == true)
                    {
                      //  a.COLOR = C.Red;
                        this.isCollision = true;
                        
                    }
      
                }
                else
                {
              //      a.COLOR = C.White;
                    this.isCollision = false;
                }

            }
            else
            {
                a.COLOR = C.White;
                this.isCollision = false;
            }
        }
        #endregion
        private bool CheckPixelCollision()
        {
            C[] colorData1 = new C[player.Height * player.Width];
            C[] colorData2 = new C[enemy.Height * enemy.Width];
            a.PAGE.tex.GetData<Color>(colorData1);
            b.PAGE.tex.GetData<Color>(colorData2);
            int top, bottom, left, right;
            top = Math.Max(player.Top, enemy.Top);
            bottom = Math.Min(player.Bottom, enemy.Bottom);
            left = Math.Max(player.Left, enemy.Left);
            right = Math.Min(player.Right, enemy.Right);

            for (int y = top; y < bottom; y++)
            {
                 for (int x = left; x < right; x++)
                 {
                     Color A = colorData1[(y - player.Top) * (player.Width) + (x - player.Left)];
                     Color B = colorData2[(y - enemy.Top) * (enemy.Width) + (x - enemy.Left)];
                   if (A.A != 0 && B.A != 0)
                   {
                    return true;
                   }
            }

            }
            return false;


        }
       

























        public bool ISCollision
        {
            get
            {
                return this.isCollision;
            }
            set
            {
                this.isCollision = value;
            }
        }


        public AnimatedObj A_AnimatedObj
        {
            get
            {
                return this.a;
            }

        }
        public AnimatedObj B_AnimatedObj
        {
            get
            {
                return this.b;
            }

        }








    }
}
