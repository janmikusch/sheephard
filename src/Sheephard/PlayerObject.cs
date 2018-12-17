/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Sheephard
{
    public abstract class PlayerObject : IComparable<PlayerObject>
    {
        public enum State
        {
            Idle, Walking, Running, Eating, Fearing
            , Headbanging
        }

        public const float WALKING_SPEED = 1.5f;
        public const float RUNNING_SPEED = 3.5f;
        public const int DISPLAYBOARDERLEFT = 100;
        public const int DISPLAYBOARDERRIGHT = 1280;
        public const int DISPLAYBOARDERTOP = 80;
        public const int DISPLAYBOARDERBOTTOM = 730;

        protected State CurrentState;
        //input vars
        protected Vector2 _movement;

        public Vector2 Movement { get => _movement; set => _movement = value; }

        //player vars
        public Color PlayerColor
        {
            get
            {
                if (PlayerId == 0)
                    return Color.Red;
                if (PlayerId == 1)
                    return Color.DodgerBlue;
                if (PlayerId == 2)
                    return Color.DarkOrchid;
                if (PlayerId == 3)
                    return Color.Orange;

                return Color.White;
            }
        }

        public int PlayerId { get; }
        public Vector2 Position { get; set; }
        public Sheep Sheep { get; set; }
        //public bool IsActive { get; set; }



        protected PlayerObject(int playerId, Vector2 position = new Vector2())
        {
            PlayerId = playerId;
            Position = position;
            Init();
        }

        protected abstract void GetInput(GameTime gameTime);

        public abstract bool IsNpc();

        public abstract void DoStupidThings();

        public virtual void Init()
        {
            Movement = new Vector2(0, 0);
            CurrentState = State.Idle;
        }

        public int CompareTo(PlayerObject other)
        {
            return this.Position.Y.CompareTo(other.Position.Y);
        }

        public void Update(GameTime gameTime, List<PlayerObject> players)
        {
            Sheep.Update(gameTime);

            if (GameController.RoundStartCountdown > 0)
            {
                return;
            }

            if (GameController.SelectedPlayer != this) //when selected you can't move
            {
                GetInput(gameTime);
            }
            else
            {
                Sheep.Play(AnimationManager.AnimationType.Fearing);
                return;
            }

            CollisionDetection(); // YOU SHALL NOT PASS!!!!!!!!


            PlayState();


        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sheep.Draw(gameTime, spriteBatch, Position, PlayerColor);
            if (GameController.State == GameState.Waiting && PlayerId >= 0)
            {
                Vector2 delta = new Vector2(50, -20);
                var feedbacktext = GameController.ScoreBoard.ScoreList["Player" + PlayerId.ToString()].Feedback();
                spriteBatch.DrawString(FontHelper.Reference.XxLarge, feedbacktext, Position + delta, PlayerColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.89f);
            }

            if (GameController.RoundStartCountdown > 0)
            {
                var text = GameController.RoundStartCountdown.ToString("0");

                var centerPosX = (Game1.Reference.GraphicsDevice.Viewport.Width / 2) -
                                 (FontHelper.Reference.XxxxLarge.MeasureString(text).X / 2);
                var centerPosY = (Game1.Reference.GraphicsDevice.Viewport.Height / 2) -
                                 (FontHelper.Reference.XxxxLarge.MeasureString(text).Y / 2);

                spriteBatch.DrawString(FontHelper.Reference.XxxxLarge, text,
                    new Vector2(centerPosX, centerPosY), Color.AliceBlue, 0f,
                    Vector2.Zero, 1f, SpriteEffects.None, 0.9f);
            }
        }

        private void PlayState()
        {
            switch (CurrentState)
            {
                case State.Eating:
                    Sheep.Play(AnimationManager.AnimationType.Eating);
                    break;
                case State.Running:
                    Sheep.Play(AnimationManager.AnimationType.Running);
                    Position += _movement;
                    break;
                case State.Walking:
                    Sheep.Play(AnimationManager.AnimationType.Walking);
                    Position += _movement;
                    break;
                case State.Headbanging:
                    Sheep.Play(AnimationManager.AnimationType.Headbanging);
                    break;
                default:
                    Sheep.Play(AnimationManager.AnimationType.Idle);
                    break;
            }
        }

        protected void CollisionDetection()
        {
            if (_movement.X < 0 && CollisionLeft() ||
                _movement.X > 0 && CollisionRight())
                _movement.X = 0;
            if (_movement.Y < 0 && CollisionTop() ||
                _movement.Y > 0 && CollisionBottom())
                _movement.Y = 0;
        }

        // Collision
        public Rectangle GetRectangle()
        {
            var offsetY = 140;
            var offsetX = 100;
            return new Rectangle((int)(Math.Round(Position.X) + offsetX),
                (int)(Math.Round(Position.Y) + offsetY),
                AnimationManager.Reference.GetWidthOfSheepTexture() - offsetX,
                AnimationManager.Reference.GetHeightOfSheepTexture() - offsetY);
        }

        public bool CollisionLeft()
        {
            var thisRec = this.GetRectangle();

            if (thisRec.Left + _movement.X < DISPLAYBOARDERLEFT)
                return true;

            foreach (var player in GameController.Players)
            {
                if (player == this)
                    continue;

                var playerRect = player.GetRectangle();

                if (thisRec.Left + _movement.X < playerRect.Right &&
                    thisRec.Right > playerRect.Right &&
                    thisRec.Bottom > playerRect.Top &&
                    thisRec.Top < playerRect.Bottom)
                    return true;

            }

            return false;
        }

        public bool CollisionRight()
        {
            var thisRec = this.GetRectangle();

            if (thisRec.Right + _movement.X > DISPLAYBOARDERRIGHT)
                return true;

            foreach (var player in GameController.Players)
            {
                if (player == this)
                    continue;
                var playerRect = player.GetRectangle();

                if (thisRec.Right + _movement.X > playerRect.Left &&
                    thisRec.Left < playerRect.Left &&
                    thisRec.Bottom > playerRect.Top &&
                    thisRec.Top < playerRect.Bottom)
                    return true;

            }


            return false;
        }

        public bool CollisionTop()
        {
            var thisRec = this.GetRectangle();

            if (thisRec.Top + _movement.Y < DISPLAYBOARDERTOP)
                return true;

            foreach (var player in GameController.Players)
            {
                if (player == this)
                    continue;
                var playerRect = player.GetRectangle();

                if (thisRec.Top + _movement.Y < playerRect.Bottom &&
                    thisRec.Bottom > playerRect.Bottom &&
                    thisRec.Right > playerRect.Left &&
                    thisRec.Left < playerRect.Right)
                    return true;

            }

            return false;
        }

        public bool CollisionBottom()
        {
            var thisRec = this.GetRectangle();

            if (thisRec.Bottom + _movement.Y > DISPLAYBOARDERBOTTOM)
                return true;

            foreach (var player in GameController.Players)
            {
                if (player == this)
                    continue;
                var playerRect = player.GetRectangle();

                if (thisRec.Bottom + _movement.Y > playerRect.Top &&
                    thisRec.Top < playerRect.Top &&
                    thisRec.Right > playerRect.Left &&
                    thisRec.Left < playerRect.Right)
                    return true;

            }

            return false;
        }
    }
}
