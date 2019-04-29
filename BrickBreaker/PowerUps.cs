using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class PowerUps 
    {
        int x;
        int y;
        string name;

        public PowerUps(int _x, int _y, string _name)
        {
            x = _x;
            y = _y;
            name = _name;
        }
        public void tick(Paddle p)
        {
            //move


            //check if colliding with paddle p
            


            //if colliding call collide method


        }
        public void collide(Paddle p)
        {
            //use switch statement to check which powerup is colliding and do stuff
            switch (name)
            {
                case "catchyBoi":

                    break;

                case "multiBoi":
                    GameScreen.balls.Add(new Ball(GameScreen.balls[0].x + 10, GameScreen.balls[0].y, Math.Abs(GameScreen.balls[0].xSpeed), Math.Abs(GameScreen.balls[0].ySpeed), GameScreen.balls[0].size));
                    GameScreen.balls.Add(new Ball(GameScreen.balls[0].x - 10, GameScreen.balls[0].y, Math.Abs(GameScreen.balls[0].xSpeed), Math.Abs(GameScreen.balls[0].ySpeed), GameScreen.balls[0].size));
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

        public PowerUps randomGenBoi(int _x, int _y)
        {
            Random rnd = new Random();

            int randomNumber = rnd.Next(1, 106);

            if (randomNumber <= 10)
            {
                return new PowerUps(_x, _y, "mutliBoi");
            }
            else if (randomNumber <= 20)
            {
                return new PowerUps(_x, _y, "fastBoi");
            }
            else if (randomNumber <= 35)
            {
                return new PowerUps(_x, _y, "slowBoi");
            }
            else if (randomNumber <= 55)
            {
                return new PowerUps(_x, _y, "smallBoi");
            }
            else if (randomNumber <= 80)
            {
                return new PowerUps(_x, _y, "enlargedBoi");
            }
            else   // if its lower than 105
            {
                return new PowerUps(_x, _y, "lifeBoi");
            }
            
        
        }
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
