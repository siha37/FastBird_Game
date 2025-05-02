using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolder._01.Script._98.Loading
{
    public class LoadingScene : MonoBehaviour
    {
        public static string NextScene;
        [SerializeField] private Image progressBar;

        public static void LoadScene(string sceneName)
        {
            NextScene = sceneName;
            UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingScene");
        }
        private void Start()
        {
            StartCoroutine(LoadScene());
        }
        IEnumerator LoadScene()
        {
            yield return null;

            AsyncOperation op = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(NextScene);
            op.allowSceneActivation = false;
            
            Time.timeScale = 1;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
            
            float timer = 0f;
            
            while(!op.isDone)
            {
                yield return null;
                timer += Time.deltaTime;
                if(op.progress < 0.9f)
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, op.progress, timer);
                    if(progressBar.fillAmount >= op.progress)
                    {
                        timer = 0f;
                    }
                }
                else
                {
                    progressBar.fillAmount = Mathf.Lerp(progressBar.fillAmount, 1f, timer);
                    if (Mathf.Approximately(progressBar.fillAmount, 1.0f))
                    {
                        op.allowSceneActivation = true;
                        yield break;
                    }
                }
            }
        }
    }
}