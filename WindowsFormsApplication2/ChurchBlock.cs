using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class ChurchBlock : Block
    {
        public override void StopAction(ref Player player)
        {
            player.State = PlayerState.Stop;
        }
    }
}
