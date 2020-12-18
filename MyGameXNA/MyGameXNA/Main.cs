

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
   public class Main:Game1
    {
        private MouseState mouseState;
        private bool Visble;
        private Drawing backMain;
        private Drawing MainGame;
        private string str;
        private int OptionSelect;
        public Main()
        {
            Visble = true;
            T2 BackMainPageTexture = Tools.cm.Load<T2>("Main/BackMain");
            backMain = new Drawing(BackMainPageTexture, new V2(0, -200), new Rectangle(0, 0, BackMainPageTexture.Width, BackMainPageTexture.Height), C.White, 0, V2.Zero, new V2(1.1f, 1.1f), SE.None, 0);
            T2 MainPageTexture = Tools.cm.Load<T2>("Main/Main");
            MainGame = new Drawing(MainPageTexture, new V2(graphics.PreferredBackBufferWidth / 3, 0), new Rectangle(0, 0, MainPageTexture.Width, MainPageTexture.Height), C.White, 0, V2.Zero, new V2(1, 1), SE.None, 0);
            str = "";
            OptionSelect = 0;
        }

        public virtual void Update()
        {
            mouseState = Mouse.GetState();

            //T2 t = new T2(GraphicsDevice, 1, 1);
            //t.SetData<C>(new[] { C.Yellow });

            Rectangle singlePlayer = new Rectangle((int)(graphics.PreferredBackBufferWidth / 3) + 31, 79, 658, 76);
            Rectangle TwoPlayers = new Rectangle((int)(graphics.PreferredBackBufferWidth / 3) + 75, 192, 576, 76);
            Rectangle Online = new Rectangle((int)(graphics.PreferredBackBufferWidth / 3) + 204, 311, 304 + 2, 78);
            Rectangle Exit = new Rectangle((int)(graphics.PreferredBackBufferWidth / 3) + 264,425, 177, 78);

        //    Game1.spriteBatch.Draw(t, singlePlayer, Color.White);
            //Game1.spriteBatch.Draw(t, TwoPlayers, Color.White);
            // Game1.spriteBatch.Draw(t, Online, Color.White);
         //   Game1.spriteBatch.Draw(t, Exit, Color.White);

            MainGame.POS = new V2(cam.Position.X + graphics.PreferredBackBufferWidth / 3, MainGame.POS.Y);




                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    if (singlePlayer.Contains(mouseState.X, mouseState.Y))
                    {
                        Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.tick].Play();
                        str = "on singlePlayer ";
                        MainGame.COLOR = C.Transparent;
                       this.OptionSelect = 1;
           

                    }
                    if (TwoPlayers.Contains(mouseState.X, mouseState.Y))
                    {
                        Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.tick].Play();
                        str = "on TwoPlayers";
                        MainGame.COLOR = C.Transparent;
                        this.OptionSelect = 2;
                    }
                    if (Online.Contains(mouseState.X, mouseState.Y))
                    {
                        Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.tick].Play();
                        str = "on Online ";
                        MainGame.COLOR = C.Transparent;
                        this.OptionSelect = 3;
                    }
                    if (Exit.Contains(mouseState.X, mouseState.Y))
                    {
                        Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.tick].Play();
                        str = "Exit";
                        MainGame.COLOR = C.Transparent;
                        this.OptionSelect = 4;
                    }







                }

            

        }


        public Drawing MAINGAME
        {
            get
            {
                return this.MainGame;
            }
            set
            {
                this.MainGame = value;
            }
        }

        public Drawing BackMain
        {
            get
            {
                return this.backMain;
            }
            set
            {
                this.backMain = value;
            }
        }

        public virtual void Draw()
        {
            //spriteBatch.DrawString(FontPostiveXY, "XMouse=" + mouseState.X + "  YMouse=" + mouseState.Y, new V2(cam.Position.X + 500, 0), C.White);
            //spriteBatch.DrawString(Game1.FontPostiveXY, str, new V2(100, 100), C.White);
        }





        #region Get/Set
        public bool VISBLE
        {
            get
            {
                return this.Visble;
            }
            set
            {
                this.Visble = value;
            }
        }

        public int OPTIONSELECT
        {
            get
            {
                return this.OptionSelect;
            }
            set
            {
                this.OptionSelect = value;
            }
        }

        #endregion





    }
}
