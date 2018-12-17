using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sheephard
{
    public class ScoreBoard
    {
        private static ScoreBoard _reference;
        public static ScoreBoard Reference => _reference ?? (_reference = new ScoreBoard());

        public static Texture2D TimerBackground { get; set; }
        public static Texture2D ScoreBackground { get; set; }

        public Dictionary<string, Score> ScoreList;

        public ScoreBoard()
        {
            ScoreList = new Dictionary<string, Score>();
        }

        public void LoadContent(Game1 g)
        {
            //ScoreBoard
            TimerBackground = g.Content.Load<Texture2D>("textures/timerOverlay");
            ScoreBackground = g.Content.Load<Texture2D>("textures/scoreOverlay");
        }

        public int GetPlaceOfPlayer(PlayerObject po)
        {
            var s = ScoreList.Values.ToList();
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i].Name == "Player " + (po.PlayerId + 1))
                    return i + 1;
            }
            return 0;
        }

        public int GetPointsOfPlayer(PlayerObject po)
        {
            var s = ScoreList.Values.ToList();
            for (int i = 0; i < s.Count; i++)
            {
                if (s[i].Name == "Player " + (po.PlayerId + 1))
                    return s[i].Points;
            }
            return 0;
        }

        public void DrawBigScoreboard(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var s = ScoreList.Values.ToList();


            spriteBatch.DrawString(FontHelper.Reference.XxLarge, "Endergebniss:",
                new Vector2(100, 100), Color.AliceBlue, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            s.Sort();
            for (int i = 0; i < s.Count; i++)
            {
                spriteBatch.DrawString(FontHelper.Reference.XxLarge, s[i].ToString(),
                    new Vector2(100, 220 + i * 100), s[i].Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var s = ScoreList.Values.ToList();


            spriteBatch.Draw(TimerBackground, new Rectangle(0, 0, TimerBackground.Width, TimerBackground.Height),
                null, new Color(Color.White, 0.25f), 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
            spriteBatch.Draw(ScoreBackground, new Rectangle(0, -300 + s.Count * 38, ScoreBackground.Width, ScoreBackground.Height),
                null, new Color(Color.White, 0.25f), 0f, Vector2.Zero, SpriteEffects.None, 0.9f);

            var text = "Zeit: ";
            if (GameController.State == GameState.Waiting)
            {
                text = "Runde " + (GameController.Round + 1) + " in: ";
                if (GameController.Round == GameController.MaxRounds)
                {
                    text = "Spiel endet in: ";
                }
            }

            var centerPosTimer = (Game1.Reference.GraphicsDevice.Viewport.Width / 2) -
                                 (FontHelper.Reference.Medium.MeasureString(text + "00").Length() / 2);

            spriteBatch.DrawString(FontHelper.Reference.Medium, text + GameController.RoundCountdown.ToString("00"),
                new Vector2(centerPosTimer, 10), Color.AliceBlue, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);

            s.Sort();

            spriteBatch.DrawString(FontHelper.Reference.Medium, "Runde " + GameController.Round,
                new Vector2(1090, 10), Color.AliceBlue, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            for (int i = 0; i < s.Count; i++)
            {
                spriteBatch.DrawString(FontHelper.Reference.Medium, s[i].ToString(),
                    new Vector2(1090, 50 + i * 35), s[i].Color, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
            }

        }

        public class Score : IComparable<Score>
        {
            public string Name { get; private set; }
            public int Points { get; private set; }
            public Color Color { get; private set; }
            private int _feedbackPoint;

            public Score(string name, Color color)
            {
                Points = 0;
                Name = name;
                Color = color;
                _feedbackPoint = 0;
            }

            public void Success()
            {
                Points++;
                _feedbackPoint = 1;
            }

            public void Neutral()
            {
                _feedbackPoint = 0;
            }

            public void Failure()
            {
                Points--;
                _feedbackPoint = -1;
            }

            public int CompareTo(Score other)
            {
                return other.Points.CompareTo(Points);
            }

            public string Feedback()
            {
                if (_feedbackPoint < 0)
                    return "-1";
                if (_feedbackPoint > 0)
                    return "+1";
                return "+0";
            }

            public override string ToString()
            {
                return Name + ":  " + Points;
            }
        }
    }
}
