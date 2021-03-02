using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class PayMoneyBlock : Block
    {
        public override void StopAction(ref Player player)
        {
            player.Money = player.Money - 200;
        }
    }
}
