using UnityEngine;

namespace MyFolder._01.Script._03.UI.Elemental.TOP
{
    public abstract class BaseUI : MonoBehaviour
    {
        public virtual void OnOpen()
        {
            gameObject.SetActive(true);
        }

        public virtual void OnClose()
        {
            gameObject.SetActive(false);
        }
    
        public virtual bool BlocksGameplayInput => false;

        public virtual bool RequiresPause => false;
    }
}
