using System.Collections.Generic;
using MyFolder._01.Script._02.Object.Player;
using MyFolder._01.Script._03.UI.Elemental.TOP;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace MyFolder._01.Script._03.UI
{
    public class UIManager : DieSingleTone<UIManager>
    {
        private static Stack<BaseUI> uiStack = new Stack<BaseUI>();

        [SerializeField] private InputActionAsset actionAsset;
        private InputAction backAction;
        [SerializeField] private PlayerController playerController;

        private void Start()
        {
            KeyBinding();
            playerController = FindFirstObjectByType<PlayerController>();
            SceneManager.sceneUnloaded += SceneUnload;
        }

        private void SceneUnload(Scene scene)
        {
            StackReset();
        }

        private void KeyBinding()
        {
            var map = actionAsset.FindActionMap("UI");
            backAction = map.FindAction("Cancel");
            backAction.started += HandleBackButton;
        }

        void Update()
        {
            // OLD InputSystem None
            /*if (Input.GetKeyDown(KeyCode.Escape))
            {
                HandleBackButton();
            }*/
        }

        public void OpenUI(BaseUI ui)
        {
            /*
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnClose();
            }*/

            if (ui.RequiresPause)
            {
                Time.timeScale = 0f;
            }
            if(!playerController)
                playerController = FindFirstObjectByType<PlayerController>();
            if (ui.BlocksGameplayInput && playerController)
            {
                playerController.BlockInput(true);
            }
            ui.OnOpen();
            uiStack.Push(ui);
        }

        public void HandleBackButton(InputAction.CallbackContext context)
        {
            HandleBackButton();
        }
        public void HandleBackButton()
        {
            if (uiStack.Count == 0)
            {
                return;
            }

            BaseUI top = uiStack.Pop();
            top.OnClose();
            
            Debug.Log("BackButton" + top.gameObject.name);
            if (top.RequiresPause)
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            if (top.BlocksGameplayInput && playerController)
            {
                playerController.BlockInput(false);
            }
            if (uiStack.Count > 0)
            {
                Debug.Log(uiStack.Peek().gameObject);
                uiStack.Peek().OnOpen(); // 상태 복구
            }
        }

        private void StackReset()
        {
            uiStack.Clear();
        }
        
        private void ConfirmExit()
        {
            Debug.Log("App Exit Confirm");
        }
    }
}
