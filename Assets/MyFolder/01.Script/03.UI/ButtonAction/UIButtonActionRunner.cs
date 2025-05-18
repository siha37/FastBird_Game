using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolder._01.Script._03.UI.ButtonAction
{
    public class UIButtonActionRunner : MonoBehaviour
    {
        [SerializeReference]
        public List<UIButtonAction> actions = new();

        private void Awake()
        {
            TryGetComponent(out Button button);
            button.onClick.AddListener(RunActions);
        }

        private void RunActions()
        {
            foreach (var action in actions)
            {
                action?.Execute(gameObject);
            }
        }
    }
}