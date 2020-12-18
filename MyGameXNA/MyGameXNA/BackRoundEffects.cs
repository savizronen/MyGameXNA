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
  public  class BackRoundEffects
    {
        private List<Drawing> DrawingEffects;
        private List<AnimatedObj> AnimatedEffects;


        public void Init()
        {
            DrawingEffects = new List<Drawing>();
            AnimatedEffects = new List<AnimatedObj>();
        }
        public BackRoundEffects(Drawing Draw)
        {
            this.DrawingEffects.Add(Draw);
            Game1.CallUpdate += new SigUpdate(Update);
         //   Game1.CallDraw += new SigDraw(Draw);
        }
        public BackRoundEffects(AnimatedObj Animate)
        {
            this.AnimatedEffects.Add(Animate);
            Game1.CallUpdate += new SigUpdate(Update);
           // Game1.CallDraw += new SigDraw(Draw);
        }
        public BackRoundEffects(List<Drawing> Draws, List<AnimatedObj> Animates)
        {
            this.DrawingEffects = Draws;
            this.AnimatedEffects = Animates;
            Game1.CallUpdate += new SigUpdate(Update);
       //     Game1.CallDraw += new SigDraw(Draw);
        }

        public virtual void Update()
        {
            for (int i = 0; i < DrawingEffects.Count; i++)
            {
                DrawingEffects[i].POS = new V2(DrawingEffects[i].POS.X + 5, DrawingEffects[i].POS.Y);
            }
            for (int z = 0; z < AnimatedEffects.Count; z++)
            {
                DrawingEffects[z].POS = new V2(DrawingEffects[z].POS.X + 5, DrawingEffects[z].POS.Y);
            }
        }














    }
}
