using MyFolder._01.Script._03.UI.Elemental.TOP;
using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction.Actions
{
    [System.Serializable]
    public class OpenUIAction : UIButtonAction
    {
        public BaseUI targetUI;

        public override void Execute(GameObject sender)
        {
            if (targetUI != null)
                UIManager.Instance.OpenUI(targetUI);
        }
    }
}