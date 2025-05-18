using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MyFolder._01.Script._1001.Admob
{
    public class RetryRewardADCount : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI adCountText;
        private const int MaxCount = 1;
        private int currentCount = MaxCount;
        private Button button;

        void Start()
        {
            adCountText.text = currentCount.ToString() + "/" + MaxCount.ToString();
            button = GetComponent<Button>();
        }

        public void UseRewardedAd()
        {
            currentCount--;
            if(currentCount == 0)
                button.interactable = false;
            adCountText.text = currentCount.ToString() + "/" + MaxCount.ToString(); 
        }
    }
}
