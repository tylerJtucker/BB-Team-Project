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
                string side = collisionSide(blockRec);
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
                string side = collisionSide(paddleRec);
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

        /// <summary>
        /// Given the colliding rectangle finds which sides are colliding
        /// </summary>
        /// <param name="r">Colliding rectangle</param>
        /// <returns>Side as a string</returns>
        public string collisionSide(Rectangle r)
        {
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
                }
                else
                // on the right
                {
                    xSpeed *= -1;
                }
            }
            else
            {
                if (wy > -hx)
                // on the left
                {
                    xSpeed *= -1;
                }
                else
                // at the bottom
                {
                    ySpeed *= -1;
                }
            }

            return side;
        }

    }
}
