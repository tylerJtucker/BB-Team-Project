using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class PowerUps
    {
        
    }

    public class extraLife
    {
        
        public extraLife()
        {
            GameScreen.ChangeLives(1);

        }

    }

    public class ballCatch
    {

        public ballCatch()
        {

        }

    }
     public class multiBall
    {

        public multiBall()
        {

        }
           
    }


    public class paddleBig
    {

        public paddleBig()
        {
            GameScreen.ChangePaddle(10); // stretch paddle by 10 pixels
        }

    }

    public class paddleSmall
    {

        public paddleSmall()
        {
            GameScreen.ChangePaddle(-10); // shrink paddle by 10 pixels
        }

    }

    public class slowDown
    {

        public slowDown()
        {
            GameScreen.ChangeSpeeds(-1, -1, -1);  // -1 for testing    (xSpeed, ySpeed, paddleSpeed)

        }

    }
}
