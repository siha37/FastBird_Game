using GoogleMobileAds.Api;
using UnityEngine;

namespace MyFolder._01.Script._1001.Admob
{
    public class GoogleAdmobInitialize : MonoBehaviour
    {
        public void Start()
        {
            MobileAds.Initialize(status =>
            {
            
            });
        }
    }
}
