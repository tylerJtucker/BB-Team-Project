using System;
using System.Drawing;
using System.Windows.Forms;

namespace BrickBreaker
{
    public class Ball
    {
        public int x, y, xSpeed, ySpeed, size;
        public Color colour;

        public static Random rand = new Random();

        public Ball(int _x, int _y, int _xSpeed, int _ySpeed, int _ballSize)
        {
            //is direction an input from outside?
            x = _x;
            y = _y;
            xSpeed = _xSpeed;
            ySpeed = _ySpeed;
            size = _ballSize;

            //this is a small change
               
        }

        public void Move()
        {
            /*
            x = x + xSpeed;
            y = y + ySpeed;
            same function but different code to set up next step
            my idea is to move pixel by pixel and check every time for collisions
            in the proximity of the ball to prevent glitches
            */

            int xFrames = Math.Abs(xSpeed);
            int yFrames = Math.Abs(ySpeed);

            for (int i = 0; i < xFrames; i++)
            {
                if (xSpeed >= 0)
                {
                    x++;
                }
                else
                {
                    x--;
                }
                //collide with obj in proximity
            }

            for (int i = 0; i < yFrames; i++)
            {
                if (ySpeed >= 0)
                {
                    y++;
                }
                else
                {
                    y--;
                }
                //collide with obj in proximity
            }
        }

        public bool BlockCollision(Block b)
        {
            //we might not need this method

            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                ySpeed *= -1;
            }

            return blockRec.IntersectsWith(ballRec);         
        }

        public void PaddleCollision(Paddle p, bool pMovingLeft, bool pMovingRight)
        {
            //make sure to develop the physics behind this stuff
            //so angles and such

            Rectangle ballRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(p.x, p.y, p.width, p.height);

            if (ballRec.IntersectsWith(paddleRec))
            {
                if (y + size >= p.y)
                {
                    ySpeed *= -1;
                }

                if (pMovingLeft)
                    xSpeed = -Math.Abs(xSpeed);
                else if (pMovingRight)
                    xSpeed = Math.Abs(xSpeed);
            }
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left wall
            if (x <= 0)
            {
                xSpeed *= -1;
            }
            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                xSpeed *= -1;
            }
            // Collision with top wall
            if (y <= 2)
            {
                ySpeed *= -1;
            }
        }

        public bool BottomCollision(UserControl UC)
        {
            Boolean didCollide = false;

            if (y >= UC.Height)
            {
                didCollide = true;
            }

            return didCollide;
        }

    }
}
