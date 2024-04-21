
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace Sandbox1;

public class MenuCursor : Sprite
{
    private List<Vector2> positions;
    private int maxPosition;
    private int currentRowPos;
    private SoundEffect cursorMoveSound;


    public MenuCursor(Texture2D texture, Vector2 position, List<Vector2> positions) : base(texture, position)
    {
        this.positions = positions;
        //this.offSet = offSet;

        //Set initial cursor position to the first from list of positions
        this.position = positions[0];

        //Set max number of positions equal to length of positions list
        maxPosition = positions.Count;
        currentRowPos = 1;  //or set to zero?
    }

    public void Load(ContentManager contentManager)
    {
        cursorMoveSound = contentManager.Load<SoundEffect>("Audio/SFX/blipSelect");
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
    }

    //Rn only supports up and down
    public void Move(string direction)
    {
        switch (direction)
        {
            case "down":
                currentRowPos++;
                Debug.WriteLine("cursorRowPos: " + currentRowPos);
                if (currentRowPos <= maxPosition)
                {
                    position = positions[currentRowPos-1];
                    cursorMoveSound.Play();                    
                }
                else
                {
                    currentRowPos--;
                }
                break;

            case "up":
                currentRowPos--;
                Debug.WriteLine("cursorRowPos: " + currentRowPos);

                if (currentRowPos > 0)
                {
                    position = positions[currentRowPos-1];
                    cursorMoveSound.Play(); //Little bit slow, wonder if we can speed up
                }
                else
                {
                    currentRowPos++;
                }
                break;
        }
    }

    public int GetCurrentRowPos()
    {
        return currentRowPos;
    }
}