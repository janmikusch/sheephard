/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sheephard
{
    public class CreditScreen
    {
        public static CreditScreen Reference => _reference ?? (_reference = new CreditScreen());
        private static CreditScreen _reference;

        private CreditScreen() { }

        public void Update(GameTime gameTime)
        {
            if (InputManager.MenuBackPressed())
            {
                GameController.State = GameState.MainMenu;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            //Impressium
            spriteBatch.DrawString(FontHelper.Reference.Medium, "Jan Mikusch", new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(FontHelper.Reference.Medium, "FH Salzburg", new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(FontHelper.Reference.Medium, "Multimedia Technology", new Vector2(10, 90), Color.White);
            spriteBatch.DrawString(FontHelper.Reference.Medium, "Multimedia Projekt (1)", new Vector2(10, 130), Color.White);


            spriteBatch.DrawString(FontHelper.Reference.Medium,
                "Assets:",
                new Vector2(10, 200), Color.White);

            spriteBatch.DrawString(FontHelper.Reference.Small,
                "Sheeps: https://tokegameart.net/item/cartoon-sheep/",
                new Vector2(10, 240), Color.White);

            spriteBatch.DrawString(FontHelper.Reference.Small,
                "Music: https://opengameart.org/content/joy-ride",
                new Vector2(10, 280), Color.White);

            spriteBatch.DrawString(FontHelper.Reference.Small,
                "Sheep-Soundeffects: https://opengameart.org/content/sheep-sound-bleats-yo-frankie",
                new Vector2(10, 320), Color.White);

            spriteBatch.DrawString(FontHelper.Reference.Small,
                "Font: http://www.1001fonts.com/luckiest-guy-font.html#license",
                new Vector2(10, 360), Color.White);

        }
    }
}
