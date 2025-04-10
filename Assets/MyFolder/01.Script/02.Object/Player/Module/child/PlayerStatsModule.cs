using Assets.MyFolder._01.Script._02.Object.Player.State;
using Assets.MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module.child
{
    class PlayerStatsModule : IPlayerModule
    {
        public float DashMutiple { get; set; } = 2f;
        [SerializeField] private float jumpForce = 3f;
        public float JumpForce => jumpForce;

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
        }

        public void Init(PlayerController player)
        {
        }

        public void OnDisable()
        {
        }

        public void OnEnable()
        {
        }
    }
}
