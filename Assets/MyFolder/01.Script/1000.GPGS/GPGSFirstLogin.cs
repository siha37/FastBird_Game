using GooglePlayGames;
using GooglePlayGames.BasicApi;
using MyFolder._01.Script._01.SingleTone;
using TMPro;
using UnityEngine;

namespace MyFolder._01.Script._1000.GPGS
{
    public class GPGSFirstLogin : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI logtext;
    
        private void Start()
        {
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
            SignIn();
        }

        public void SignIn()
        {
            PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        }

        internal void ProcessAuthentication(SignInStatus status)
        {
            if (status == SignInStatus.Success)
            {
                SettingSaveManager.Instance.settingData.accountName = PlayGamesPlatform.Instance.GetUserDisplayName();
                SettingSaveManager.Instance.settingData.accountId = PlayGamesPlatform.Instance.GetUserId();
                SettingSaveManager.Instance.AcountImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();
            
                logtext.text = "Success \n" + name;        
            }
            else
            {
                SettingSaveManager s=  SettingSaveManager.Instance;
                logtext.text = "Fail \n";
            
                //Disable your intergration with play Games Services or show a login button
                //to ask user to sign-in. Clicking it should call
                //PlayGamesPlatform.Instance.ManuallAuthenticate(ProcessAuthentication)
                //Login failed
            }
        }
    }
}
