namespace MyFolder._01.Script._02.Object.Player.State
{
    public interface IPlayerState
    {
        public void Enter(PlayerController player);
        public void Update();
        public void Exit();
        public string GetName();
    }
}