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
            Rectangle blockRec = new Rectangle(b.x, b.y, b.width, b.height);
            Rectangle ballRec = new Rectangle(x, y, size, size);

            if (ballRec.IntersectsWith(blockRec))
            {
                string side = CollisionSide(blockRec);
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
                ballRec.X -= xSpeed;
                ballRec.Y -= ySpeed;

                string side = CollisionSide(paddleRec);

                if (side == "top")
                {
                    int resultSpeed = 0;

                    if (pMovingLeft)
                    {
                        if (xSpeed > 0)
                        {
                            resultSpeed = -p.speed + xSpeed;
                        }
                        else if (xSpeed == 0)
                        {
                            resultSpeed = - p.speed / 4;
                        }
                        else
                        {
                            resultSpeed = xSpeed;
                        }

                    }
                    else if (pMovingRight)
                    {
                        if (xSpeed > 0)
                        {
                            resultSpeed = xSpeed;
                        }
                        else if (xSpeed == 0)
                        {
                            resultSpeed = p.speed / 4;
                        }
                        else
                        {
                            resultSpeed = p.speed + xSpeed;
                        }
                    }

                    else
                    {
                        resultSpeed = xSpeed;
                    }

                    xSpeed = resultSpeed;

                }
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
            //Checks for bottom wall collsion if two player
            if(GameScreen.Twoplayer == true)
            {
                if (y >= UC.Height - size)
                {
                    ySpeed *= -1;
                }
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

        /// <summary>
        /// Given the colliding rectangle finds which sides are colliding
        /// </summary>
        /// <param name="r">Colliding rectangle</param>
        /// <returns>Side as a string</returns>
        public string CollisionSide(Rectangle r)
        {            
            //this algorothm is also known as the Minkowski sum
            //manage collision on all sides

            string side = null;

            Point centreBall = new Point(x + size / 2, y + size / 2);
            Point centreRect = new Point(r.X + r.Width / 2, r.Y + r.Height / 2);

            float w = (size + r.Width) / 2;
            float h = (size + r.Height) / 2;

            float dX = centreRect.X - centreBall.X;
            float dY = centreRect.Y - centreBall.Y;

            float wy = w * dY;
            float hx = h * dX;

            if (wy > hx)
            {
                if (wy > -hx)
                // collision at the top 
                {
                    ySpeed *= -1;
                    side = "top";
                }
                else
                // on the right
                {
                    xSpeed *= -1;
                    side = "right";
                }
            }
            else
            {
                if (wy > -hx)
                // on the left
                {
                    xSpeed *= -1;
                    side = "left";
                }
                else
                // at the bottom
                {
                    ySpeed *= -1;
                    side = "bottom";
                }
            }

            return side;
        }
    }
}
