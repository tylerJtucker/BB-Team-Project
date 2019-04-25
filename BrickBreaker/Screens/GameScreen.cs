/*  Created by: Brick Beaker Team 1
 *  Project: Brick Breaker
 *  Date: Tuesday, April 4th
 */ 
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values

        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, pauseArrowDown;

        // Game values
        int lives;
        int bricksBroken;
        int score;
        

        // constants
        const int BALLSPEED = 6;
        const int PADDLESPEED = 8;
        const int PADDLEWIDTH = 80;

        // Paddle and Ball objects
        static Paddle paddle;
        static Ball ball;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Black);
        SolidBrush blockBrush2 = new SolidBrush(Color.White);
        SolidBrush shadowBrush = new SolidBrush(Color.LightGray);

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        
        }


        public void OnStart()
        {
          
            //set life counter
            lives = 3;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            int paddleY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.Black);

            // setup starting ball values
            int ballX = this.Width / 2 - 10;
            int ballY = this.Height - paddle.height - 80;

            // Creates a new ball
            int xSpeed = 6;
            int ySpeed = 6;
            int ballSize = 20;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);

            #region Creates blocks for generic level. Need to replace with code that loads levels.

            blocks.Clear();
            int x = 10;

            while (blocks.Count < 12)
            {
                x += 57;
                Block b1 = new Block(x, 10, 1, Color.White);
                blocks.Add(b1);
            }

            #endregion

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 button presses
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = true;
                    break;
                case Keys.Right:
                    rightArrowDown = true;
                    break;
                case Keys.P:
                    pauseArrowDown = true;
                    break;
                default:
                    break;
            }
        }

        private void GameScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //player 1 button releases
            switch (e.KeyCode)
            {
                case Keys.Left:
                    leftArrowDown = false;
                    break;
                case Keys.Right:
                    rightArrowDown = false;
                    break;
                case Keys.P:
                    pauseArrowDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
           
           
            // Move the paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }
            if (pauseArrowDown)
            {
                
                PauseScreen ps = new PauseScreen();
                Form form = this.FindForm();
        
                gameTimer.Enabled = false;

                form.Controls.Add(ps);
                form.Controls.Remove(this);
                
                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);
            }

            // Move ball
            ball.Move();

            // Check for collision with top and side walls
            ball.WallCollision(this);

            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this))
            {
                lives--;

                // Moves the ball back to origin
                ball.x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                ball.y = (this.Height - paddle.height) - 85;

                if (lives == 0)
                {
                    gameTimer.Enabled = false;
                    OnEnd();
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            ball.PaddleCollision(paddle, leftArrowDown, rightArrowDown);

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    blocks.Remove(b);
                    bricksBroken++;

                    if (blocks.Count == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }

                    break;
                }
            }

            //redraw the screen
            Refresh();
        }

        public void OnEnd()
        {
            score = bricksBroken * 50;
            
            // Goes to the game over screen
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();
            
            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            
            // Draws paddle
            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(shadowBrush, paddle.x + 3, paddle.y + 3, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush, paddle.x, paddle.y, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush2, paddle.x + 1, paddle.y + 1, paddle.width - 2, paddle.height - 2);

            // Draws blocks

            foreach (Block b in blocks)
                {
                e.Graphics.FillRectangle(shadowBrush, b.x + 3, b.y + 3, b.width, b.height);
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
               e.Graphics.FillRectangle(blockBrush2, b.x + 1, b.y + 1, b.width - 2, b.height - 2);
            }
                
            

            // Draws ball
           e.Graphics.FillEllipse(shadowBrush, ball.x + 3, ball.y + 3, ball.size, ball.size);
            e.Graphics.FillEllipse(blockBrush, ball.x, ball.y, ball.size, ball.size);
            e.Graphics.FillEllipse(blockBrush2, ball.x + 1, ball.y + 1, ball.size - 2, ball.size - 2);
 

        }

        public static void ChangeSpeeds (int xSpeed, int ySpeed, int paddleSpeed)
        {
            if (ball.xSpeed < 0) { ball.xSpeed -= xSpeed; }
            else { ball.xSpeed += xSpeed; }

            if (ball.ySpeed < 0) { ball.ySpeed -= ySpeed; }
            else { ball.ySpeed += ySpeed; }

            paddle.speed += paddleSpeed;
        }

        public void ChangePaddle (int width)
        {
            paddle.width += width;
        }

        public void ChangeLives (int number)
        { 
            lives += number;
        }
    }
}
