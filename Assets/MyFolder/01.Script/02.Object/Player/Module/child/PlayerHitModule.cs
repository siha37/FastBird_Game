using Assets.MyFolder._01.Script._02.Object.Player;
using Assets.MyFolder._01.Script._02.Object.Player.Module;
using MyFolder._01.Script._02.Object.Player.State;
using MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;

namespace MyFolder._01.Script._02.Object.Player.Module.child
{
    public class PlayerHitModule : IPlayerColliderModule 
    {
        PlayerController playerController;
        private readonly string targetTag = "Obstacle";
        private readonly string scoreTag = "ScoreCollision";
        private bool collisionAble = true;

        public void Init(PlayerController player)
        {
            playerController = player;
        }

        public void OnEnable()
        {
            
        }

        public void OnDisable()
        {
        }

        public void ChangedState(IPlayerState oldstate, IPlayerState newstate)
        {
            if (oldstate is DieState)
            {
                collisionAble = false;
            }
            else if(oldstate is StartState)
            {
                collisionAble = true;
            }
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
        }

        public void OnCollisionStay2D(Collision2D collision)
        {
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (!collisionAble)
                return;
            if (other.CompareTag(targetTag))
            {
                //Fail
                playerController.SetPlayerState<DieState>();
            }
            else if (other.CompareTag(scoreTag))
            {
                playerController.scoreManager.Score_OnPointUp();
            }
        }

        public void OnTriggerExit2D(Collider2D other)
        {
            if (!collisionAble)
                return;
            if (other.CompareTag(targetTag))
            {
                //Exit
            }
            else if (other.CompareTag(scoreTag))
            {
                
            }
        }

        public void OnTriggerStay2D(Collider2D other)
        {
        }
    }
}
