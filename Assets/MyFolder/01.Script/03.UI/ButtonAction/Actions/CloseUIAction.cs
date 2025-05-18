using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction.Actions
{
    [System.Serializable]
    public class CloseUIAction : UIButtonAction
    {
        public override void Execute(GameObject sender)
        {
            UIManager.Instance.HandleBackButton();
        }
    }
}