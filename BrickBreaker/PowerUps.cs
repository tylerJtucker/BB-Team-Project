using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BrickBreaker
{
    public class PowerUps 
    {
        public int x;
        public int y;
        public string name;

        public PowerUps(int _x, int _y, string _name)
        {
            x = _x;
            y = _y;
            name = _name;
        }
        public void Move ()
        {
            y += 5;
        }
        public bool Collision (Paddle p)
        {
            Rectangle a = new Rectangle(x, y, 5, 5);
            Rectangle b = new Rectangle(p.x, p.y, p.width, p.height);

            if (a.IntersectsWith(b))
            {
                return true;
            }
            return false;
        }
        public void collide(Paddle p)
        {
            //use switch statement to check which powerup is colliding and do stuff
            switch (name)
            {
                case "catchyBoi":

                    break;

                case "multiBoi":
                   // GameScreen.Ball
                    break;

                case "lifeBoi":
                    GameScreen.ChangeLives(1); // should input 1 more life into kirans code    SHOULD
                    break;

                case "smallBoi":
                    GameScreen.ChangePaddle(-20);
                    break;

                case "enlargedBoi":
                    GameScreen.ChangePaddle(40);
                    break;

                case "slowBoi":
                    GameScreen.ChangeSpeeds(-2, -2, -2);
                    break;

                case "fastBoi":
                    GameScreen.ChangeSpeeds(3, 3, 3);
                    break;

            }

        }

    }

    public class blankClass
    {

       
    }

    //public class extraLife : PowerUps
    //{
        
    //    public extraLife(int _x, int _y)
    //    {
    //        GameScreen.ChangeLives(1);

    //    }

    //}

    //public class ballCatch
    //{

    //    public ballCatch(int _x, int _y)
    //    {

    //    }

    //}
    // public class multiBall
    //{

    //    public multiBall()
    //    {

    //    }
           
    //}


    //public class paddleBig
    //{

    //    public paddleBig()
    //    {
    //        GameScreen.ChangePaddle(10); // stretch paddle by 10 pixels
    //    }

    //}

    //public class paddleSmall
    //{

    //    public paddleSmall()
    //    {
    //        GameScreen.ChangePaddle(-10); // shrink paddle by 10 pixels
    //    }

    //}

    //public class slowDown
    //{

    //    public slowDown()
    //    {
    //        GameScreen.ChangeSpeeds(-1, -1, -1);  // -1 for testing    (xSpeed, ySpeed, paddleSpeed)

    //    }

    //}
}
