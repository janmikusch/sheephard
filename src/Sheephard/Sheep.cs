/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
    The Calculation for the Current frame was made with the tutorial here: 
    https://blogs.msdn.microsoft.com/tarawalker/2013/04/12/windows-8-game-development-using-c-xna-and-monogame-3-0-building-a-shooter-game-walkthrough-part-5-animating-the-playership-and-creating-a-parallaxing-background/
*/

namespace Sheephard
{
    public class Sheep
    {
        private AnimationManager.AnimationType _currentAnimation;
        public bool LookDirectionLeft { get; set; }

        public bool IsBlack { get; set; }

        public int CurrentFrame { get; set; }
        public double ElapsedTime { get; private set; }


        public Sheep()
        {
            CurrentFrame = 0;
            ElapsedTime = 0;
            LookDirectionLeft = false;
            IsBlack = false;
            _currentAnimation = AnimationManager.AnimationType.Idle;
        }

        public void Update(GameTime gameTime)
        {
            ElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds; // adds elapsed time since the last frame

            if (ElapsedTime > AnimationManager.FRAMETIME)
            {
                CurrentFrame++;

                //wenn anzahl an maximalen frames erreicht ist, starte von 0
                if (CurrentFrame == AnimationManager.Reference.TextureListSheep[_currentAnimation].FrameCount)
                {
                    //blinking idle
                    if (_currentAnimation == AnimationManager.AnimationType.IdleBlinking)
                        _currentAnimation = AnimationManager.AnimationType.Idle;
                    else if (_currentAnimation == AnimationManager.AnimationType.Idle)
                    {
                        var n = RandomNumber.Between(0, 100);
                        if (n < 20)
                            Play(AnimationManager.AnimationType.IdleBlinking);
                    }

                    if (_currentAnimation == AnimationManager.AnimationType.Dying)
                        CurrentFrame--; //don't loop dying
                    else
                        CurrentFrame = 0;
                }

                ElapsedTime = 0;


            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Vector2 position, Color color)
        {
            var c = Color.White;
            if (GameController.State == GameState.Waiting)
                c = color;
            AnimationManager.Reference.DrawSheep(spriteBatch, position, _currentAnimation, LookDirectionLeft, IsBlack, CurrentFrame, c);
        }

        public void Play(AnimationManager.AnimationType animation)
        {
            if (_currentAnimation == AnimationManager.AnimationType.IdleBlinking &&
                animation == AnimationManager.AnimationType.Idle)
                return;
            if (_currentAnimation != animation)
            {
                _currentAnimation = animation;
                CurrentFrame = 0;
                ElapsedTime = 0;
            }
        }

    }
}
