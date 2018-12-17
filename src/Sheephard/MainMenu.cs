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
    public class MainMenu
    {
        public static MainMenu Reference => _reference ?? (_reference = new MainMenu());
        private static MainMenu _reference;

        public static Texture2D HowToTexture { get; set; }
        public string ErrorMessage { get; set; }

        public List<MenuObject> MenuObjects { get; set; }

        private MenuObjectType _selectedItem;
        public MenuObjectType SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (!((int)value < 0 || (int)value > Enum.GetNames(typeof(MenuObjectType)).Length - 1))
                    _selectedItem = value;
            }
        }

        private Sheep _menuSheep1;
        private Sheep _menuSheep2;
        private Sheep _menuSheep3;
        private Sheep _menuSheep4;
        private int _playerCount;
        private int _maxRounds;

        public int PlayerCount
        {
            get => _playerCount; set
            {
                if (value <= 4 && value >= 2) _playerCount = value;
            }
        }

        public int MaxRounds
        {
            get => _maxRounds; set
            {
                if (value <= 50 && value >= 4) _maxRounds = value;
            }
        }

        private MainMenu()
        {
            MenuObjects = new List<MenuObject>();
            Init();
        }

        public void Init()
        {
            PlayerCount = 2;
            MaxRounds = 12;
            SelectedItem = MenuObjectType.Play;

            _menuSheep1 = new Sheep { IsBlack = true };
            _menuSheep1.Play(AnimationManager.AnimationType.Eating);
            _menuSheep2 = new Sheep { IsBlack = false, LookDirectionLeft = true };
            _menuSheep2.Play(AnimationManager.AnimationType.Eating);
            _menuSheep3 = new Sheep { IsBlack = false };
            _menuSheep3.Play(AnimationManager.AnimationType.Hurt);
            _menuSheep4 = new Sheep { IsBlack = false, LookDirectionLeft = true };
            _menuSheep4.Play(AnimationManager.AnimationType.Fearing);

            ErrorMessage = "";


        }

        public void LoadContent(Game1 g)
        {
            HowToTexture = g.Content.Load<Texture2D>("textures/HowTo");

            Reference.MenuObjects.Add(new MenuObject() { Text = "START", Type = MenuObjectType.Play });
            Reference.MenuObjects.Add(new MenuObject() { Text = "CREDITS", Type = MenuObjectType.Credits });
            Reference.MenuObjects.Add(new MenuObject() { Text = "SPIELER", Type = MenuObjectType.Players });
            Reference.MenuObjects.Add(new MenuObject() { Text = "RUNDEN:", Type = MenuObjectType.Rounds });
        }

        public void Update(GameTime gameTime)
        {
            if (InputManager.MenuDownPressed())
            {
                SelectedItem = SelectedItem + 1;
                ErrorMessage = "";
            }
            if (InputManager.MenuUpPressed())
            {
                SelectedItem = SelectedItem - 1;
                ErrorMessage = "";
            }

            if (SelectedItem == MenuObjectType.Play)
            {
                if (InputManager.MenuContinuePressed())
                {
                    //test
                    //Game1.Reference.StartGame(PlayerCount, 0);

                    if (InputManager.ConnectedGamepads >= PlayerCount)
                    {
                        GameController.StartGame(PlayerCount, 4 + PlayerCount, MaxRounds);
                    }
                    else
                    {
                        ErrorMessage = "Zu wenige Gamepads!\nVerbinde mehr GamePads.";
                    }
                }
            }
            // PlayerObject selection
            if (SelectedItem == MenuObjectType.Players)
            {
                if (InputManager.MenuRightPressed())
                {
                    PlayerCount++;
                }
                if (InputManager.MenuLeftPressed())
                {
                    PlayerCount--;
                }
            }

            if (SelectedItem == MenuObjectType.Credits)
            {
                if (InputManager.MenuContinuePressed())
                {
                    GameController.State = GameState.Credits;
                }
            }

            if (SelectedItem == MenuObjectType.Rounds)
            {
                if (InputManager.MenuRightPressed())
                {
                    MaxRounds++;
                }
                if (InputManager.MenuLeftPressed())
                {
                    MaxRounds--;
                }
            }

            _menuSheep1.Update(gameTime);
            _menuSheep2.Update(gameTime);
            _menuSheep3.Update(gameTime);
            _menuSheep4.Update(gameTime);

        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(HowToTexture,
                new Rectangle(0, 0, HowToTexture.Width, HowToTexture.Height),
                null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);

            //Connected Controllers
            spriteBatch.DrawString(FontHelper.Reference.Small,
                "Verbundene Gamepads: " + InputManager.ConnectedGamepads,
                new Vector2(1000, 8), Color.AliceBlue);

            //error
            spriteBatch.DrawString(FontHelper.Reference.Small,
                ErrorMessage,
                new Vector2(300, 60), Color.DarkRed);

            //Impressium


            for (int i = 0; i < MenuObjects.Count - 1; i++)
            {
                MenuObjects[i].Draw(spriteBatch, gameTime, new Vector2(20, 20 + (100 * i)));
            }

            MenuObjects[3].Draw(spriteBatch, gameTime, new Vector2(20, 680), true, MaxRounds.ToString());

            _menuSheep1.Draw(gameTime, spriteBatch, new Vector2(40, 270), Color.White);
            _menuSheep2.Draw(gameTime, spriteBatch, new Vector2(230, 290), Color.White);
            if (PlayerCount > 2)
                _menuSheep3.Draw(gameTime, spriteBatch, new Vector2(10, 400), Color.White);
            if (PlayerCount > 3)
                _menuSheep4.Draw(gameTime, spriteBatch, new Vector2(200, 410), Color.White);


        }


    }
}
