using Assets.MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;

namespace Assets.MyFolder._01.Script._02.Object.Player.State
{
    /// <summary>
    /// PlayerStateMachine class
    /// </summary>
    /// <remarks>
    /// This class is responsible for managing the player's state machine.
    /// </remarks>
    /// <author>Jin</author>
    /// <date>2023/10/12</date>
    /// <version>1.0</version>
    /// <see cref="IPlayerState"/>
    public class PlayerStateMachine
    {
        /********************전역 변수 선언********************/

        PlayerController player;

        IPlayerState currentState;


        /*********************생성자**************************/

        public PlayerStateMachine(PlayerController player)
        {
            this.player = player;
        }

        /********************초기화 함수**********************/

        private void Start()
        {

        }

        /********************업데이트 함수********************/

        private void Update()
        {
            currentState?.Update();
        }

        /********************Public 함수*********************/
        public void ChangeState<T>(out IPlayerState oldState, out IPlayerState newState) where T : IPlayerState, new()
        {
            newState = new T();
            oldState = currentState;
            currentState?.Exit();
            currentState = newState;
            currentState.Enter();


        }

        public string GetCurrentState()
        {
            return currentState.GetName();
        }

        /********************Private 함수********************/


    }
}