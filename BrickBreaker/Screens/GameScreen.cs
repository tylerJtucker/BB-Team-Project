
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
using System.Xml;

namespace BrickBreaker
{
    public partial class GameScreen : UserControl
    {
        #region global values
        //player1 button control keys - DO NOT CHANGE
        Boolean leftArrowDown, rightArrowDown, pauseArrowDown, aLetterDown, dLetterDown;

        // Game values
        static int lives;
        int bricksBroken;
        int score;
        int b = 1;
        public static Boolean Twoplayer = false;
        // constants
        const int BALLSPEED = 6;
        const int PADDLESPEED = 8;
        const int PADDLEWIDTH = 80;
        public static int paddleHeight = 20;
        // Paddle and Ball objects
        static Paddle paddle;
        static Paddle paddle2;
        static Ball ball;

        // list of all blocks and paddles for current level
        List<Block> blocks = new List<Block>();
        List<Paddle> paddles = new List<Paddle>();

        // Brushes
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Red);

        #endregion

        public GameScreen()
        {
            InitializeComponent();
            OnStart();
        }
        #region 
        public void LoadLevel(string level)
        {
            //creates variables and xml reader needed
            XmlReader reader = XmlReader.Create(level);
            string blockX;
            string blockY;
            string blockHP;
            int intX;
            int intY;
            int intHP;

            //Grabs all the blocks for the current level and adds them to the list
            while (reader.Read())
            {
                reader.ReadToFollowing("x");
                blockX = reader.ReadString();
                reader.ReadToFollowing("y");
                blockY = reader.ReadString();
                reader.ReadToFollowing("hp");
                blockHP = reader.ReadString();

                if (blockX != "")
                {
                    intX = Convert.ToInt16(blockX);
                    intY = Convert.ToInt16(blockY);
                    intHP = Convert.ToInt16(blockHP);

                    Block b = new Block(intX, intY, intHP);

                    blocks.Add(b);
                }
            }
        }
        #endregion

        public void OnStart()
        {
            //set life counter
            lives = 1000;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleBoostY = 60;
            int paddleX = ((this.Width / 2) - (paddleWidth / 2));
            //set Diffrent Starting Height for P1 Paddle
            if (Twoplayer == true)
            {
                paddleBoostY = 80;
            }
            else
            {
                paddleBoostY = 60;
            }
            int paddleY = (this.Height - paddleHeight) - paddleBoostY;
            int paddleY2 = (this.Height - this.Height + paddleHeight + 60);
            int paddleSpeed = 8;
            //creates Paddle and Adds to List
            paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.Purple);
            paddles.Add(paddle);
            paddle2 = new Paddle(paddleX, paddleY2, paddleWidth, paddleHeight, paddleSpeed, Color.Blue);
            paddles.Add(paddle2);
            // setup starting ball values
            int ballX = this.Width / 2 - 10;
            int ballY = paddle.y - (paddleWidth / 2);

            // Creates a new ball
            int xSpeed = 6;
            int ySpeed = 6;
            int ballSize = 20;
            ball = new Ball(ballX, ballY, xSpeed, ySpeed, ballSize);

            //loads current level based on whether it's one or two player
            if (Twoplayer == false)
            {
                LoadLevel("Resources/level1.xml");
            }
            else
            {
                LoadLevel("Resources/twoplayerlevel1.xml");

            }
            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 and 2 button presses
            switch (e.KeyCode)
            {
                case Keys.A:
                    aLetterDown = true;
                    break;
                case Keys.D:
                    dLetterDown = true;
                    break;
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
            //player 1 and 2 button releases
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
                case Keys.A:
                    aLetterDown = false;
                    break;
                case Keys.D:
                    dLetterDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            // move P1 Paddle
            if (leftArrowDown && paddle.x > 0)
            {
                paddle.Move("left");
            }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width))
            {
                paddle.Move("right");
            }
            //move P2 Paddle
            if (aLetterDown && paddle2.x > 0)
            {
                paddle2.Move("left");
            }
            if (dLetterDown && paddle2.x < (this.Width - paddle2.width))
            {
                paddle2.Move("right");
            }
            //pause Screen
            if (pauseArrowDown)
            {

                PauseScreen ps = new PauseScreen();
                Form form = this.FindForm();

                gameTimer.Enabled = false;

                form.Controls.Add(ps);
                form.Controls.Remove(this);

                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);
            }

            // move ball
            ball.Move();

            // check for collision with top and side walls
            // Check for ball hitting bottom of screen
            if (ball.BottomCollision(this) && Twoplayer == false)
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
            //Ignores Bottom Wall Collsion from Single Player
            else
            {
                ball.WallCollision(this);
            }

            // Check for collision of ball with paddles, (incl. paddle movement)
            foreach (Paddle p in paddles)
            {
                ball.PaddleCollision(p, leftArrowDown, rightArrowDown);
            }

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {// trying to get it where if it's less than 1hp, go oppsite direction
                if (ball.BlockCollision(b) && b.hp <= 1)
                {
                    blocks.Remove(b);
                    bricksBroken++;

                    if (blocks.Count == 0 || lives == 0)
                    {
                        if (lives == 0)
                        {
                            gameTimer.Enabled = false;
                            OnEnd();
                        }
                        LoadLevels();
                    }

                    break;
                }
            }

            //redraw the screen
            Refresh();
        }



        public void LoadLevels()
        {       // Loads diffrent levels when there are no more blocks and player is alive
            if (lives > 0 && blocks.Count == 0 && Twoplayer == false)
            {
                b++;
                switch (b)
                {
                    case 2:
                        LoadLevel("Resources/level2.xml");
                        break;
                    case 3:
                        LoadLevel("Resources/level3.xml");
                        break;
                    case 4:
                        LoadLevel("Resources/level4.xml");
                        break;
                    case 5:
                        LoadLevel("Resources/level5.xml");
                        break;
                    case 6:
                        LoadLevel("Resources/level6.xml");
                        break;
                    case 7:
                        LoadLevel("Resources/level7.xml");
                        break;
                }
            }
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
            // Draws one paddle in Single Player
            if (Twoplayer == false)
            {
                paddleBrush.Color = paddle.colour;
                e.Graphics.FillRectangle(paddleBrush, paddle.x, paddle.y, paddle.width, paddle.height);
            }
            //Draws two paddle in two player
            if (Twoplayer == true)
            {
                foreach (Paddle p in paddles)
                {
                    paddleBrush.Color = p.colour;
                    e.Graphics.FillRectangle(paddleBrush, p.x, p.y, p.width, p.height);
                }

            }

            // Draws blocks
            foreach (Block b in blocks)
            {
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
            }

            // Draws ball
            e.Graphics.FillRectangle(ballBrush, ball.x, ball.y, ball.size, ball.size);
        }




        #region change value functions
        public static void ChangeSpeeds(int xSpeed, int ySpeed, int paddleSpeed)
        {
            if (ball.xSpeed < 0) { ball.xSpeed -= xSpeed; }
            else { ball.xSpeed += xSpeed; }

            if (ball.ySpeed < 0) { ball.ySpeed -= ySpeed; }
            else { ball.ySpeed += ySpeed; }

            paddle.speed += paddleSpeed;
        }

        public static void ChangePaddle(int width)
        {
            paddle.width += width;
        }

        public static void ChangeLives(int number)
        {
            lives += number;
        }

        public void ReturnSpeeds()
        {
            if (ball.xSpeed < 0) { ball.xSpeed = -BALLSPEED; }
            else { ball.xSpeed = BALLSPEED; }

            if (ball.ySpeed < 0) { ball.ySpeed = -BALLSPEED; }
            else { ball.ySpeed = BALLSPEED; }

            paddle.speed = PADDLESPEED;
        }

        public static void ReturnPaddle()
        {
            paddle.width = PADDLESPEED;
        }
        #endregion
    }


}
