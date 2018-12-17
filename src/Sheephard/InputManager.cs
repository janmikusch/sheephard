/*
 * Jan Mikusch
 * FH Salzburg - Multimedia Technology
 * MultiMediaProjekt (1)
 */

using Microsoft.Xna.Framework.Input;

namespace Sheephard
{
    public static class InputManager
    {
        #region keyboard const

        private const Keys KEY_TOMAINMENU = Keys.Escape;
        private const Keys KEY_TOGGLEFULLSCREEN = Keys.F11;

        private const Keys KEY_LEFT = Keys.Left;
        private const Keys KEY_UP = Keys.Up;
        private const Keys KEY_RIGHT = Keys.Right;
        private const Keys KEY_DOWN = Keys.Down;

        private const Keys KEY_CONTINUE = Keys.Enter;
        private const Keys KEY_BACK = Keys.Back;

        #endregion

        #region gamepad const

        private const Buttons BTN_TOMAINMENU = Buttons.Start;
        private const Buttons BTN_TOGGLEFULLSCREEN = Buttons.Back;

        private const Buttons DPAD_LEFT = Buttons.DPadLeft;
        private const Buttons DPAD_UP = Buttons.DPadUp;
        private const Buttons DPAD_RIGHT = Buttons.DPadRight;
        private const Buttons DPAD_DOWN = Buttons.DPadDown;

        private const Buttons STICK_LEFT = Buttons.LeftThumbstickLeft;
        private const Buttons STICK_UP = Buttons.LeftThumbstickUp;
        private const Buttons STICK_RIGHT = Buttons.LeftThumbstickRight;
        private const Buttons STICK_DOWN = Buttons.LeftThumbstickDown;

        private const Buttons BTN_RUN = Buttons.A;
        private const Buttons BTN_EAT = Buttons.B;
        private const Buttons BTN_CHOOSE = Buttons.X;
        private const Buttons BTN_MAKESOUND = Buttons.Y;

        private const Buttons BTN_CONTINUE = Buttons.A;
        private const Buttons BTN_BACK = Buttons.B;

        #endregion

        //keyboard
        private static KeyboardState _currentKeyboardState;
        private static KeyboardState _lastKeyboardState;

        //gamepad
        public static GamePadState[] CurrentGamePadStates;
        private static GamePadState[] _lastGamePadStates;

        private static GamePadCapabilities[] _gamePadCapabilitieses;

        public static void Init()
        {
            CurrentGamePadStates = new GamePadState[4];
            _lastGamePadStates = new GamePadState[4];
            _gamePadCapabilitieses = new GamePadCapabilities[4];

            CurrentGamePadStates[0] = GamePadState.Default;
            CurrentGamePadStates[1] = GamePadState.Default;
            CurrentGamePadStates[2] = GamePadState.Default;
            CurrentGamePadStates[3] = GamePadState.Default;

            _lastGamePadStates[0] = GamePadState.Default;
            _lastGamePadStates[1] = GamePadState.Default;
            _lastGamePadStates[2] = GamePadState.Default;
            _lastGamePadStates[3] = GamePadState.Default;


            SetCurrentState();
            SetLastState();
        }

        public static void SetCurrentState()
        {
            _currentKeyboardState = Keyboard.GetState();

            _gamePadCapabilitieses[0] = GamePad.GetCapabilities(0);
            _gamePadCapabilitieses[1] = GamePad.GetCapabilities(1);
            _gamePadCapabilitieses[2] = GamePad.GetCapabilities(2);
            _gamePadCapabilitieses[3] = GamePad.GetCapabilities(3);

            CurrentGamePadStates[0] = GamePad.GetState(0);
            CurrentGamePadStates[1] = GamePad.GetState(1);
            CurrentGamePadStates[2] = GamePad.GetState(2);
            CurrentGamePadStates[3] = GamePad.GetState(3);
        }

        public static void SetLastState()
        {
            _lastKeyboardState = Keyboard.GetState();

            _lastGamePadStates[0] = GamePad.GetState(0);
            _lastGamePadStates[1] = GamePad.GetState(1);
            _lastGamePadStates[2] = GamePad.GetState(2);
            _lastGamePadStates[3] = GamePad.GetState(3);
        }

        public static bool IsGamePadConnected(int id)
        {
            return _gamePadCapabilitieses[id].IsConnected;
        }

        public static int ConnectedGamepads
        {
            get
            {
                var c = 0;
                if (InputManager.IsGamePadConnected(0))
                    c++;
                if (InputManager.IsGamePadConnected(1))
                    c++;
                if (InputManager.IsGamePadConnected(2))
                    c++;
                if (InputManager.IsGamePadConnected(3))
                    c++;
                return c;
            }
        }

        #region GeneralInput

        public static bool ToggleFullscreenPressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_TOGGLEFULLSCREEN) && _lastKeyboardState.IsKeyUp(KEY_TOGGLEFULLSCREEN) ||
                   CurrentGamePadStates[0].IsButtonDown(BTN_TOGGLEFULLSCREEN) && _lastGamePadStates[0].IsButtonUp(BTN_TOGGLEFULLSCREEN) ||
                   CurrentGamePadStates[1].IsButtonDown(BTN_TOGGLEFULLSCREEN) && _lastGamePadStates[1].IsButtonUp(BTN_TOGGLEFULLSCREEN) ||
                   CurrentGamePadStates[2].IsButtonDown(BTN_TOGGLEFULLSCREEN) && _lastGamePadStates[2].IsButtonUp(BTN_TOGGLEFULLSCREEN) ||
                   CurrentGamePadStates[3].IsButtonDown(BTN_TOGGLEFULLSCREEN) && _lastGamePadStates[3].IsButtonUp(BTN_TOGGLEFULLSCREEN);
        }

        public static bool ToMainMenuPressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_TOMAINMENU) && _lastKeyboardState.IsKeyUp(KEY_TOMAINMENU) ||
                   CurrentGamePadStates[0].IsButtonDown(BTN_TOMAINMENU) && _lastGamePadStates[0].IsButtonUp(BTN_TOMAINMENU) ||
                   CurrentGamePadStates[1].IsButtonDown(BTN_TOMAINMENU) && _lastGamePadStates[1].IsButtonUp(BTN_TOMAINMENU) ||
                   CurrentGamePadStates[2].IsButtonDown(BTN_TOMAINMENU) && _lastGamePadStates[2].IsButtonUp(BTN_TOMAINMENU) ||
                   CurrentGamePadStates[3].IsButtonDown(BTN_TOMAINMENU) && _lastGamePadStates[3].IsButtonUp(BTN_TOMAINMENU);
        }
        #endregion

        #region MenuInput

        /// <summary>
        /// Check, if the Left key or any left Dpad / left STick got pressed
        /// </summary>
        /// <returns>true if Left is pressed</returns>
        public static bool MenuLeftPressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_LEFT) && _lastKeyboardState.IsKeyUp(KEY_LEFT) ||
                   CurrentGamePadStates[0].IsButtonDown(STICK_LEFT) && _lastGamePadStates[0].IsButtonUp(STICK_LEFT) ||
                   CurrentGamePadStates[0].IsButtonDown(DPAD_LEFT) && _lastGamePadStates[0].IsButtonUp(DPAD_LEFT) ||
                   CurrentGamePadStates[1].IsButtonDown(STICK_LEFT) && _lastGamePadStates[1].IsButtonUp(STICK_LEFT) ||
                   CurrentGamePadStates[1].IsButtonDown(DPAD_LEFT) && _lastGamePadStates[1].IsButtonUp(DPAD_LEFT) ||
                   CurrentGamePadStates[2].IsButtonDown(STICK_LEFT) && _lastGamePadStates[2].IsButtonUp(STICK_LEFT) ||
                   CurrentGamePadStates[2].IsButtonDown(DPAD_LEFT) && _lastGamePadStates[2].IsButtonUp(DPAD_LEFT) ||
                   CurrentGamePadStates[3].IsButtonDown(STICK_LEFT) && _lastGamePadStates[3].IsButtonUp(STICK_LEFT) ||
                   CurrentGamePadStates[3].IsButtonDown(DPAD_LEFT) && _lastGamePadStates[3].IsButtonUp(DPAD_LEFT);
        }

        /// <summary>
        /// Check, if the Up key or any left Dpad / left STick got pressed
        /// </summary>
        /// <returns>true if Up is pressed</returns>
        public static bool MenuUpPressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_UP) && _lastKeyboardState.IsKeyUp(KEY_UP) ||
                   CurrentGamePadStates[0].IsButtonDown(STICK_UP) && _lastGamePadStates[0].IsButtonUp(STICK_UP) ||
                   CurrentGamePadStates[0].IsButtonDown(DPAD_UP) && _lastGamePadStates[0].IsButtonUp(DPAD_UP) ||
                   CurrentGamePadStates[1].IsButtonDown(STICK_UP) && _lastGamePadStates[1].IsButtonUp(STICK_UP) ||
                   CurrentGamePadStates[1].IsButtonDown(DPAD_UP) && _lastGamePadStates[1].IsButtonUp(DPAD_UP) ||
                   CurrentGamePadStates[2].IsButtonDown(STICK_UP) && _lastGamePadStates[2].IsButtonUp(STICK_UP) ||
                   CurrentGamePadStates[2].IsButtonDown(DPAD_UP) && _lastGamePadStates[2].IsButtonUp(DPAD_UP) ||
                   CurrentGamePadStates[3].IsButtonDown(STICK_UP) && _lastGamePadStates[3].IsButtonUp(STICK_UP) ||
                   CurrentGamePadStates[3].IsButtonDown(DPAD_UP) && _lastGamePadStates[3].IsButtonUp(DPAD_UP);
        }

        /// <summary>
        /// Check, if the Right key or any left Dpad / left STick got pressed
        /// </summary>
        /// <returns>true if Right is pressed</returns>
        public static bool MenuRightPressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_RIGHT) && _lastKeyboardState.IsKeyUp(KEY_RIGHT) ||
                   CurrentGamePadStates[0].IsButtonDown(STICK_RIGHT) && _lastGamePadStates[0].IsButtonUp(STICK_RIGHT) ||
                   CurrentGamePadStates[0].IsButtonDown(DPAD_RIGHT) && _lastGamePadStates[0].IsButtonUp(DPAD_RIGHT) ||
                   CurrentGamePadStates[1].IsButtonDown(STICK_RIGHT) && _lastGamePadStates[1].IsButtonUp(STICK_RIGHT) ||
                   CurrentGamePadStates[1].IsButtonDown(DPAD_RIGHT) && _lastGamePadStates[1].IsButtonUp(DPAD_RIGHT) ||
                   CurrentGamePadStates[2].IsButtonDown(STICK_RIGHT) && _lastGamePadStates[2].IsButtonUp(STICK_RIGHT) ||
                   CurrentGamePadStates[2].IsButtonDown(DPAD_RIGHT) && _lastGamePadStates[2].IsButtonUp(DPAD_RIGHT) ||
                   CurrentGamePadStates[3].IsButtonDown(STICK_RIGHT) && _lastGamePadStates[3].IsButtonUp(STICK_RIGHT) ||
                   CurrentGamePadStates[3].IsButtonDown(DPAD_RIGHT) && _lastGamePadStates[3].IsButtonUp(DPAD_RIGHT);
        }

        /// <summary>
        /// Check, if the Down key or any left Dpad / left STick got pressed
        /// </summary>
        /// <returns>true if Down is pressed</returns>
        public static bool MenuDownPressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_DOWN) && _lastKeyboardState.IsKeyUp(KEY_DOWN) ||
                   CurrentGamePadStates[0].IsButtonDown(STICK_DOWN) && _lastGamePadStates[0].IsButtonUp(STICK_DOWN) ||
                   CurrentGamePadStates[0].IsButtonDown(DPAD_DOWN) && _lastGamePadStates[0].IsButtonUp(DPAD_DOWN) ||
                   CurrentGamePadStates[1].IsButtonDown(STICK_DOWN) && _lastGamePadStates[1].IsButtonUp(STICK_DOWN) ||
                   CurrentGamePadStates[1].IsButtonDown(DPAD_DOWN) && _lastGamePadStates[1].IsButtonUp(DPAD_DOWN) ||
                   CurrentGamePadStates[2].IsButtonDown(STICK_DOWN) && _lastGamePadStates[2].IsButtonUp(STICK_DOWN) ||
                   CurrentGamePadStates[2].IsButtonDown(DPAD_DOWN) && _lastGamePadStates[2].IsButtonUp(DPAD_DOWN) ||
                   CurrentGamePadStates[3].IsButtonDown(STICK_DOWN) && _lastGamePadStates[3].IsButtonUp(STICK_DOWN) ||
                   CurrentGamePadStates[3].IsButtonDown(DPAD_DOWN) && _lastGamePadStates[3].IsButtonUp(DPAD_DOWN);
        }


        public static bool MenuContinuePressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_CONTINUE) && _lastKeyboardState.IsKeyUp(KEY_CONTINUE) ||
                   CurrentGamePadStates[0].IsButtonDown(BTN_CONTINUE) && _lastGamePadStates[0].IsButtonUp(BTN_CONTINUE) ||
                   CurrentGamePadStates[1].IsButtonDown(BTN_CONTINUE) && _lastGamePadStates[1].IsButtonUp(BTN_CONTINUE) ||
                   CurrentGamePadStates[2].IsButtonDown(BTN_CONTINUE) && _lastGamePadStates[2].IsButtonUp(BTN_CONTINUE) ||
                   CurrentGamePadStates[3].IsButtonDown(BTN_CONTINUE) && _lastGamePadStates[3].IsButtonUp(BTN_CONTINUE);
        }

        public static bool MenuBackPressed()
        {
            return _currentKeyboardState.IsKeyDown(KEY_BACK) && _lastKeyboardState.IsKeyUp(KEY_BACK) ||
                   CurrentGamePadStates[0].IsButtonDown(BTN_BACK) && _lastGamePadStates[0].IsButtonUp(BTN_BACK) ||
                   CurrentGamePadStates[1].IsButtonDown(BTN_BACK) && _lastGamePadStates[1].IsButtonUp(BTN_BACK) ||
                   CurrentGamePadStates[2].IsButtonDown(BTN_BACK) && _lastGamePadStates[2].IsButtonUp(BTN_BACK) ||
                   CurrentGamePadStates[3].IsButtonDown(BTN_BACK) && _lastGamePadStates[3].IsButtonUp(BTN_BACK);
        }

        #endregion

        #region GameInput

        public static bool GameLeftPressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(STICK_LEFT) ||
                   CurrentGamePadStates[id].IsButtonDown(DPAD_LEFT);
        }

        public static bool GameUpPressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(STICK_UP) ||
                   CurrentGamePadStates[id].IsButtonDown(DPAD_UP);
        }

        public static bool GameRightPressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(STICK_RIGHT) ||
                   CurrentGamePadStates[id].IsButtonDown(DPAD_RIGHT);
        }

        public static bool GameDownPressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(STICK_DOWN) ||
                   CurrentGamePadStates[id].IsButtonDown(DPAD_DOWN);
        }

        public static bool GameEatPressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(BTN_EAT);
        }

        public static bool GameRunPressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(BTN_RUN);
        }

        public static bool GameMakeSoundPressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(BTN_MAKESOUND) &&
                   _lastGamePadStates[id].IsButtonUp(BTN_MAKESOUND);
        }

        public static bool GameChoosePressed(int id)
        {
            return CurrentGamePadStates[id].IsButtonDown(BTN_CHOOSE) &&
                   _lastGamePadStates[id].IsButtonUp(BTN_CHOOSE);
        }

        #endregion
    }
}
