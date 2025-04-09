using System;

namespace Assets.MyFolder._01.Script._02.Object.Player.State.child
{
    class IdleState : IPlayerState
    {
        public void Enter()
        {
            Console.WriteLine("IdleState Enter");
        }
        public void Update()
        {
            Console.WriteLine("IdleState Update");
        }
        public void Exit()
        {
            Console.WriteLine("IdleState Exit");
        }
        public string GetName()
        {
            return this.GetType().Name;
        }
    }
}
