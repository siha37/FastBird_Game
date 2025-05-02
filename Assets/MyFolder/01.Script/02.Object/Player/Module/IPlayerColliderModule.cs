using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module
{
    public interface IPlayerColliderModule : IPlayerModule
    {
        public void OnCollisionEnter2D(Collision2D collision);
        public void OnCollisionExit2D(Collision2D collision);
        public void OnCollisionStay2D(Collision2D collision);
        public void OnTriggerEnter2D(Collider2D other);
        public void OnTriggerExit2D(Collider2D other);
        public void OnTriggerStay2D(Collider2D other);
    }
}