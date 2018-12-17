/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Sheephard
{
    public enum GameState
    {
        MainMenu,
        Play,
        Waiting,
        Credits,
        GameEnd
    }

    public static class GameController
    {
        //private static int ___debug_headbangcounter = 0;
        private static Random rnd = new Random();

        public const double WAITTONEXTROUND = 7d;
        public const double ROUNDTIME = 60d;
        public const double STARTCOUNTDOWN = 3d;

        //Music
        public static Song BackgroundMusic;

        public static SoundEffect[] SheepSounds { get; private set; }

        //texture
        public static Texture2D BackgroundTexture;

        //references
        public static ScoreBoard ScoreBoard { get; set; }

        //game fields
        public static int Round { get; set; }
        public static int MaxRounds { get; set; }
        public static int BlackSheepId { get; set; } //ID of BlackSheep - player
        public static double RoundCountdown { get; private set; }
        public static double RoundStartCountdown { get; private set; }
        public static bool CrazyWasActive { get; private set; }

        //player objects
        public static PlayerObject BlackSheepPlayer { get; set; }
        public static PlayerObject SelectedPlayer { get; set; }
        public static List<PlayerObject> Players { get; set; }

        //GameState
        public static GameState State { get; set; }

        public static void Init()
        {
            Round = 0;
            BlackSheepId = -1;
            State = GameState.MainMenu;
            SheepSounds = new SoundEffect[2];
        }

        public static void LoadContent(Game1 g)
        {
            BackgroundTexture = g.Content.Load<Texture2D>("textures/background_1280x720");

            /*
             * https://opengameart.org/content/sheep-sound-bleats-yo-frankie
             * https://opengameart.org/content/joy-ride
            */

            BackgroundMusic = g.Content.Load<Song>("Joy_Ride");
            SheepSounds[0] = g.Content.Load<SoundEffect>("sheep1");
            SheepSounds[1] = g.Content.Load<SoundEffect>("sheepBleet");
        }

        public static void Update(GameTime gameTime)
        {
            InputManager.SetCurrentState();

            if (InputManager.ToMainMenuPressed())
            {
                
                if (State != GameState.MainMenu)
                    State = GameState.MainMenu; // go to main menu
                else
                {
                    Game1.Reference.Exit(); // or exit game
                }
            }

            if (InputManager.ToggleFullscreenPressed())
            {
                Game1.Reference.GraphicsDeviceManager.ToggleFullScreen();
                Game1.Reference.GraphicsDeviceManager.ApplyChanges();
            }


            switch (State)
            {
                case GameState.MainMenu:
                    MainMenu.Reference.Update(gameTime);
                    break;
                case GameState.Play:
                    if (RoundStartCountdown > 0)
                    {
                        RoundStartCountdown -= gameTime.ElapsedGameTime.TotalSeconds;
                    }
                    else
                        RoundCountdown -= gameTime.ElapsedGameTime.TotalSeconds;

                    if (RoundCountdown < 0)
                    {
                        EndRound();
                        break;
                    }

                    var dst = CheckDoStupidThings();

                    foreach (var p in Players)
                    {
                        if (dst)
                        {
                            p.DoStupidThings();
                        }
                        p.Update(gameTime, Players);
                    }

                    CheckSelected();

                    break;
                case GameState.Waiting:

                    if (RoundCountdown > 0)
                        RoundCountdown -= gameTime.ElapsedGameTime.TotalSeconds;
                    else
                    {
                        if (Round >= MaxRounds)
                        {
                            State = GameState.GameEnd;
                            RoundCountdown = 10d;
                            return;
                        }
                        else
                        {
                            State = GameState.Play;
                            StartRound();
                        }
                    }

                    foreach (var p in Players)
                    {
                        p.Sheep.Update(gameTime);
                        p.Sheep.Play(SelectedPlayer == p
                            ? AnimationManager.AnimationType.Dying
                            : AnimationManager.AnimationType.Idle);
                    }

                    break;
                case GameState.Credits:
                    CreditScreen.Reference.Update(gameTime);
                    break;
                case GameState.GameEnd:
                    RoundCountdown -= gameTime.ElapsedGameTime.TotalSeconds;
                    if (RoundCountdown < 0)
                    {
                        State = GameState.MainMenu;
                    }

                    break;
            }

            InputManager.SetLastState();
        }

        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(BackgroundTexture, new Rectangle(0, 0, BackgroundTexture.Width, BackgroundTexture.Height),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            switch (State)
            {
                case GameState.MainMenu:
                    MainMenu.Reference.Draw(spriteBatch, gameTime);
                    break;
                case GameState.Play:
                    foreach (var player in Players)
                    {
                        player.Draw(gameTime, spriteBatch);
                        ScoreBoard.Draw(spriteBatch, gameTime);
                    }

                    break;
                case GameState.Waiting:
                    for (var index = 0; index < Players.Count; index++)
                    {
                        var player = Players[index];
                        player.Draw(gameTime, spriteBatch);
                        ScoreBoard.Draw(spriteBatch, gameTime);
                    }

                    break;
                case GameState.Credits:
                    CreditScreen.Reference.Draw(spriteBatch, gameTime);
                    break;
                case GameState.GameEnd:
                    ScoreBoard.DrawBigScoreboard(spriteBatch, gameTime);
                    break;
            }
        }

        public static void PlayMusic()
        {
            MediaPlayer.Play(BackgroundMusic);
            MediaPlayer.IsRepeating = true;
        }

        public static void StartGame(int playerCount, int npcCount, int maxRounds)
        {
            ScoreBoard = new ScoreBoard();
            MaxRounds = maxRounds;
            Round = 0;

            State = GameState.Play;
            Players = new List<PlayerObject>();
            List<int> gamePadIds = new List<int>(4);
            if (InputManager.IsGamePadConnected(0))
                gamePadIds.Add(((int)PlayerIndex.One));
            if (InputManager.IsGamePadConnected(1))
                gamePadIds.Add(((int)PlayerIndex.Two));
            if (InputManager.IsGamePadConnected(2))
                gamePadIds.Add(((int)PlayerIndex.Three));
            if (InputManager.IsGamePadConnected(3))
                gamePadIds.Add(((int)PlayerIndex.Four));

            for (int i = 0; i < playerCount; i++)
            {
                if (i + 1 > gamePadIds.Count)
                {
                    State = GameState.MainMenu;
                    MainMenu.Reference.ErrorMessage = "GamePad Error";
                    return;
                }

                var p = new Player(i, gamePadIds[i], new Vector2(-2000f, -2000f));
                Players.Add(p);

                ScoreBoard.ScoreList.Add("Player" + i, new ScoreBoard.Score("Player " + (i + 1), p.PlayerColor));
            }

            for (int i = 0; i < npcCount; i++)
            {
                Players.Add(new Npc(-1) { Position = new Vector2(-1000f, -1000f) });
            }


            StartRound();
        }

        private static void StartRound()
        {
            Round++;
            SelectedPlayer = null;
            RoundCountdown = ROUNDTIME;
            RoundStartCountdown = STARTCOUNTDOWN;
            CrazyWasActive = false;
            BlackSheepNext();
            foreach (var player in Players)
            {
                player.Init();
                player.Position = SpawnPosition(player);
                player.Sheep = new Sheep();
                if (player.PlayerId == BlackSheepId)
                {
                    BlackSheepPlayer = player;
                    player.Sheep.IsBlack = true;
                }
            }
        }

        private static Vector2 SpawnPosition(PlayerObject p)
        {
            var contains = true;
            Vector2 v = new Vector2();
            int margin = 100;

            do
            {
                v.X = RandomNumber.Between(40, 1050);
                v.Y = RandomNumber.Between(0, 500);
                p.Position = v;
                contains = false;
                var pRec = p.GetRectangle();

                foreach (var player in Players)
                {
                    if (p != player)
                    {
                        var other = player.GetRectangle();
                        other.Inflate(margin, margin);
                        if (other.Contains(pRec.Center))
                        {
                            contains = true;
                        }
                    }
                }
            } while (contains);

            return v;
        }

        private static void BlackSheepNext()
        {
            if (BlackSheepId >= MainMenu.Reference.PlayerCount - 1 || BlackSheepId < 0)
                BlackSheepId = 0;
            else
                BlackSheepId++;
        }

        private static void CheckSelected()
        {
            Point point = BlackSheepPlayer.GetRectangle().Center;

            if (BlackSheepPlayer.Sheep.LookDirectionLeft)
                point.X -= (int)(BlackSheepPlayer.GetRectangle().Width * 1.4f);
            else
                point.X += (int)(BlackSheepPlayer.GetRectangle().Width * 1.4f);

            SelectedPlayer = null;

            foreach (var playerObject in Players)
            {
                if (playerObject != BlackSheepPlayer)
                {
                    var rec = playerObject.GetRectangle();
                    if (rec.Contains(point))
                    {
                        SelectedPlayer = playerObject;
                    }
                }
            }
        }

        public static void ChooseSelected()
        {
            if (SelectedPlayer != null)
                EndRound();
        }

        public static void EndRound()
        {
            if (SelectedPlayer != null)
            {
                //point for every player who wasn't found and is not the black sheep
                if (SelectedPlayer.PlayerId != 0 && BlackSheepPlayer.PlayerId != 0)
                    ScoreBoard.ScoreList["Player0"].Success();
                if (SelectedPlayer.PlayerId != 1 && BlackSheepPlayer.PlayerId != 1)
                    ScoreBoard.ScoreList["Player1"].Success();
                if (MainMenu.Reference.PlayerCount >= 3 && SelectedPlayer.PlayerId != 2 &&
                    BlackSheepPlayer.PlayerId != 2)
                    ScoreBoard.ScoreList["Player2"].Success();
                if (MainMenu.Reference.PlayerCount == 4 && SelectedPlayer.PlayerId != 3 &&
                    BlackSheepPlayer.PlayerId != 3)
                    ScoreBoard.ScoreList["Player3"].Success();

                if (SelectedPlayer.PlayerId >= 0)
                {
                    //point for blackie, no point for found player
                    ScoreBoard.ScoreList["Player" + BlackSheepPlayer.PlayerId].Success();
                    ScoreBoard.ScoreList["Player" + SelectedPlayer.PlayerId].Neutral();
                }
                else
                {
                    ScoreBoard.ScoreList["Player" + BlackSheepPlayer.PlayerId].Failure(); //wrong target
                }
            }
            else
            {
                //point for every player who wasn't found and is not the black sheep
                if (BlackSheepPlayer.PlayerId != 0)
                    ScoreBoard.ScoreList["Player0"].Success();
                if (BlackSheepPlayer.PlayerId != 1)
                    ScoreBoard.ScoreList["Player1"].Success();
                if (MainMenu.Reference.PlayerCount >= 3 && BlackSheepPlayer.PlayerId != 2)
                    ScoreBoard.ScoreList["Player2"].Success();
                if (MainMenu.Reference.PlayerCount == 4 && BlackSheepPlayer.PlayerId != 3)
                    ScoreBoard.ScoreList["Player3"].Success();

                ScoreBoard.ScoreList["Player" + BlackSheepPlayer.PlayerId].Failure();
                //minus point - nobody found!
            }

            State = GameState.Waiting;
            RoundCountdown = WAITTONEXTROUND;
        }

        private static bool CheckDoStupidThings()
        {
            if (RoundCountdown < ROUNDTIME / 3 && CrazyWasActive == false)
            {
                float place = ScoreBoard.GetPlaceOfPlayer(BlackSheepPlayer);
                float points = ScoreBoard.GetPointsOfPlayer(BlackSheepPlayer);

                float divide = MaxRounds + 2 * points;
                if (divide <= 0)
                    divide = 1;

                // platzierung erhöht, punkte verringern punkte
                float chance = (Round * place / divide);

                // round * place / ( MaxRound + 2*points)


                var v = rnd.Next(10000);

                if (v < chance)
                {
                    CrazyWasActive = true;
                    return true;
                }
            }

            return false;
        }
    }
}
