using MyFolder._01.Script._98.Loading;
using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction.Actions
{
    [System.Serializable]
    public class LoadSceneAction : UIButtonAction
    {
        public string sceneName;

        public override void Execute(GameObject sender)
        {
            if (!string.IsNullOrEmpty(sceneName))
                LoadingScene.LoadScene(sceneName);
        }
    }
}