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
    public class Drawing
    {
        #region Data
        protected T2 tex ;
        protected V2 org;
        protected Rectangle rec;
       // protected Rec? rec2;
        protected V2 pos;
        C color;
        F rot;
        V2 scale;
        protected SE se;
       // protected V2 Headtex;
        F layer;
        
        #endregion


        #region ctor
        public Drawing(T2 tex, V2 pos, Rectangle rec, C color,
                                F rot, V2 org, V2 scale, SE se, F layer)
        {
            this.tex = tex;
            this.pos = pos;
            this.rec = rec;
            this.color = color;
            this.rot = rot;
            this.org = org;
            this.scale = scale;
            this.se = se;
            this.layer = layer;
      //      this.Headtex = new V2(pos.X,this.tex.Height);
            Game1.CallDraw += new SigDraw(Draw);
        }
        //public Drawing(T2 tex, V2 pos, Rec? rec2, C color,
        //                F rot, V2 org, V2 scale, SE se, F layer)
        //{
        //    this.tex = tex;
        //    this.pos = pos;
        //    this.rec2 = rec2;
        //    this.color = color;
        //    this.rot = rot;
        //    this.org = org;
        //    this.scale = scale;
        //    this.se = se;
        //    this.layer = layer;
        //    Game1.CallDraw += new SigDraw(Draw);
        //}

        /// <summary>
        /// /////////
        /// </summary>
    //    int bla = 0;



        #endregion

        #region Drawing
        public virtual void Draw()
        {
            Game1.spriteBatch.Draw(tex, pos, rec, color, rot,
                                                    org, scale, se, layer);


        }

        #endregion
        #region Get/Set
        public T2 TEX
        {
            get
            {
                return this.tex;
            }
            set
            {
                this.tex = value;
            }
        }

        public F ROT
        {
            get
            {
                return this.rot;
            }
            set
            {
                this.rot = value;
            }
        }
        public V2 ORG
        {
            get
            {
                return this.org;
            }
            set
            {
                this.org = value;
            }
        }
        public Rectangle REC
        {
            get
            {
                return this.rec;
            }
            set
            {
                this.rec = value;
            }
        }
                public V2 POS
        {
            get
            {
                return this.pos;
            }
            set
            {
                this.pos= value;
            }
        }
           public C COLOR
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }
           public SE SE
           {
               get
               {
                   return this.se;
               }
               set
               {
                   this.se = value;
               }
           }
         










        
        #endregion




    }
}

