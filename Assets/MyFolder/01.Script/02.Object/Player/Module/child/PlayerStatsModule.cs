using Assets.MyFolder._01.Script._02.Object.Player.State;
using Assets.MyFolder._01.Script._02.Object.Player.State.child;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module.child
{
    class PlayerStatsModule : IPlayerModule
    {
        public float BaseSpeed { get; set; } = 5f;
        public float CurrentSpeed { get; set; } = 0f;
        public float DashMutiple { get; set; } = 2f;

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
            // Handle state change logic here
            // For example, you might want to adjust player stats based on the new state
            if (newstate is MoveState Mstate)
            {
                // Example: Adjust speed based on player state
                CurrentSpeed = BaseSpeed;
            }
            else if(newstate is DashState Dstate)
            {
                CurrentSpeed = BaseSpeed * DashMutiple; // Example: Double speed for dash state
            }
        }

        

        public void Init(PlayerController player)
        {
        }

        public void OnDisable()
        {
            throw new System.NotImplementedException();
        }

        public void OnEnable()
        {
            throw new System.NotImplementedException();
        }

    }
}
