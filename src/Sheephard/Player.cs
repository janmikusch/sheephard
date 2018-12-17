using Microsoft.Xna.Framework;

namespace Sheephard
{
    public class Player : PlayerObject
    {
        private readonly int _gamePadId;
        //do not spam bleaaa
        private double _soundWaitTime;


        public Player(int playerId, int gamePadId, Vector2 position = new Vector2()) : base(playerId, position)
        {
            _gamePadId = gamePadId;
        }

        public override bool IsNpc()
        {
            return false;
        }

        public override void DoStupidThings()
        {
            //do nothing
        }

        public override void Init()
        {
            base.Init();
            _soundWaitTime = 0;
        }

        protected override void GetInput(GameTime gameTime)
        {
            CurrentState = State.Idle;
            _movement = new Vector2();

            if (_soundWaitTime <= 0)
            {
                if (InputManager.GameMakeSoundPressed(_gamePadId))
                {
                    GameController.SheepSounds[RandomNumber.Between(0, 1)].Play();
                    _soundWaitTime = 3000D;
                }
            }
            else
            {
                _soundWaitTime -= gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            //Debug.WriteLine(_soundWaitTime);


            if (InputManager.GameEatPressed(_gamePadId))
            {
                CurrentState = State.Eating;
                return;
            }
            if (InputManager.GameChoosePressed(_gamePadId) && Sheep.IsBlack)
            {
                GameController.ChooseSelected();
            }

            if (InputManager.GameLeftPressed(_gamePadId) ||
                InputManager.GameUpPressed(_gamePadId) ||
                InputManager.GameRightPressed(_gamePadId) ||
                InputManager.GameDownPressed(_gamePadId)
            )
            {
                CurrentState = InputManager.GameRunPressed(_gamePadId) ? State.Running : State.Walking;
            }

            if (InputManager.GameLeftPressed(_gamePadId))
            {
                _movement.X += -1;
                Sheep.LookDirectionLeft = true;
            }
            if (InputManager.GameRightPressed(_gamePadId))
            {
                _movement.X += 1;
                Sheep.LookDirectionLeft = false;
            }
            if (InputManager.GameUpPressed(_gamePadId))
            {
                _movement.Y += -1;
            }
            if (InputManager.GameDownPressed(_gamePadId))
            {
                _movement.Y += 1;
            }


            if (Movement.LengthSquared() <= 0)
                CurrentState = State.Idle;

            switch (CurrentState)
            {
                case State.Running:
                    _movement *= RUNNING_SPEED;
                    break;

                case State.Walking:
                    _movement *= WALKING_SPEED;
                    break;
            }
        }
    }
}
