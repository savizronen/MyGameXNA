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
    public class Background 
    {
        #region Data
        private int[] heights1;
        private int[] heights2;
        private List<PointPos> FaillCharacter;
        private List<PointPos> BlockCharacter;
        private List<PointPos> UpPoints;
        private Drawing backgroundTexture;
        private Drawing foregroundTexture;
        private int limitXRight, limitXLeft;
        private List<PointPos> limits;//TO FALL DOWN

        private GroundType[,] grounds;

        #endregion
        #region ctor
        public Background(Drawing backgroundTexture, Drawing foregroundTexture)
        {
            this.backgroundTexture = backgroundTexture;
            this.foregroundTexture = foregroundTexture;
            this.limitXLeft = 0;
            this.limitXRight = this.backgroundTexture.TEX.Width - 4;
            this.FaillCharacter = new List<PointPos>();
            this.BlockCharacter = new List<PointPos>();
            this.UpPoints = new List<PointPos>();
            createHeights();
            initMatrixMap();
            
        }
        #endregion

        #region GroundType enum
        public enum GroundType
        {
            None,
            Wall,
            playerWall,
            UpPoint,
            faillpoint,
            ground
        }
        #endregion
        #region Create Matrix Map Colors
        private void initMatrixMap()
        {
            grounds = new GroundType[backgroundTexture.TEX.Width, backgroundTexture.TEX.Height];

            Color[] texColor = new Color[backgroundTexture.TEX.Width * backgroundTexture.TEX.Height];
            backgroundTexture.TEX.GetData<Color>(texColor);

            #region FILL GROUND ARRAY BY TEX
            for (int x = 0; x < backgroundTexture.TEX.Width; x++)
            {
                for (int y = 0; y < backgroundTexture.TEX.Height; y++)
                {
                    grounds[x, y] = GroundType.None; //default is None

                    if (texColor[x + y * backgroundTexture.TEX.Width] ==C.Gray)
                        grounds[x, y] = GroundType.Wall;

                    if (texColor[x + y * backgroundTexture.TEX.Width] == C.Yellow)
                        grounds[x, y] = GroundType.faillpoint;
                    if (texColor[x + y * backgroundTexture.TEX.Width] == C.CornflowerBlue)
                    {
                        grounds[x, y] = GroundType.playerWall;
                    }
                }
            }

            for (int z = 0; z < heights1.Length; z++)
           {
                grounds[z, heights1[z]] = GroundType.ground;
            }
            for (int y = 0; y < heights2.Length; y++)
            {
                grounds[y, heights2[y]] = GroundType.ground;
            }


            #endregion
        }
        #endregion
        #region Get GroundType
        public GroundType this[float x, float y]
        {
            get
            {
                if (x < 0 || x >= grounds.GetLength(0) || y < 0 || y >= grounds.GetLength(1))
                    return GroundType.None;
                return grounds[(int)(x), (int)(y)];
            }
        }
        #endregion
        #region Create the map that player can walk
        private void createHeights()
        {
            bool h1 = false, h2 = false;
            C[] mask;
            T2 maskpic = this.backgroundTexture.TEX;
            mask = new C[maskpic.Width * maskpic.Height];
            heights1 = new int[maskpic.Width];
            heights2 = new int[maskpic.Width];

            maskpic.GetData<C>(mask);
            C backGround = C.Black;
            for (int x = 0; x < maskpic.Width; x++)
            {
                h1 = false;
                h2 = false;

                for (int y = 0; y < maskpic.Height - 1; y++)
                {
                    int index = y * maskpic.Width + x;
                    int nextIndex = (y + 1) * maskpic.Width + x;

                    if (mask[index] == backGround && mask[nextIndex] == C.White && h1 == false)
                    {
                        heights1[x] = y;
                        h1 = true;
                    }
                    if (mask[index] == backGround && mask[nextIndex] == C.Red && h2 == false)
                    {
                        heights2[x] = y;
                        h2 = true;
                    }
                    if (mask[index] == backGround && mask[nextIndex] == C.Yellow)
                    {
                        FaillCharacter.Add(new PointPos(x, y));
                    }

                    if (mask[index] == C.Gray)
                    {
                        BlockCharacter.Add(new PointPos(x, y));
                    }
                    if (mask[index] == C.CornflowerBlue)
                        UpPoints.Add(new PointPos(x,y));

                }
            }

        }
        #endregion


        public bool isWallClose(V2 position, V2 lookTo, out V2 meetPoint)
        {
            meetPoint = position + lookTo;
            float maxlength = lookTo.Length();
            lookTo.Normalize();
            V2 rayPos = position;
            while (this[rayPos.X, rayPos.Y] != GroundType.playerWall)
            {
                if ((rayPos - position).Length() > maxlength)
                    return false;
                rayPos += lookTo;
            }
            meetPoint = rayPos;
            return true;
        }

 
        public bool CheckIfTheBotNeedtoJump(V2 PosPlayer, V2 PosBot, SpriteEffects BotEffects)
        {

         if (PosPlayer.Y < PosBot.Y)
            {
                if (BotEffects == SpriteEffects.None)
            {
                for (int X = (int)PosBot.X ; X < (int)PosBot.X + 90; X++)
                {
                    if (X <= 0 || X >= Game1.map.GetlimitXRight())
                    {
                        return false;
                    }
                    if (Game1.map.grounds[(int)(X), (int)PosBot.Y-10] == GroundType.Wall)
                    {
                        return true;
                    }
                }
            }
            
         if (BotEffects == SpriteEffects.FlipHorizontally)
            {
                for (int X = (int)PosBot.X; X> (int)PosBot.X-90; X--)
                {
                    if (X <= 0 || X >= Game1.map.GetlimitXRight())
                    {
                        return false;
                    }


                    if (Game1.map.grounds[(int)(X), (int)PosBot.Y - 10] == GroundType.Wall)
                    {
                        return true;
                    }
                }
 
            }
            }
            return false;
        }


        public int CheckIfCollisionWithGround(V2 Pos)
        {
  
                for (int y = (int)Pos.Y-50; y < backgroundTexture.TEX.Height; y++)
                {
                    if (Game1.map.grounds[(int)(Pos.X),y] == GroundType.ground)
                    return y;
                }
                int y1 = Game1.map.GetIndexHeights((int)Pos.X);
                int y2 = Game1.map.GetIndexHeights2((int)Pos.X);

                if ((Pos.Y-  y1) < (y2 - Pos.Y))
                {
                    return y1;

                }


                return y2;
        }




        #region Get/Set

        #region GetHeights
        public int[] GetHeights()
        {
            return this.heights1;
        }
        public int GetIndexHeights(int pos)
        {
            return this.heights1[pos];
        }
        public int[] GetHeights2()
        {
            return this.heights2;
        }
        public int GetIndexHeights2(int pos)
        {
            return this.heights2[pos];
        }
        public List<PointPos> GetFaillCharacter()
        {
            return this.FaillCharacter;
        }
        #endregion

        #region limits map
        /// <summary>
        /// limits map
        /// </summary>
        /// <returns></returns>
        public int GetlimitXLeft()
        {
            return this.limitXLeft;
        }
        public int GetlimitXRight()
        {
            return this.limitXRight;
        }
        public List<PointPos> GetlimitsInside()
        {
            return this.limits;
        }
        public List<PointPos> GetBlocksCharacter()
        {
            return this.BlockCharacter;
        }
        public List<PointPos> GetUpPoints()
        {
            return this.UpPoints;
        }



        #endregion

        #endregion


    }


}

