using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction.Actions
{
    public class URLOpenAction : UIButtonAction
    {
        public string URL;
        public override void Execute(GameObject sender)
        {
            Application.OpenURL(URL);
        }
    }
}