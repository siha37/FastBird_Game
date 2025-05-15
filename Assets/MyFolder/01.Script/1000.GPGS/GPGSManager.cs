using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;

public class GPGSManager : MonoBehaviour
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
            string name = PlayGamesPlatform.Instance.GetUserDisplayName();
            string id = PlayGamesPlatform.Instance.GetUserId();
            string ImgUrl = PlayGamesPlatform.Instance.GetUserImageUrl();
            
            logtext.text = "Success \n" + name;        
        }
        else
        {
            logtext.text = "Fail \n";
            
            //Disable your intergration with play Games Services or show a login button
            //to ask user to sign-in. Clicking it should call
            //PlayGamesPlatform.Instance.ManuallAuthenticate(ProcessAuthentication)
            //Login failed
        }
    }
}
