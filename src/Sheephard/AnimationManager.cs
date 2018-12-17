/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Sheephard
{
    public class AnimationManager
    {
        public enum AnimationType
        {
            Dying,
            Eating,
            Fearing,
            Hurt,
            Idle,
            IdleBlinking,
            Jerking,
            Running,
            Walking,
            Headbanging
        }


        // static ref
        private static AnimationManager _reference;
        public static AnimationManager Reference => _reference ?? (_reference = new AnimationManager());

        public const float FRAMETIME = 40f;

        public Dictionary<AnimationType, SpriteSheet> TextureListSheep { get; set; }
        public float Scale { get; set; }

        public int GetWidthOfSheepTexture()
        {
            return (int)((TextureListSheep[AnimationType.Idle].TextureBlackRight.Width /
                 TextureListSheep[AnimationType.Idle].Colums) * Scale);
        }

        public int GetHeightOfSheepTexture()
        {
            return (int)((TextureListSheep[AnimationType.Idle].TextureBlackRight.Height /
                           TextureListSheep[AnimationType.Idle].Rows) * Scale);
        }

        public AnimationManager()
        {
            TextureListSheep = new Dictionary<AnimationType, SpriteSheet>();

            Scale = 0.4f;
        }

        public void LoadContent(Game1 g)
        {

            #region sheep_textures

            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Headbanging,
                new SpriteSheet()
                {
                    Colums = 3,
                    Rows = 2,
                    FrameCount = 6,
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_headbang_left"),
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_headbang_left"), //too lazy for black
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_headbang_right"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_white/white_headbang_right") //too lazy for black
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Dying,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 5,
                    FrameCount = 18,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_dying_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_dying_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_dying_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_dying_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Eating,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 4,
                    FrameCount = 13,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_eating_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_eating_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_eating_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_eating_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Fearing,
                new SpriteSheet()
                {
                    Colums = 3,
                    Rows = 2,
                    FrameCount = 6,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_fearing_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_fearing_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_fearing_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_fearing_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Hurt,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 3,
                    FrameCount = 12,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_hurt_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_hurt_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_hurt_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_hurt_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Idle,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 3,
                    FrameCount = 12,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_idle_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_idle_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_idle_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_idle_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.IdleBlinking,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 3,
                    FrameCount = 12,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_idle_blinking_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_idle_blinking_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_idle_blinking_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_idle_blinking_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Jerking,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 3,
                    FrameCount = 12,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_jerking_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_jerking_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_jerking_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_jerking_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Running,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 3,
                    FrameCount = 12,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_running_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_running_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_running_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_running_right")
                });
            AnimationManager.Reference.TextureListSheep.Add(AnimationManager.AnimationType.Walking,
                new SpriteSheet()
                {
                    Colums = 4,
                    Rows = 5,
                    FrameCount = 18,
                    TextureBlackLeft = g.Content.Load<Texture2D>("textures/sheep_black/black_walking_left"),
                    TextureBlackRight = g.Content.Load<Texture2D>("textures/sheep_black/black_walking_right"),
                    TextureWhiteLeft = g.Content.Load<Texture2D>("textures/sheep_white/white_walking_left"),
                    TextureWhiteRight = g.Content.Load<Texture2D>("textures/sheep_white/white_walking_right")
                });


            #endregion


        }


        public void DrawSheep(SpriteBatch spriteBatch, Vector2 position,
                AnimationType animationType, bool left, bool isBlack, int frame, Color color)
        {
            SpriteSheet ss = TextureListSheep[animationType];
            Texture2D tex = null;
            if (left && isBlack) tex = ss.TextureBlackLeft;
            if (left && !isBlack) tex = ss.TextureWhiteLeft;
            if (!left && isBlack) tex = ss.TextureBlackRight;
            if (!left && !isBlack) tex = ss.TextureWhiteRight;

            if (tex == null)
                return;

            var currentRow = (int)(frame / ss.Colums);
            var currentColum = (int)(frame % ss.Colums);

            var frameHeight = tex.Height / ss.Rows;
            var frameWidth = tex.Width / ss.Colums;


            var zIndex = 0.75f - 0.5f / (position.Y + 100);




            spriteBatch.Draw(
                tex,
                position,
                new Rectangle(
                    frameWidth * currentColum,
                    frameHeight * currentRow,
                    frameWidth,
                    frameHeight),
                color,
                0f,
                Vector2.Zero,
                Scale,
                SpriteEffects.None,
                zIndex);
        }
    }

    public class SpriteSheet
    {
        public Texture2D TextureBlackLeft { get; set; }
        public Texture2D TextureBlackRight { get; set; }
        public Texture2D TextureWhiteLeft { get; set; }
        public Texture2D TextureWhiteRight { get; set; }
        public int FrameCount { get; set; }
        public int Rows { get; set; }
        public int Colums { get; set; }
    }
}
