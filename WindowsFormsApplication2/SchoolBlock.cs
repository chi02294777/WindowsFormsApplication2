using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class SchoolBlock : Block
    {
        public override void StopAction(ref Player player)
        {
            player.State = PlayerState.Normal;
        }
    }
}
