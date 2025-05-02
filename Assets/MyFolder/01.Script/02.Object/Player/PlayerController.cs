using System.Collections.Generic;
using Assets.MyFolder._01.Script._02.Object.Player.Module;
using Assets.MyFolder._01.Script._02.Object.Player.Module.child;
using MoreMountains.Feedbacks;
using MyFolder._01.Script._01.Manager;
using MyFolder._01.Script._02.Object.Player.Module.child;
using MyFolder._01.Script._02.Object.Player.State;
using MyFolder._01.Script._02.Object.Player.State.child;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace MyFolder._01.Script._02.Object.Player
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
        /********************���� ���� ����********************/
        #region MODULE

        public bool moduleAble = false; 
        Dictionary<System.Type, IPlayerModule> modules = new Dictionary<System.Type, IPlayerModule>();
        List<IPlayerTickableModule> tickableModules = new List<IPlayerTickableModule>();
        List<IPlayerColliderModule> colliderModules = new List<IPlayerColliderModule>();
        
        #endregion

        #region STATE

        PlayerStateMachine stateMachine;

        #endregion

        #region Inspector

        [Header("State")]
        [SerializeField] string currentStateName;
        public InputActionAsset inputActionAsset;

        #endregion

        #region PROPERTY
        private float backgroundSpeed = 1f;
        [SerializeField] private float dashMultiplier = 3f; 
        [SerializeField] private MMF_Player dashMf;
        public MMF_Player DashMf => dashMf;
        [SerializeField] private MMF_Player dashToIdleMf;
        public MMF_Player DashToIdleMf => dashToIdleMf;
        [SerializeField] private MMF_Player dieMf;  
        public MMF_Player DieMf => dieMf;
        

        public ScoreManager scoreManager;

        [SerializeField] private SpriteRenderer spriteObject;
        public SpriteRenderer SpriteObject => spriteObject; 
        
        #endregion
        
        /********************  ********************/
        #region INIT
        private void Start()
        {
            ModuleInt();
            StateInit();
        }

        #endregion

        /********************Ʈ  Լ********************/
        #region UPDATE
        private void Update()
        {
            if (moduleAble)
            {
                foreach (var state in tickableModules)
                {
                    state.Update();
                }
            }
        }
        private void FixedUpdate()
        {
            if (moduleAble)
            {
                foreach (var state in tickableModules)
                {
                    state.FixedUpdate();
                }
            }
        }
        #endregion

        /********************Public  Լ*********************/
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
            else if (module is IPlayerColliderModule colliderable)
                colliderModules.Add(colliderable);
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
        
        #region  SPEEDRETURN

        public void SetBackgroundSpeed(float speed)
        {
            backgroundSpeed = speed;
            // 배경과 파이프의 속도를 변경하는 로직 추가
            // 예: BackgroundController.Instance.SetSpeed(speed);
            // 예: PipeSpawner.Instance.SetSpeed(speed);
            //Debug.Log($"[PlayerController] Background speed set to: {speed}");
        }

        public float GetBackgroundSpeed()
        {
            if(GetCurrentState() == nameof(DashState))
                return backgroundSpeed * dashMultiplier;
            return backgroundSpeed;
        }

        #endregion

        /********************Private  Լ**********************/

        #region MODULE

        private void ModuleInt()
        {
            AddModule<PlayerStatsModule>();
            AddModule<PlayerMovement>();
            AddModule<PlayerInputModule>();
            AddModule<AnimationController>();
            AddModule<PlayerGravityModule>();
            AddModule<PlayerCameraAreaModule>();
            AddModule<PlayerHitModule>();
            AddModule<PlayerFeelControllModule>();
            AddModule<PlayerDieModule>();
        }

        #endregion

        #region STATE

        private void StateInit()
        {
            stateMachine = new PlayerStateMachine(this);
            SetPlayerState<StartState>();
        }

        #endregion
        
        #region COLLIDER
        
        private void OnTriggerEnter2D(Collider2D col)
        {
            foreach (IPlayerColliderModule colliderModule in colliderModules)
            {
                colliderModule.OnTriggerEnter2D(col);
            }
        }

        private void OnTriggerExit2D(Collider2D col)
        {
            foreach (IPlayerColliderModule colliderModule in colliderModules)
            {
                colliderModule.OnTriggerExit2D(col);
            }
        }
        #endregion
    }
}