using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Sandbox1
{
    public class MainMenuScene : IScene
    {
        private ContentManager contentManager;
        
        //Fonts
        private SpriteFont mainFont;
        private SpriteFont subFont;

        //Sprites
        private Sprite menuCursor;

        //Audio
        private Song mainMenuMusic;
        private SoundEffect cursorMoveSound;


        private Texture2D mainMenu;
        private Vector2 titlePosition;
        private Vector2 optionsPosition;
        private List<Vector2> cursorPositions = new();
        private GraphicsDeviceManager graphics;
        private List<FontSprite> menuOptions;
        private int numRows;
        private int cursorRowPos = 1;
        private KeyboardState prevKBState;

        public MainMenuScene(ContentManager contentManager, GraphicsDeviceManager graphics)
        {
            this.contentManager = contentManager;
            this.graphics = graphics;

            //Initialize our menu sizeing and position
            titlePosition.X = graphics.PreferredBackBufferWidth/3 - 25;
            titlePosition.Y = graphics.PreferredBackBufferHeight/5;

            optionsPosition.X = graphics.PreferredBackBufferWidth/3 + 50;
            optionsPosition.Y = graphics.PreferredBackBufferHeight/3;
        }

        public void Load()
        {
            //Load our content specifically for the main menu
            mainMenu = contentManager.Load<Texture2D>("Menus/MainMenuBackground");
            mainFont = contentManager.Load<SpriteFont>("Fonts/ancientModern");
            subFont = contentManager.Load<SpriteFont>("Fonts/goblinAppears");

            //Load Music
            mainMenuMusic = contentManager.Load<Song>("Audio/Music/mainMenuMusic");

            //Load Sfx
            cursorMoveSound = contentManager.Load<SoundEffect>("Audio/SFX/blipSelect");
            
            //Load our menu options
            LoadOptions();

            //Load Cursor
            LoadCursor();

            //Play main menu music upon loading
            MediaPlayer.Play(mainMenuMusic);

            numRows = menuOptions.Count;
            Debug.WriteLine("numRows = " + numRows);
        }

        //Maybe rename, also can we move this elsewhere?
        public void LoadOptions()
        {
            //Maybe abstract this?
            menuOptions = new List<FontSprite>
            {
            new FontSprite(subFont,"New Game",optionsPosition,Color.Black),
            new FontSprite(subFont,"Load Game",optionsPosition,Color.Black),
            new FontSprite(subFont,"Quit",optionsPosition,Color.Black)
            };

            //Custom displace y pos of each option for the menu
            int count = 0;
            foreach(FontSprite option in menuOptions)
            {
                option.SetYPos((int)option.position.Y + (25 * count));  
                count++;                 
            }
        }

        public void LoadCursor()
        {
            //Load our cursor at initial position
            menuCursor = new Sprite(contentManager.Load<Texture2D>("Menus/selectorSword"),Vector2.Zero); 

            //Modify our cursors y position (see if this cant be scaled better)
            menuCursor.position.Y -= menuOptions[1].GetStringHeight() * 1.5f;

            //Make list of cursor positions
            foreach (FontSprite option in menuOptions)
            {
                cursorPositions.Add(
                    new Vector2(
                        option.position.X + option.StringLength() + 5, 
                        option.position.Y - menuOptions[1].GetStringHeight() * 1.5f));
            }

            //Test to see if this works
            menuCursor.position = cursorPositions[0];
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState currentKBState = Keyboard.GetState();
            
            if (currentKBState.IsKeyDown(Keys.Down) && !prevKBState.IsKeyDown(Keys.Down))
            {   
                cursorRowPos++;
                Debug.WriteLine("cursorRowPos: " + cursorRowPos);

                if (cursorRowPos <= numRows)
                {
                    menuCursor.position = cursorPositions[cursorRowPos-1];
                    cursorMoveSound.Play();                    
                }
                else
                {
                    cursorRowPos--;
                }
            }

            if (currentKBState.IsKeyDown(Keys.Up) && !prevKBState.IsKeyDown(Keys.Up))
            {   
                cursorRowPos--;
                Debug.WriteLine("cursorRowPos: " + cursorRowPos);

                if (cursorRowPos > 0)
                {
                    menuCursor.position = cursorPositions[cursorRowPos-1];
                    cursorMoveSound.Play(); //Little bit slow, wonder if we can speed up
                }
                else
                {
                    cursorRowPos++;
                }
            }

            //Check to see if keyPressed is enter (might abstract later)
            if (currentKBState.IsKeyDown(Keys.Enter) && prevKBState.IsKeyDown(Keys.Enter))
            {
                switch(cursorRowPos)
                {
                    //New Game
                    case 1:
                        break;

                    //Load Game
                    case 2:
                        break;

                    //Quit
                    case 3:
                        Program.game.Exit(); //Double check this doesn't break anything
                        break;
                }
            }
            
            prevKBState = currentKBState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {   //Maybe add an "if" statement here to exlude anytime greater than one (but make sure to reset after some time so not break)
            //Draw our main menu Specific Content
            spriteBatch.Draw(mainMenu, new Rectangle(0,0,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.DrawString(  //Need to fix this to not be static
                mainFont, 
                "Adventure Qwest!", 
                titlePosition, 
                Color.Black
                );  
            
            //Draw our menu options
            foreach(FontSprite option in menuOptions)
            {
                //Note: this updates constantly, which might be annoying if not bad for computer
                option.DrawString(spriteBatch);                  
            }

            //Draw our selection cursor
            menuCursor.Draw(spriteBatch);
        }

    }
}