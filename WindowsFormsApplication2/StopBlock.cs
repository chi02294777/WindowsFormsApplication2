using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class StopBlock : Block
    {
        public override void StopAction(ref Player player)
        {
            player.State = PlayerState.Stop;
        }
    }
}
