using MyFolder._01.Script._03.UI.ButtonAction;
using MyFolder._01.Script._03.UI.ButtonAction.Actions;
using UnityEngine;

namespace MyFolder._01.Script._00.Game
{
    public class ApplicationPause : MonoBehaviour
    {
        [SerializeField] private  OpenUIAction openUIAction;
        void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                  //TODO: 일시정지 처리
                  openUIAction.Execute(gameObject);
            } 
        }
    }
}
