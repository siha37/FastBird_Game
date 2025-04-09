using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Player.State
{
    public interface IPlayerState
    {
        public void Enter();
        public void Update();
        public void Exit();
        public string GetName();
    }
}