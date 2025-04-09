using Assets.MyFolder._01.Script._02.Object.Player.Module;
using Assets.MyFolder._01.Script._02.Object.Player.Module.child;
using Assets.MyFolder._01.Script._02.Object.Player.State;
using Assets.MyFolder._01.Script._02.Object.Player.State.child;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;

namespace Assets.MyFolder._01.Script._02.Object.Player
{
    /// <summary>
    /// PlayerController class
    /// </summary>
    /// <remarks>
    /// This class is responsible for controlling the player's state and modules.
    /// </remarks>
    /// <author>Jin</author>
    /// <date>2023/10/12</date>
    /// <version>1.0</version>
    /// <see cref="MonoBehaviour"/>
    public class PlayerController : MonoBehaviour
    {
        /********************전역 변수 선언********************/
        #region MODULE

        Dictionary<System.Type, IPlayerModule> modules = new Dictionary<System.Type, IPlayerModule>();
        List<IPlayerTickableModule> tickableModules = new List<IPlayerTickableModule>();

        #endregion

        #region STATE

        PlayerStateMachine stateMachine;

        #endregion

        #region Inspector

        [Header("State")]
        [ReadOnly][SerializeField] string currentStateName;

        #endregion


        /********************초기화 함수**********************/
        #region INIT
        private void Start()
        {
            ModuleInt();
            StateInit();
        }

        private void ModuleInt()
        {
            AddModule<PlayerStatsModule>();
            AddModule<PlayerMovement>();
            AddModule<AnimationController>();
        }

        private void StateInit()
        {
            stateMachine = new PlayerStateMachine(this);
            SetPlayerState<MoveState>();
        }
        #endregion

        /********************업데이트 함수********************/
        #region UPDATE
        private void Update()
        {
            foreach (var state in tickableModules)
            {
                state.Update();
            }
        }
        private void FixedUpdate()
        {
            foreach (var state in tickableModules)
            {
                state.FixedUpdate();
            }
        }
        #endregion

        /********************Public 함수*********************/
        #region MODULE

        public T AddModule<T>() where T : IPlayerModule, new()
        {
            System.Type type = typeof(T);
            if (modules.ContainsKey(type))
            {
                Debug.LogError($"Already Exist Module : {type}");
                return default;
            }
            IPlayerModule module = new T();

            modules.Add(type, module);
            if(module is IPlayerTickableModule tickable)
                tickableModules.Add(tickable);
            module.Init(this);

            return (T)module;
        }

        public void RemoveModule<T>() where T : IPlayerModule
        {
            System.Type type = typeof(T);
            if (modules.ContainsKey(type))
            {
                if (modules[type] is IPlayerTickableModule tickable)
                    tickableModules.Remove(tickable);

                modules.Remove(type);
            }
            else
            {
                Debug.LogError($"Not Exist Module : {type}");
            }
        }

        public T GetModule<T>() where T : IPlayerModule
        {
            System.Type type = typeof(T);
            if (modules.ContainsKey(type))
            {
                return (T)modules[type];
            }
            Debug.LogError($"Not Exist Module : {type}");
            return default;
        }

        #endregion

        #region STATE

        public void SetPlayerState<T>() where T : IPlayerState, new()
        {
            IPlayerState oldState;
            IPlayerState newState;
            stateMachine.ChangeState<T>(out oldState, out newState);
            currentStateName = newState.GetName();
            foreach (var module in modules)
            {
                module.Value.ChangedState(oldState, newState);
            }
        }

        public string GetCurrentState()
        {
            return stateMachine.GetCurrentState();
        }



        #endregion

        /********************Private 함수**********************/

        #region MODULE

        #endregion
    }
}