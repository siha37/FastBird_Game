using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.RemoteConfig;
using MyFolder._01.Script._98.Loading;
using UnityEngine;

namespace MyFolder._01.Script._00.Game
{
    public class FirebaseVersionChecker : MonoBehaviour
    {
        
        [SerializeField] private GameObject versionPopup;
        private const string VersionKey = "lastest_version";

        void Start()
        {
            StartCoroutine(CheckVersionCoroutine());
        }

        IEnumerator CheckVersionCoroutine()
        {
            yield return InitializeFirebase();
            string latestVersion = FirebaseRemoteConfig.DefaultInstance.GetValue(VersionKey).StringValue;
            string currentVersion = Application.version;

            Debug.Log($"Current: {currentVersion}, Latest: {latestVersion}");
            
            if (IsNewerVersion(latestVersion, currentVersion))
            {
                ShowUpdatePopup();
            }
            else
            {
                yield return new WaitForSeconds(2);
                LoadingScene.LoadScene("StartScene");
            }
        }

        private IEnumerator InitializeFirebase()
        {
            var dependencyTask = FirebaseApp.CheckAndFixDependenciesAsync();
            yield return new WaitUntil(() => dependencyTask.IsCompleted);

            FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(new System.Collections.Generic.Dictionary<string, object>
            {
                { VersionKey, "1.0.0" } // 기본값
            });

            var fetchTask = FirebaseRemoteConfig.DefaultInstance.FetchAsync(System.TimeSpan.Zero);
            yield return new WaitUntil(() => fetchTask.IsCompleted);

            if (fetchTask.Exception == null)
            {
                FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
            }
        }

        private bool IsNewerVersion(string serverVersion, string currentVersion)
        {
            System.Version vServer = new System.Version(serverVersion);
            System.Version vLocal = new System.Version(currentVersion);
            return vServer > vLocal;
        }

        private void ShowUpdatePopup()
        {
            Debug.Log("🔥 새 버전이 있습니다! 업데이트를 유도하세요.");
            // 팝업 UI 띄우기, 또는 Store URL로 연결
            versionPopup.SetActive(true);
        }
    }
}
