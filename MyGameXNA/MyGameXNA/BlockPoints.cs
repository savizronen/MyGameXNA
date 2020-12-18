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
    class BlockPoints
    {
        private int pointX;
        private int startpointY;
        private int endpointY;


        public BlockPoints(int pointX, int startpointY, int endpointY)
        {
            this.pointX = pointX;
            this.startpointY = startpointY;
            this.endpointY = endpointY;
        }



        public int PointX
        {
            get
            {
                return this.pointX;
            }
            set
            {
                this.pointX = value;
            }
        }
        public int StartpointY
        {
            get
            {
                return this.startpointY;
            }
            set
            {
                this.startpointY = value;
            }
        }
        public int EndpointY
        {
            get
            {
                return this.endpointY;
            }
            set
            {
                this.endpointY = value;
            }
        }










    }
}
