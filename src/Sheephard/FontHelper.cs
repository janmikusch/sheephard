/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */


using Microsoft.Xna.Framework.Graphics;

namespace Sheephard
{
    public class FontHelper
    {
        private static FontHelper _reference;
        public static FontHelper Reference => _reference ?? (_reference = new FontHelper());

        public SpriteFont XxLarge;
        public SpriteFont Medium;
        public SpriteFont Small;
        public SpriteFont XxxxLarge;

        public void LoadContent(Game1 g)
        {
            XxLarge = g.Content.Load<SpriteFont>("Fonts/XXLarge");
            XxxxLarge = g.Content.Load<SpriteFont>("Fonts/XXXXL");
            Medium = g.Content.Load<SpriteFont>("Fonts/Medium");
            Small = g.Content.Load<SpriteFont>("Fonts/Small");
        }
    }
}
