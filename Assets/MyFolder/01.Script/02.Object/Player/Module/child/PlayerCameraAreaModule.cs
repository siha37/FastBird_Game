using Assets.MyFolder._01.Script._02.Object.Player;
using Assets.MyFolder._01.Script._02.Object.Player.Module;
using MyFolder._01.Script._02.Object.Player.State;
using MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Player.Module.child
{
    public class PlayerCameraAreaModule : IPlayerTickableModule
    {
        Camera mainCamera;
        private Transform myTf;
        PlayerController playerController;
        public void Init(PlayerController player)
        {
            mainCamera = Camera.main;    
            playerController = player;
            myTf = player.transform;
        }

        public void OnEnable()
        {
        }

        public void OnDisable()
        {
        }

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
        }

        public void Update()
        {
            if (playerController.GetCurrentState() != nameof(DieState))
            {
                Vector3 viewPos = mainCamera.WorldToViewportPoint(myTf.position);

                if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1)
                {
                    //카메라 영역 밖으로 나감
                    //Fail
                    playerController.SetPlayerState<DieState>();
                }
            }
        }

        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
        }
    }
}