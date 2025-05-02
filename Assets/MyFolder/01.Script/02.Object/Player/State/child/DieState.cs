namespace MyFolder._01.Script._02.Object.Player.State.child
{
    public class DieState : IPlayerState
    {
        private PlayerController player;
        public void Enter(PlayerController player)
        {
            GameManager.Instance.StopGame();
            this.player = player;
            player.moduleAble = false;
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