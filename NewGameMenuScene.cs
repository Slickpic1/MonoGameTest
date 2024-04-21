using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Sandbox1;

public class NewGameMenuScene : MenuScene
{
    public NewGameMenuScene(ContentManager contentManager, GraphicsDeviceManager graphics, SceneManager sceneManager) : base(contentManager, graphics, sceneManager)
    {
        
    }

    public override void Load()
    {
        backroundTexture = contentManager.Load<Texture2D>("Menus/subMainMenu");
        base.Load();
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

}