using MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._02.Object.Player.State;
using MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module.child
{
    /// <summary>
    /// AnimationController class
    /// </summary>
    /// <remarks>
    /// This class is responsible for controlling the player's animations.
    /// </remarks>
    /// <author>Jin</author>
    /// <date>2023/10/12</date>
    /// <version>1.0</version>
    /// <see cref="IPlayerModule"/>
    public class AnimationController : IPlayerTickableModule
    {
        PlayerController player;
        Animator animator;

        public void FixedUpdate()
        {
        }

        public void Init(PlayerController player)
        {
            this.player = player;
            player.TryGetComponent(out animator);
        }

        public void LateUpdate()
        {
        }

        public void OnDisable()
        {
        }

        public void OnEnable()
        {
        }

        public void Update()
        {
        }

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
            if(newstate is MoveState)
            {
                
            }
        }

    }
}