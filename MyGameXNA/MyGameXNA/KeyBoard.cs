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
    class BotKeys : Baseinput
    {
        bool left, right, up, down, space, prevLeft, prevRight, prevUp, prevDown, prevSpace, A, S, D, Z, X, C, prevA, prevS, prevD, prevZ, prevX, prevC;
        public BotKeys(bool left, bool right, bool up, bool down, bool space, bool A, bool S, bool D, bool Z, bool X, bool C)
        {
            this.prevLeft = this.left;
            this.left = left;
            this.prevRight = this.right;
            this.right = right;
            this.prevUp = this.up;
            this.up = up;
            this.prevDown = this.down;
            this.down = down;
            this.prevSpace = this.space;
            this.space = space;
            this.prevA = this.A;
            this.A = A;
            this.prevS = this.S;
            this.S = S;
            this.prevD = this.D;
            this.D = D;
            this.prevZ = this.Z;
            this.Z = Z;
            this.prevX = this.X;
            this.X = X;
            this.prevC = this.C;
            this.C= C;






        }
        public override bool leftPressed()
        {
            return left;
        }

        public override bool rightPressed()
        {
            return right;
        }

        public override bool upPressed()
        {
            return up;
        }

        public override bool downPressed()
        {
            return down;
        }
        public override bool spacePressed()
        {
            return space;
        }
        public override bool APressed()
        {
            return A;
        }
        public override bool SPressed()
        {
            return S;
        }
        public override bool DPressed()
        {
            return D;
        }
        public override bool ZPressed()
        {
            return Z;
        }
        public override bool XPressed()
        {
            return X;
        }
        public override bool CPressed()
        {
            return C;
        }





        public override bool prevLeftPressed()
        {
            return prevLeft;
        }

        public override bool prevRightPressed()
        {
            return prevRight;
        }

        public override bool prevUpPressed()
        {
            return prevUp;
        }

        public override bool prevDownPressed()
        {
            return prevDown;
        }
        public override bool prevSpacePressed()
        {
            return prevSpace;
        }
        public override bool prevAPressed()
        {
            return prevA;
        }
        public override bool prevSPressed()
        {
            return prevS;
        }
        public override bool prevDPressed()
        {
            return prevD;
        }
        public override bool prevZPressed()
        {
            return prevZ;
        }
        public override bool prevXPressed()
        {
            return prevX;
        }
        public override bool prevCPressed()
        {
            return prevC;
        }




    }
    class GamerKeys : Baseinput
    {
        Keys left, right, up, down, space, prevLeft, prevRight, prevUp, prevDown, prevSpace, A, S, D,Z,X,C, prevA, prevS, prevD,prevZ,prevX,prevC;
        public GamerKeys(Keys up, Keys down, Keys left, Keys right, Keys space,Keys A,Keys S,Keys D,Keys Z,Keys X,Keys C)
        {
            this.prevLeft = this.left;
            this.left = left;
            this.prevRight = this.right;
            this.right = right;
            this.prevUp = this.up;
            this.up = up;
            this.prevDown = this.down;
            this.down = down;
            this.prevSpace = this.space;
            this.space = space;
            this.prevA = this.A;
            this.A = A;
            this.prevS = this.S;
            this.S = S;
            this.prevD = this.D;
            this.D = D;
            this.prevZ = this.Z;
            this.Z = Z;
            this.prevX = this.X;
            this.X = X;
            this.prevC = this.C;
            this.C = C;



        }

        public override bool leftPressed()
        {

            return Tools.ks.IsKeyDown(left);

        }

        public override bool rightPressed()
        {
            return Tools.ks.IsKeyDown(right);
        }

        public override bool upPressed()
        {
            return Tools.ks.IsKeyDown(up);
        }

        public override bool downPressed()
        {
            return Tools.ks.IsKeyDown(down);
        }

        public override bool spacePressed()
        {
            return Tools.ks.IsKeyDown(space);
        }
        public override bool APressed()
        {
            return Tools.ks.IsKeyDown(A);
        }
        public override bool SPressed()
        {
            return Tools.ks.IsKeyDown(S);
        }
        public override bool DPressed()
        {
            return Tools.ks.IsKeyDown(D);
        }
        public override bool ZPressed()
        {
            return Tools.ks.IsKeyDown(Z);
        }

        public override bool XPressed()
        {
            return Tools.ks.IsKeyDown(X);
        }
        public override bool CPressed()
        {
            return Tools.ks.IsKeyDown(C);
        }


        public override bool prevLeftPressed()
        {
            return Tools.ks.IsKeyDown(prevLeft);
        }

        public override bool prevRightPressed()
        {
            return Tools.ks.IsKeyDown(prevRight);
        }

        public override bool prevUpPressed()
        {
            return Tools.ks.IsKeyDown(prevUp);
        }

        public override bool prevDownPressed()
        {
            return Tools.ks.IsKeyDown(prevDown);
        }
        public override bool prevSpacePressed()
        {
            return Tools.ks.IsKeyDown(prevSpace);
        }

        public override bool prevAPressed()
        {
            return Tools.ks.IsKeyDown(prevA);
        }

        public override bool prevSPressed()
        {
            return Tools.ks.IsKeyDown(prevS);
        }
        public override bool prevDPressed()
        {
            return Tools.ks.IsKeyDown(prevD);
        }
        public override bool prevZPressed()
        {
            return Tools.ks.IsKeyDown(prevZ);
        }
        public override bool prevXPressed()
        {
            return Tools.ks.IsKeyDown(prevX);
        }
        public override bool prevCPressed()
        {
            return Tools.ks.IsKeyDown(prevC);
        }





        
    }
   public abstract class Baseinput
    {

        public abstract bool leftPressed();
        public abstract bool rightPressed();
        public abstract bool upPressed();
        public abstract bool downPressed();
        public abstract bool spacePressed();
        public abstract bool APressed();
        public abstract bool SPressed();
        public abstract bool DPressed();
        public abstract bool ZPressed();
        public abstract bool XPressed();
        public abstract bool CPressed();

        public abstract bool prevLeftPressed();
        public abstract bool prevRightPressed();
        public abstract bool prevUpPressed();
        public abstract bool prevDownPressed();
        public abstract bool prevSpacePressed();
        public abstract bool prevAPressed();
        public abstract bool prevSPressed();
        public abstract bool prevDPressed();
        public abstract bool prevZPressed();
        public abstract bool prevXPressed();
        public abstract bool prevCPressed();

    }
}
