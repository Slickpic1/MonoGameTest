using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Sandbox1;

public class MenuScene : IScene
{
    protected ContentManager contentManager;
    protected GraphicsDeviceManager graphics;
    protected SpriteString option;
    protected SpriteString title;
    protected SpriteFont titleFont;
    protected SpriteFont optionFont;
    protected MenuCursor menuCursor;
    protected Texture2D backroundTexture;
    protected KeyboardState prevKBState;
    protected SceneManager sceneManager;
    protected List<SpriteString> menuOptions;
    protected List<Vector2> cursorPositions = new();

    public MenuScene(ContentManager contentManager, GraphicsDeviceManager graphics, SceneManager sceneManager) : base()
    {
        this.contentManager = contentManager;
        this.graphics = graphics;
        this.sceneManager = sceneManager;
    }

    public virtual void Load()
    {

    }

    public virtual void Update(GameTime gameTime)
    {

    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(backroundTexture, new Rectangle(0,0,graphics.PreferredBackBufferWidth,graphics.PreferredBackBufferHeight), Color.White);
        title.DrawString(spriteBatch);
            
        //Draw our menu options
        foreach(SpriteString option in menuOptions)
        {
            //Note: this updates constantly, which might be annoying if not bad for computer
            option.DrawString(spriteBatch);                  
        }
        //Draw our selection cursor
        menuCursor.Draw(spriteBatch);
    }
}