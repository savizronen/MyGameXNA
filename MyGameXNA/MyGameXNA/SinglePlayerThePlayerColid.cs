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
    class SinglePlayerThePlayerColid
    {
        private Character Player;
        private List<AnimatedObj> Teleports;
        private Random random = new Random();
       private List<pixelCollision> CollisionWithGifts;
       private List<pixelCollision> CollisionWithTeleports;

                           
           // this.CollisionBotWithTeleports = new List<pixelCollision>();

        public SinglePlayerThePlayerColid(List<AnimatedObj> Teleports, Character Player)
        {
            this.CollisionWithTeleports = new List<pixelCollision>();
            this.CollisionWithGifts = new List<pixelCollision>();
            this.Teleports = Teleports;
            this.Player = Player;
            InitColids();
            Game1.CallUpdate += new SigUpdate(Update);
        }


        private void InitColids()
        {
            CollisionWithGifts.Add(new pixelCollision(this.Player, Game1.present.B_AnimatedObj));
            CollisionWithGifts.Add(new pixelCollision(this.Player, Game1.GiftLife.B_AnimatedObj));


            for (int i = 0; i < this.Teleports.Count; i++)
            {
                this.CollisionWithTeleports.Add(new pixelCollision(this.Player, Teleports[i]));
            }


        }


        public virtual void Update()
        {
             //teleports
            if (this.Player.KEYS.upPressed() && (this.CollisionWithTeleports[0].ISCollision || this.CollisionWithTeleports[2].ISCollision))
          {
             Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.Teleport].Play();
              int randNumber = random.Next(1, 3);
              if (randNumber == 1)
              {
                  Player.POS = new V2(Game1.teleports[1].POS.X + 40, Game1.teleports[1].POS.Y);

              }
              else
              {
                  Player.POS = new V2(Game1.teleports[3].POS.X - 40, Game1.teleports[3].POS.Y);
              }
          }
            //Game1.colidesCharacter

            if (this.Player.KEYS.upPressed() && (this.CollisionWithTeleports[1].ISCollision || this.CollisionWithTeleports[3].ISCollision))
          {
              Game1.SoundsEffectsG.ListSoundsEffectsGame[(int)SoundsE.Teleport].Play();
              int randNumber = random.Next(1, 3);
              if (randNumber == 1)
              {
                 Player.POS = new V2(Game1.teleports[0].POS.X+40,Game1.teleports[0].POS.Y);

              }
              else
              {
                  Player.POS = new V2(Game1.teleports[2].POS.X - 40, Game1.teleports[2].POS.Y);
              }
          }

          if (Game1.present.ISCollision)
          {
            Game1.present.B_AnimatedObj.COLOR = C.Transparent;
            Game1.present.B_AnimatedObj.POS = new V2(0, 0);
           this.Player.MAdEnargy.changeMadCharacterAdding(50);

          }
          if (Game1.GiftLife.ISCollision)
          {
            Game1.GiftLife.B_AnimatedObj.COLOR = C.Transparent;
            Game1.GiftLife.B_AnimatedObj.POS = new V2(0, 0);
            this.Player.MADLife.changeMadCharacterAdding(50);

          }

        }












        








    }
}
