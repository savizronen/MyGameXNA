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
    static class Tools
    {
        public static CM cm;
        public static KeyboardState ks, pks;
        public static void init(CM cm)
        {
            Tools.cm = cm;
            Game1.CallUpdate += new SigUpdate(Update);
          

        }
        public static SpriteFont LoadSpriteFont(string folder, string fileName)
        {
            SpriteFont Font = cm.Load<SpriteFont>(folder + "/" + fileName);
            return Font;
        }


    public static T Load<T>(T type, string folder, string fileName)
    {
        T Type = cm.Load<T>(folder + "/" + fileName);
        return Type;
    }

        public static void Update()
        {
            pks = ks;
            ks = Keyboard.GetState();

        }
    }
}
