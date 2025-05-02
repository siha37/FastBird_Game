using Assets.MyFolder._01.Script._02.Object.Player;
using Assets.MyFolder._01.Script._02.Object.Player.Module;
using MoreMountains.Feedbacks;
using MyFolder._01.Script._02.Object.Player.State;
using MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Player.Module.child
{
    public class PlayerFeelControllModule : IPlayerModule
    {
        private PlayerController player;
        public void Init(PlayerController player)
        {
            this.player = player;
        }

        public void OnEnable()
        {
        }

        public void OnDisable()
        {
        }

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
            if (oldstate is DashState && newstate is IdleState)
            {
                player.DashMf.StopFeedbacks();
                player.DashToIdleMf.PlayFeedbacks();
            }
            else if (newstate is DashState)
            {
                player.DashToIdleMf.StopFeedbacks();
                player.DashMf.PlayFeedbacks();
            }
        }
    }
}
