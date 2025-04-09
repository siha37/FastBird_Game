using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MyFolder._01.Script._02.Object.Player.Module
{
    interface IPlayerTickableModule : IPlayerModule
    {
        public void Update();
        public void FixedUpdate();
        public void LateUpdate();
    }
}
