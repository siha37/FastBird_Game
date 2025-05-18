using MyFolder._01.Script._01.SingleTone;
using UnityEngine;

namespace MyFolder._01.Script._03.UI.ButtonAction.Actions
{
    [System.Serializable]
    public class CallAdmobAction : UIButtonAction
    {
        [SerializeField] private GoogleRewardADManager rewardADManager;
        public override void Execute(GameObject sender)
        {
            rewardADManager.OnUserPressedRevive(StageManager.Instance.ReStart); 
        }
    }
}
