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
using System.IO;
#endregion



namespace MyGameXNA
{
    /// <summary>
    /// 
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        public static event SigUpdate CallUpdate;
        public static event SigDraw CallDraw;
        public static GraphicsDevice device;
        public static  Character mypic, mypic2;
        private  Drawing back, forground;
        public static List<AnimatedObj> teleports;
        public static SoundsEffectsGame SoundsEffectsG;
        public static int option;
        public static Main main;
        public static Camera cam;
        public static Camera cam2;
        bool LoadGame;
        public static Background map;
        public static pixelCollision present;
        public static pixelCollision GiftLife;
        protected static int screenWidth;
        protected static int screenHeight;
        public static SpriteFont FontPostiveXY;
        private Viewport DefaultView, ViewTop, ViewBottom;
        private int numberCam = 1;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            device = graphics.GraphicsDevice;
            Content.RootDirectory = "Content";
            option = 0;
            this.LoadGame = false;
        }


        protected override void Initialize()
        {
           Tools.init(Content);
  //  graphics.PreferredBackBufferWidth = 2042;//2042;2774
  //     graphics.PreferredBackBufferHeight = 706; //706;690
        graphics.IsFullScreen = true;
            graphics.ApplyChanges();
            Window.Title = "MyGameXNA";
            IsMouseVisible = true;
            base.Initialize();
        }


        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;
            screenWidth = device.PresentationParameters.BackBufferWidth;
            screenHeight = device.PresentationParameters.BackBufferHeight;
            SoundsEffectsG = new SoundsEffectsGame();
            ///Viewports
            ///
            DefaultView = GraphicsDevice.Viewport;
            ViewTop = new Viewport();
            ViewTop.X = 0;
            ViewTop.Y = 0;
            ViewTop.Width = screenWidth;
            ViewTop.Height = screenHeight / 2;
            //// Unterer Bereich
            ViewBottom = new Viewport();
            ViewBottom.X = 0;
            ViewBottom.Y = screenHeight / 2;
            ViewBottom.Width = (int)(Game1.screenWidth );
            ViewBottom.Height = screenHeight / 2;
            cam = new Camera();
            main = new Main();
            teleports = new List<AnimatedObj>();

        }

        /// <summary>
        /// Two player split screen
        /// </summary>
        public void TwoPlayers()
        {
            Game1.main.BackMain.COLOR = C.Transparent;
            Rectangle screenRectangle = new Rectangle(0, 0, (int)((Game1.screenWidth) *1.488f), Game1.screenHeight);
            back = new Drawing(Content.Load<T2>("MapsGAME/multiPlayerMapMapBack"),
                                    new V2(0, 0),
                                    screenRectangle,
                                    C.White,
                                    0,
                                    V2.Zero,
                                    new V2(1, 1),
                                    SE.None,
                                    0);
            forground = new Drawing(Content.Load<T2>("MapsGAME/multiPlayerMapMapFront"),
                        new V2(0, 0),
                        screenRectangle,
                        C.White,
                        0,
                        V2.Zero,
                        new V2(1, 1),
                        SE.None,
                        0);
            map = new Background(back, forground);
            Gifts gifts = new Gifts();
            mypic = new Character(new GamerKeys(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space, Keys.V, Keys.M, Keys.K ,Keys.N, Keys.B, Keys.L), "player1", States.stand, new V2(96, 116), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.7f), SE.None, 0,1);
            present = new pixelCollision(mypic, gifts.PRESENT);
            GiftLife = new pixelCollision(mypic, gifts.LIFE);


            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(3, 118), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));
            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(3, 442), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));
            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(1911, 118), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));
            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(1911, 442), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));


            mypic2 = new Character(new GamerKeys(Keys.W, Keys.S, Keys.A, Keys.D, Keys.Tab,Keys.CapsLock, Keys.LeftShift, Keys.D3, Keys.D1, Keys.D2, Keys.D4), "player1", States.stand, new V2(96, 116), new Rectangle(160, 170, 74, 83), C.Cyan, 0, V2.Zero, new V2(0.7f), SE.None, 0, 2);
            MultiPlayerThePlayerColid a = new MultiPlayerThePlayerColid(mypic.characterAttacksToBotColid, teleports, mypic2);
            MultiPlayerThePlayerColid b = new MultiPlayerThePlayerColid(mypic2.characterAttacksToBotColid, teleports, mypic);

            cam2 = new Camera();
            VectorsXNA v1 = new VectorsXNA(GraphicsDevice, mypic, 5f);
            VectorsXNA v2 = new VectorsXNA(GraphicsDevice, mypic2, 5f);
   
        }

        /// <summary>
        /// SinglePlayer full screen the player Against Bots (Survival)
        /// </summary>
        public void SinglePlayer()
        {
            FontPostiveXY = Content.Load<SpriteFont>("Fonts/MyFont");
            option = 1;
            SoundsEffectsG = new SoundsEffectsGame(); 
            Rectangle screenRectangle = new Rectangle(0, 0, (int)((Game1.screenWidth) * 2.305f), Game1.screenHeight);
            back = new Drawing(Content.Load<T2>("MapsGAME/newBitmap"),
                                    new V2(0, 0),
                                    screenRectangle,
                                    C.White,
                                    0,
                                    V2.Zero,
                                    new V2(1, 1),
                                    SE.None,
                                    0);
            forground = new Drawing(Content.Load<T2>("MapsGAME/newMap"),
                        new V2(0, 0),
                        screenRectangle,
                        C.White,
                        0,
                        V2.Zero,
                        new V2(1, 1),
                        SE.None,
                        0);
            map = new Background(back, forground);
            Gifts gifts = new Gifts();
            mypic = new Character(new GamerKeys(Keys.Up, Keys.Down, Keys.Left, Keys.Right, Keys.Space, Keys.A, Keys.S, Keys.D, Keys.Z, Keys.X, Keys.C), "player1", States.stand, new V2(174, 180), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0,1);
            present = new pixelCollision(mypic, gifts.PRESENT);
            GiftLife = new pixelCollision(mypic, gifts.LIFE);

            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(22, 180), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));
            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(22, 682), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));
            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(2925, 180), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));
            teleports.Add(new AnimatedObj("teleport", States.telephortWork, new V2(2925, 682), new Rectangle(), C.White, 0, V2.Zero, new V2(0.5f), SE.None, 0));


  
            //התמונות
            T2 Frame = Content.Load<T2>("MadCharacterLife/Frame");
            T2 barLife = Content.Load<T2>("MadCharacterLife/Bar");
            T2 barEnargy = Content.Load<T2>("MadCharacterEnergy/Bar");

            SinglePlayerThePlayerColid Any = new SinglePlayerThePlayerColid(teleports, mypic);
          //  cam = new Camera();
            
           // אוייב חדש
            AICharacter bot = new AICharacter("player3", States.stand, new V2(2797, 181), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, mypic, mypic.characterAttacksToBotColid);
            Colides botcolide = new Colides(mypic.characterAttacksToBotColid, teleports, bot);


            //אוייב חדש
            AICharacter bot1 = new AICharacter("player3", States.stand, new V2(2119, 174), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, mypic, Game1.mypic.characterAttacksToBotColid);
            new Colides(Game1.mypic.characterAttacksToBotColid, teleports, bot1);

            //אוייב חדש
            AICharacter bot2 = new AICharacter("player3", States.stand, new V2(2000, 174), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, mypic, Game1.mypic.characterAttacksToBotColid);
            new Colides(Game1.mypic.characterAttacksToBotColid, teleports, bot2);


            //אוייב חדש
            AICharacter bot3 = new AICharacter("player3", States.stand, new V2(1900, 174), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, mypic, Game1.mypic.characterAttacksToBotColid);
            new Colides(Game1.mypic.characterAttacksToBotColid, teleports, bot3);

            //אוייב חדש
            AICharacter bot4 = new AICharacter("player3", States.stand, new V2(689, 500), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, mypic, Game1.mypic.characterAttacksToBotColid);
            new Colides(Game1.mypic.characterAttacksToBotColid, teleports, bot4);
            //אוייב חדש
            AICharacter bot5 = new AICharacter("player3", States.stand, new V2(1689, 552), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, mypic, Game1.mypic.characterAttacksToBotColid);
            new Colides(Game1.mypic.characterAttacksToBotColid, teleports, bot5);
            AICharacter bot6= new AICharacter("player3", States.stand, new V2(2793, 181), new Rectangle(160, 170, 74, 83), C.White, 0, V2.Zero, new V2(0.9f), SE.None, 0, mypic, Game1.mypic.characterAttacksToBotColid);
            new Colides(Game1.mypic.characterAttacksToBotColid, teleports, bot6);



           // bots a = new bots();
            VectorsXNA b = new VectorsXNA(GraphicsDevice, mypic, 5f);


        }

        protected override void UnloadContent()
        {


        }


        protected override void Update(GameTime gameTime)
        {

            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();
            Mouse.WindowHandle = Window.Handle;

            if (option == 0)
            {
                main.Update();
                option = main.OPTIONSELECT;

            }

            if (option == 1)
            {
                if(LoadGame==false)
                {
                SinglePlayer();
                LoadGame = true;
                }
                cam.update(mypic);
                if (CallUpdate != null)
                    CallUpdate();
            }
            if (option == 2)
            {
                if (LoadGame == false)
                {
                    TwoPlayers();
                    LoadGame = true;
                }
                cam.update(mypic);
                cam2.update(mypic2);
                if (CallUpdate != null)
                    CallUpdate();
            }


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {


            if (option == 0)
            {
                GraphicsDevice.Viewport = DefaultView;
              
                spriteBatch.Begin(SpriteSortMode.Deferred,
                                          BlendState.AlphaBlend,
                                          null,
                                          null,
                                          null, null);
                main.BackMain.Draw();
                main.MAINGAME.Draw();
   
                spriteBatch.End();

            }

            if (option == 1)
            {
                GraphicsDevice.Viewport = DefaultView;
                DrawSprites(cam);
            }

            if (option == 2)
            {
               
                GraphicsDevice.Viewport = ViewTop;
                numberCam = 1;
                DrawSprites(cam);
                GraphicsDevice.Viewport = ViewBottom;
               numberCam = 2;
                DrawSprites(cam2);
            }
            if (option == 4)
            {
                Exit();
            }
            base.Draw(gameTime);
            if (Keyboard.GetState().IsKeyDown(Keys.F8))
            {
                Game1.Screenshot(GraphicsDevice);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        

        public void DrawSprites(Camera CAMERA)
        {

      
            spriteBatch.Begin(SpriteSortMode.Deferred,
                                          BlendState.AlphaBlend,
                                          null,
                                          null,
                                          null, null, CAMERA.ViewMatrix);




            if (option == 2)
            {
                if (numberCam == 1)
                {
                    mypic2.SetMAdsColor = C.Transparent;
                    mypic.SetMAdsColor = C.White;
                }
                if (numberCam == 2)
                {
                    mypic.SetMAdsColor = C.Transparent;
                    mypic2.SetMAdsColor = C.White;

                }


            }
            if (Game1.CallDraw != null)
            {
                CallDraw();
            }

            //spriteBatch.DrawString(FontPostiveXY, "X=" + mypic.POS.X.ToString() + "  Y=" + mypic.POS.Y.ToString(), new V2(CAMERA.Position.X + 300, 0), C.White);

            spriteBatch.End();

        }

        /// <summary>
        /// Screenshot in key F8
        /// </summary>
        /// <param name="device"></param>
        public static void Screenshot(GraphicsDevice device)
        {
            byte[] screenData;

            screenData = new byte[device.PresentationParameters.BackBufferWidth * device.PresentationParameters.BackBufferHeight * 4];

            device.GetBackBufferData<byte>(screenData);

            Texture2D t2d = new Texture2D(device, device.PresentationParameters.BackBufferWidth, device.PresentationParameters.BackBufferHeight, false, device.PresentationParameters.BackBufferFormat);

            t2d.SetData<byte>(screenData);

            int i = 0;
            string name = "ScreenShot" + i.ToString() + ".png";
            while (File.Exists(name))
            {
                i += 1;
                name = "ScreenShot" + i.ToString() + ".png";

            }

            Stream st = new FileStream(name, FileMode.Create);

            t2d.SaveAsPng(st, t2d.Width, t2d.Height);

            st.Close();

            t2d.Dispose();
        }






    }
}
