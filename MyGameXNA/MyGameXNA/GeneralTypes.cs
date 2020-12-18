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
    public enum GiftsColids { ColidPresent, ColidLife };//התנגשות עם התקפות 
    public enum atc { IceAttack, FireAttack };//התנגשות עם התקפות 

    public enum States { telephortSteel, telephortWork, stand, Walk, jumpUP, jumpToSide, bullet, bulletexplodes, bullet2, bullet2explodes, Run, Boxing, magicPower, MagicSpeed, FireMen, IceMen, AttackBall,BoxingBot, GiftEnergy, GiftEnergyInTheAIr, GiftLife };
    
    public delegate void SigUpdate();
    public delegate void SigDraw();
   // interface IFocus
    //{
    //    V2 Pos { get; set; }
    //    F Rot { get; set; }

    //}
}


