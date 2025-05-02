using System;

namespace MyFolder._01.Script._02.Object.Player.State.child
{
    class IdleState : IPlayerState
    {
        public void Enter(PlayerController player)
        {
        }
        public void Update()
        {
        }
        public void Exit()
        {
        }
        public string GetName()
        {
            return this.GetType().Name;
        }
    }
}
