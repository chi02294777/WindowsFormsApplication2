using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    enum TurnPhase
    {
        Initial,
        Walk,
        Dice,
        End
    }

    enum PlayerState
    {
        Normal,
        SpeedUp,
        SpeedDown,
        Stop,
        Backward,
        Buy,
        Sell,
        DiceAgain
    }

}
