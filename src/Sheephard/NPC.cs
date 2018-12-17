using Microsoft.Xna.Framework;

namespace Sheephard
{
    public class Npc : PlayerObject
    {
        private double _aiStanceElapsedTime;
        private double _aiStanceWaitTime;

        public Npc(int playerId, Vector2 position = new Vector2(), int gamePadId = -1) : base(playerId, position)
        {
        }


        public override void Init()
        {
            base.Init();
            _aiStanceElapsedTime = 0;
            _aiStanceWaitTime = 0;
        }

        public override void DoStupidThings()
        {
            Headbanging();
        }

        public void Headbanging()
        {
            CurrentState = State.Headbanging;
            _aiStanceWaitTime = RandomNumber.Between(2000, 3000);
        }

        public override bool IsNpc()
        {
            return true;
        }

        protected override void GetInput(GameTime gameTime)
        {

            _aiStanceElapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_aiStanceElapsedTime >= _aiStanceWaitTime)
            {
                _aiStanceElapsedTime = 0;
                _aiStanceWaitTime = RandomNumber.Between(2000, 3000);

                var n = RandomNumber.Between(0, 100);

                if (n >= 0 && n <= 30)
                    CurrentState = State.Eating;
                else if (n > 30 && n <= 60)
                    CurrentState = State.Idle;
                else
                {

                    CreateMovement();

                    if (n > 60 && n <= 75)
                    {
                        CurrentState = State.Running;
                        _movement *= RUNNING_SPEED;
                    }
                    else
                    {
                        CurrentState = State.Walking;
                        _movement *= WALKING_SPEED;
                    }
                }


            }
        }

        private void CreateMovement()
        {
            var n = RandomNumber.Between(0, 7);

            if (n == 0)
            {
                _movement.X = 1;
                _movement.Y = 1;
                Sheep.LookDirectionLeft = false;
            }
            if (n == 1)
            {
                _movement.X = 1;
                _movement.Y = 0;
                Sheep.LookDirectionLeft = false;
            }
            if (n == 2)
            {
                _movement.X = 1;
                _movement.Y = -1;
                Sheep.LookDirectionLeft = false;
            }
            if (n == 3)
            {
                _movement.X = 0;
                _movement.Y = 1;
            }
            if (n == 4)
            {
                _movement.X = 0;
                _movement.Y = -1;
            }
            if (n == 5)
            {
                _movement.X = -1;
                _movement.Y = 1;
                Sheep.LookDirectionLeft = true;
            }
            if (n == 6)
            {
                _movement.X = -1;
                _movement.Y = 0;
                Sheep.LookDirectionLeft = true;
            }
            if (n == 7)
            {
                _movement.X = -1;
                _movement.Y = -1;
                Sheep.LookDirectionLeft = true;
            }
        }
    }
}
