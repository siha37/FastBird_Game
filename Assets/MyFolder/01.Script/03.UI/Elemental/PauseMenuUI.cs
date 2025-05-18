using MyFolder._01.Script._03.UI.Elemental.TOP;
using UnityEngine;

namespace MyFolder._01.Script._03.UI.Elemental
{
    public class PauseMenuUI : BaseUI
    {

        public override void OnOpen()
        {
            base.OnOpen();            
        }

        public override void OnClose()
        {
            base.OnClose();
        }
        
        public override bool BlocksGameplayInput => true;
        public override bool RequiresPause => true;
    }
}