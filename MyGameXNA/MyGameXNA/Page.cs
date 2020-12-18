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
    public class Page
    {
        #region Data
        public T2 tex;
        List<int> dots = new List<int>();
        public List<Rec> recs = new List<Rec>();
        public List<V2> orgs = new List<V2>();
        public List<V2> flipOrgs = new List<V2>();
        #endregion

        public Page(string name, string state)
        {

            tex = Tools.cm.Load<T2>(name + "/" + state);
            make_transperent();
            create_dots();
            create_origins();
            create_recs();
            create_flips();
        }
        private void create_dots()
        {
            C[] colors = new C[tex.Width];
            tex.GetData<C>(0, new Rec(0, tex.Height - 1, tex.Width, 1),
                                                        colors, 0, tex.Width);

            C startColor = colors[0];
            for (int i = 0; i < tex.Width; i++)
                if (colors[i] == startColor)
                    dots.Add(i);
        }
        private void create_origins()
        {
            for (int i = 1; i < dots.Count; i += 2)
                orgs.Add(new V2(dots[i] - dots[i - 1], tex.Height - 2));
        }

        private void create_recs()
        {
            for (int i = 2; i < dots.Count; i += 2)
                recs.Add(new Rec(dots[i - 2], 0, dots[i] - dots[i - 2]
                                                , tex.Height - 2));
        }

        private void create_flips()
        {
            for (int i = 0; i < recs.Count; i++)
                flipOrgs.Add(new V2(recs[i].Width - orgs[i].X
                                                    , tex.Height - 2));
        }
        private void make_transperent()
        {
            C[] colors = new C[this.tex.Width * this.tex.Height];
            tex.GetData<C>(colors);
            C temp = colors[0];
            for (int i = 0; i < colors.Length; i++)
            {
                if (temp == colors[i])
                {
                    colors[i] = C.Transparent;
                }
            }
            tex.SetData<C>(colors);
        }
    }

}
