using MyFolder._01.Script._98.Loading;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName; 
    public void LoadScene(string sceneName)
    {
        LoadingScene.LoadScene(sceneName);
    }
}
