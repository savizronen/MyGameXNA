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
   public class Camera
    {
        V2 position;
        Matrix viewMatrix;
        float scale=1f;

        public Matrix ViewMatrix
        {
            get
            {
                return this.viewMatrix;
            }
        }
        public int ScreenWidth
        {
            get
            {
                return GraphicsDeviceManager.DefaultBackBufferWidth;
            }
        }
        public int ScreenHeight
        {
            get
            {
                return GraphicsDeviceManager.DefaultBackBufferHeight;
            }
        }
  

        public void update(Drawing playerPosition)
        {
            position.X = playerPosition.POS.X - (ScreenWidth / 1.5f) / scale;

            if (position.X < 0)
                position.X = 0;

            if (position.X >= ScreenWidth *2.09f)
            {

                position.X = ScreenWidth * 2.09f;

            }
            //if (Game1.option == 2)
            //{
            //    if (position.X >= ScreenWidth * 1.488f)
            //    {

            //        position.X = ScreenWidth * 1.488f;

            //    }
            //}



            if (Game1.option == 2)
            {
                position.Y = playerPosition.POS.Y - (ScreenHeight / 1.4f) / scale;
                if (position.Y < 0)
                    position.Y = 0;

            }


            viewMatrix = Matrix.CreateTranslation(new Vector3(-position, 0));

        }
        public V2 Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

    }
}
//if (Game1.option == 2)
//{
//    position.Y = playerPosition.POS.Y - (ScreenHeight / 2f) / scale;
//    if (position.Y < 0)
//        position.Y = 0;

//}

//if (Keyboard.GetState().IsKeyDown(Keys.Z))
//{
//    scale += 0.01f;
//}
//if (Keyboard.GetState().IsKeyDown(Keys.X))
//{
//    scale -= 0.01f;
// position.X = playerPosition.POS.X - (ScreenWidth / 1.5f) / scale;