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
        }

        public void Move()
        {
            
            x = x + xSpeed;
            y = y + ySpeed;
           
            /*
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
                //foreach (Block b in blocList)
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
            //*/
        }

        public bool BlockCollision(Block b)
        {
            //manage collision on all sides

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
            //this should change the angle at which the ball is travelling

            //manage collision on the sides

            Rectangle ballRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(p.x, p.y, p.width, p.height);

            if (ballRec.IntersectsWith(paddleRec))
            {
                //get the position using the centre of the objects
                PointF ballCentre = new PointF(x + size / 2, y + size / 2);
                PointF paddleCentre = new PointF(p.x + p.width / 2, p.y + p.height / 2);

                //I neeed to do some testing with the deltas and heights so I can tell by how much the are intersecting

                float vertIntersec = (size + p.height) / 2 - Math.Abs(ballCentre.Y - paddleCentre.Y);


                /*
                if (y + size >= p.y)
                {
                    ySpeed *= -1;
                }


                if (pMovingLeft)
                    xSpeed = -Math.Abs(xSpeed);
                else if (pMovingRight)
                    xSpeed = Math.Abs(xSpeed);
                //*/
            }
        }

        public void WallCollision(UserControl UC)
        {
            // Collision with left wall
            if (x <= 0)
            {
                xSpeed = Math.Abs(xSpeed);
            }
            // Collision with right wall
            if (x >= (UC.Width - size))
            {
                xSpeed = Math.Abs(xSpeed) * -1;
            }
            // Collision with top wall
            if (y <= 2)
            {
                ySpeed = Math.Abs(ySpeed);
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
