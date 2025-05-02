namespace MyFolder._01.Script._02.Object.Player.State.child
{
    public class StartState :IPlayerState 
    {
        PlayerController player;
        public void Enter(PlayerController player)
        {
            this.player = player;
            player.moduleAble = false;
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        public void Exit()
        {
            player.moduleAble = true;
        }

        public string GetName()
        {
            return this.GetType().Name;
        }
    }
}