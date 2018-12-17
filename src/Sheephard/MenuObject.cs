/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sheephard
{
    public enum MenuObjectType { Play, Credits, Players, Rounds }
    public class MenuObject
    {
        public string Text;
        public MenuObjectType Type;


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 position,
            bool small = false, string extra = "")
        {
            SpriteFont font = small ? FontHelper.Reference.Medium : FontHelper.Reference.XxLarge;

            spriteBatch.DrawString(font, Text + " " + extra, position,
                Type == MainMenu.Reference.SelectedItem ? Color.DarkRed : Color.White);
        }
    }
}
