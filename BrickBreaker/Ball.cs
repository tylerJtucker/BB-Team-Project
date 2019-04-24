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
        }

        public bool BlockCollision(Block b)
        {
            //this algorothm is also known as the Minkowski sum
            //manage collision on all sides

            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                Point centerBall = new Point(x + size / 2, y + size / 2);
                Point centreRect = new Point(b.x + b.width / 2, b.y + b.height / 2);

                float w = (size + b.width) / 2;
                float h = (size + b.height) / 2;

                float dX = centreRect.X - centerBall.X;
                float dY = centreRect.Y - centerBall.Y;

                float wy = w * dY;
                float hx = h * dX;

                if (wy > hx)
                {
                    if (wy > -hx)
                    // collision at the top 
                    {
                        ySpeed *= -1;
                    }
                    else
                    // on the left 
                    {
                        xSpeed *= -1;
                    }
                }
                else
                {
                    if (wy > -hx)
                    // on the right
                    {
                        xSpeed *= -1;
                    }
                    else
                    // at the bottom
                    {
                        ySpeed *= -1;
                    }
                }
            }

            return blockRec.IntersectsWith(ballRec);
        }

        public void PaddleCollision(Paddle p, bool pMovingLeft, bool pMovingRight)
        {
            //make sure to develop the physics behind this stuff
            //so angles and such
            //this should change the angle at which the ball is travelling

            Rectangle ballRec = new Rectangle(x, y, size, size);
            Rectangle paddleRec = new Rectangle(p.x, p.y, p.width, p.height);

            if (ballRec.IntersectsWith(paddleRec))
            {
                Point centerBall = new Point(x + size / 2, y + size / 2);
                Point centrePad = new Point(p.x + p.width / 2, p.y + p.height / 2);

                float w = (size + p.width) / 2;
                float h = (size + p.height) / 2;

                float dX = centrePad.X - centerBall.X;
                float dY = centrePad.Y - centerBall.Y;

                float wy = w * dY;
                float hx = h * dX;

                if (wy > hx)
                {
                    if (wy > -hx)
                    // collision at the top 
                    {
                        ySpeed *= -1;

                        if (pMovingLeft)
                            xSpeed = -Math.Abs(xSpeed);
                        else if (pMovingRight)
                            xSpeed = Math.Abs(xSpeed);
                    }
                    else
                    // on the left 
                    {
                        xSpeed *= -1;
                        //here, kill the ball
                    }
                }
                else
                {
                    if (wy > -hx)
                    // on the right
                    {
                        xSpeed *= -1;
                        //kill the ball as well
                    }
                }
            }
        }

        //this one works fine
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

        //this one works fine
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
