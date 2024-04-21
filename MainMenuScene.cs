using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Sandbox1
{
    public class MainMenuScene : MenuScene
    {
        //Audio
        private Song mainMenuMusic;
        private Vector2 titlePosition;
        private Vector2 optionsPosition;
        

        public MainMenuScene(ContentManager contentManager, GraphicsDeviceManager graphics, SceneManager sceneManager) : base(contentManager, graphics, sceneManager)
        {
            //Initialize our menu sizeing and position (need to better adjust)
            titlePosition.X = graphics.PreferredBackBufferWidth/3 - 25;
            titlePosition.Y = graphics.PreferredBackBufferHeight/5;

            optionsPosition.X = graphics.PreferredBackBufferWidth/3 + 50;
            optionsPosition.Y = graphics.PreferredBackBufferHeight/3;
        }

        public override void Load()
        {
            //Load our content specifically for the main menu
            backroundTexture = contentManager.Load<Texture2D>("Menus/MainMenuBackground");
            titleFont = contentManager.Load<SpriteFont>("Fonts/ancientModern64");
            optionFont = contentManager.Load<SpriteFont>("Fonts/goblinAppears12");

            title = new SpriteString(titleFont,"Adventure Qwest!", titlePosition,Color.Black);

            //Load Music
            mainMenuMusic = contentManager.Load<Song>("Audio/Music/mainMenuMusic");
            
            //Load our menu options
            LoadOptions();

            //Load Cursor
            LoadCursor();

            //Play main menu music upon loading
            //MediaPlayer.Play(mainMenuMusic);
        }

        //Maybe rename, also can we move this elsewhere?
        public void LoadOptions()
        {
            //Maybe abstract this?
            menuOptions = new List<SpriteString>
            {
            new SpriteString(optionFont,"New Game",optionsPosition,Color.Black),
            new SpriteString(optionFont,"Load Game",optionsPosition,Color.Black),
            new SpriteString(optionFont,"Settings",optionsPosition,Color.Black),
            new SpriteString(optionFont,"Quit",optionsPosition,Color.Black)
            };

            //Custom displace y pos of each option for the menu (update for better positioning)
            int count = 0;
            foreach(SpriteString option in menuOptions)
            {
                option.SetYPos((int)option.position.Y + (25 * count));  
                count++;                 
            }
        }

        public void LoadCursor()
        { 
            //Make list of cursor positions
            foreach (SpriteString option in menuOptions)
            {
                cursorPositions.Add(
                    new Vector2(
                        option.position.X + option.StringLength() + 5, //need to better adjust
                        option.position.Y - menuOptions[1].GetStringHeight() * 1.5f));
            }

            //Load our cursor at initial position
            menuCursor = new MenuCursor(contentManager.Load<Texture2D>("Menus/selectorSword"),Vector2.Zero,cursorPositions);
            menuCursor.Load(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState currentKBState = Keyboard.GetState();
            
            if (currentKBState.IsKeyDown(Keys.Down) && !prevKBState.IsKeyDown(Keys.Down))
            {   
                menuCursor.Move("down");
            }

            if (currentKBState.IsKeyDown(Keys.Up) && !prevKBState.IsKeyDown(Keys.Up))
            {   
                menuCursor.Move("up");
            }

            //Check to see if keyPressed is enter (might abstract later)
            if (currentKBState.IsKeyDown(Keys.Enter) && prevKBState.IsKeyDown(Keys.Enter))
            {
                switch(menuCursor.GetCurrentRowPos())
                {
                    //New Game
                    case 1:
                        sceneManager.AddScene(new NewGameMenuScene(contentManager, graphics, sceneManager));
                        break;

                    //Load Game
                    case 2:
                        break;
                    
                    //Options
                    case 3:
                        break;

                    //Quit
                    default:
                        Program.game.Exit(); //Double check this doesn't break anything
                        break;
                }
            }
            
            prevKBState = currentKBState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}