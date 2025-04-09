using Assets.MyFolder._01.Script._02.Object.Player.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module.child
{
    class PlayerInputModule : IPlayerTickableModule
    {
        PlayerController player;
        private InputAction moveAction;
        private InputAction dashAction;

        void IPlayerModule.ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {

        }

        void IPlayerTickableModule.FixedUpdate()
        {
         
        }

        void IPlayerModule.Init(PlayerController player)
        {
            this.player = player;
            var playerInput = player.GetComponent<PlayerInput>();

            moveAction = playerInput.actions["Move"];
            dashAction = playerInput.actions["Dash"];
        }

        void IPlayerTickableModule.LateUpdate()
        {
            
        }

        void IPlayerModule.OnDisable()
        {
            
        }

        void IPlayerModule.OnEnable()
        {
            
        }

        void IPlayerTickableModule.Update()
        {
            
        }
    }
}
