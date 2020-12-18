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
    public class VectorsXNA
    {

        public static Texture2D pixel;
        private V2 leftRay, rightRay, CenterRay;
        private Character A;
        private float length;
        private float rot;
        private V2 meetPoint = V2.Zero;
        //   private V2 midelDraw;
        public VectorsXNA(GraphicsDevice gd, Character A, float length)
        {
            this.A = A;
            pixel = new Texture2D(gd, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            this.length = length;
           // Game1.CallDraw += new SigDraw(Draw);
            Game1.CallUpdate += new SigUpdate(Update);
        }






        public static void DrawLine(Vector2 start, float length, float rot)
        {
            Game1.spriteBatch.Draw(pixel, start, null, Color.White, rot, new Vector2(0.5f, 1), new Vector2(1, length), SpriteEffects.None, 0);
        }
        public virtual void Update()
        {
            //leftRay = V2.Transform(V2.UnitX,
            //Matrix.CreateRotationZ(-length + MathHelper.ToRadians(60)));

            //rightRay = V2.Transform(V2.UnitX,
            //    Matrix.CreateRotationZ(length + MathHelper.ToRadians(120)));
         //   if (Keyboard.GetState().IsKeyDown(Keys.Enter))
       //     Game1.map.CheckIfCollisionWithGround(Game1.mypic);
            





            if (this.A.SE == SpriteEffects.None)
            {
                CenterRay = V2.Transform(V2.UnitX,
Matrix.CreateRotationZ(length + MathHelper.ToRadians(-90)));
            }
            if (this.A.SE == SpriteEffects.FlipHorizontally)
            {
                CenterRay = V2.Transform(V2.UnitX,
Matrix.CreateRotationZ(length + MathHelper.ToRadians(90)));
            }

          //  CenterRay = Vector2.Normalize(CenterRay) * length;
          CenterRay.Normalize();
        //  float r=  CenterRay.Length();
           



            if (Game1.map.isWallClose(new V2((this.A.POS.X), (this.A.POS.Y - this.A.REC.Height / 2)), new V2(MathHelper.ToDegrees(CenterRay.X), MathHelper.ToDegrees(CenterRay.Y)), out this.meetPoint))
            {
                //float a, b;
                //a=  MathHelper.ToDegrees(CenterRay.X);
                //b = MathHelper.ToDegrees(CenterRay.Y);



                if (Game1.map.GetlimitXLeft() <= this.A.POS.X && this.A.POS.X <= Game1.map.GetlimitXRight())
                {
                    if (A is Character)
                    {


                        if (this.A.SE == SpriteEffects.None && this.A.State == States.Walk)
                            this.A.POS = new V2(this.A.POS.X - (this.A.Speed * 2), this.A.POS.Y);
                        if (this.A.SE == SpriteEffects.FlipHorizontally)
                            this.A.POS = new V2(this.A.POS.X + (this.A.Speed * 2), this.A.POS.Y);
                    }

                }
            }
            else
            {
                this.A.COLOR = C.White;
            }


        }

        //public virtual void Draw()
        //{
        //    if (this.A.SE == SpriteEffects.None)
        //    {

          
        //        DrawLine(new V2((this.A.POS.X), (this.A.POS.Y - this.A.REC.Height / 2)), length, MathHelper.ToRadians(90));





        //    }
        //    if (this.A.SE == SpriteEffects.FlipHorizontally)
        //    {

        //        DrawLine(new V2((this.A.POS.X), (this.A.POS.Y - this.A.REC.Height / 2)), length, MathHelper.ToRadians(-90));

        //    }
     //   }




    }
}
