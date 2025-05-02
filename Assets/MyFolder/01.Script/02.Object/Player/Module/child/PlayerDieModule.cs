using Assets.MyFolder._01.Script._02.Object.Player;
using Assets.MyFolder._01.Script._02.Object.Player.Module;
using Assets.MyFolder._01.Script._02.Object.Player.Module.child;
using MyFolder._01.Script._02.Object.Player.State;
using MyFolder._01.Script._02.Object.Player.State.child;
using Unity.VisualScripting;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Player.Module.child
{
    public class PlayerDieModule : IPlayerModule
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
            if (newstate is DieState)
            {
                if(!player.DieMf.IsPlaying)
                    player.DieMf.PlayFeedbacks();
                player.SetBackgroundSpeed(0);
                player.GetModule<PlayerGravityModule>().SetGravityDie();
                //Time.timeScale = 0;
            }
            else if(oldstate is DieState)
            {
                
            }
        }
    }
}