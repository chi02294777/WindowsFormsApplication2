using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    class StartBlock : Block
    {
        private Random random;

        public override void StopAction(ref Player player)
        {
            player.State = PlayerState.Normal;
            random = new Random();
            int r = random.Next(2) + 1;
            
            if (r == 1)  player.State = PlayerState.SpeedUp;
            else if (r == 2)  player.Money = player.Money + 1000;
        }

        public override void PassAction(ref Player player)
        {
            //player.Money = player.Money + 50;
        }
    }
}
