using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction.Actions
{
    [System.Serializable]
    public class QuitGameAction : UIButtonAction
    {
        public override void Execute(GameObject sender)
        {
            Application.Quit();
        }
    }
}
