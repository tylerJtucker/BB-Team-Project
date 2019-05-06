
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
        Boolean leftArrowDown, rightArrowDown, pauseArrowDown, upArrowDown, onPaddle = true, aKeyDown, dKeyDown;

        // Game values
        static int lives;
        int score;
        public static Boolean Twoplayer = false;
        int level = 1;
        int ballStartX, ballStartY, paddleStartX, paddleStartY, ballStartSpeedX = 0, ballStartSpeedY = -10;
        static int bbucks = 0;

        Random rng = new Random();

        // constants
        const int BALLSPEED = 6;
        const int PADDLESPEED = 8;
        const int PADDLEWIDTH = 80; const int PADDLEHEIGHT = 20;
        // Paddle and Ball objects
        public static Paddle paddle; public static Ball ball;

        // list of all blocks and paddles for current level
        List<Block> blocks = new List<Block>();
        public static List<int> highscores = new List<int>();
        public static List<Paddle> paddles = new List<Paddle>();
        public static List<Ball> balls = new List<Ball>();
        List<PowerUps> powerups = new List<PowerUps>();

        // Brushes
        SolidBrush drawBrush = new SolidBrush(Color.Tan);
        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Black);
        SolidBrush blockBrush2 = new SolidBrush(Color.White);
        SolidBrush shadowBrush = new SolidBrush(Color.LightGray);
        SolidBrush powerBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Arial", 12);

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
            level = 1;

            //set all button presses to false.
            leftArrowDown = rightArrowDown = aKeyDown = dKeyDown = false;

            // setup starting paddle values and create paddle object
            onPaddle = true;
            int paddleSpeed = 8;
            paddleStartX = ((this.Width / 2) - (PADDLEWIDTH / 2));
            paddleStartY = (this.Height - PADDLEHEIGHT) - 60;
            paddle = new Paddle(paddleStartX, paddleStartY, PADDLEWIDTH, PADDLEHEIGHT, paddleSpeed, Color.White);

            ballStartX = this.Width / 2 - 10;
            ballStartY = this.Height - paddle.height - 85;
            int ballSize = 20;
            balls.Clear();
            ball = new Ball (ballStartX, ballStartY, 0, 0, ballSize);
            balls.Add(ball);

            //load score
            //loadScore();
            LoadLevel("Resources/level1.xml");

            // start the game engine loop
            gameTimer.Enabled = true;
        }

        private void GameScreen_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //player 1 and 2 button presses
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

                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.A:
                    aKeyDown = true;
                    break;
                case Keys.D:
                    dKeyDown = true;
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
                    aKeyDown = false;
                    break;
                case Keys.D:
                    dKeyDown = false;

                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                default:
                    break;
            }
        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            //shoot ball off paddle
            if (upArrowDown && onPaddle)
            {
                ball.xSpeed = ballStartSpeedX;
                ball.ySpeed = ballStartSpeedY;
                onPaddle = false;
            }

            //center ball on paddle if it is supposed to be
            if (onPaddle) { balls[0].x = paddle.x + PADDLEWIDTH / 2; }

            //move paddle left and right
            if (leftArrowDown && paddle.x > 0) { paddle.Move("left"); }
            if (rightArrowDown && paddle.x < (this.Width - paddle.width)) { paddle.Move("right"); }

            //aim ball left and right from paddle
            if (aKeyDown && onPaddle)
            {
                if (ballStartSpeedX > -8 && ballStartSpeedX <= 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX < 8 && ballStartSpeedX > 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY--;
                }
            }
            if (dKeyDown && onPaddle)
            {
                if (ballStartSpeedX < 8 && ballStartSpeedX >= 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY--;
                }
                else if (ballStartSpeedX > -8 && ballStartSpeedX < 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY++;
                }
            }

            //pause game
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
            foreach (Ball b in balls) { ball.Move(); }

            //check for ball hitting side of screen
            foreach (Ball b in balls) { b.WallCollision(this); }

            // Check for ball hitting bottom of screen
            foreach (Ball b in balls)
            {
                if (b.BottomCollision(this))
                {
                    if (balls.Count == 1)
                    {
                        lives--;
                        if (lives == 0)
                        {
                            gameTimer.Enabled = false;
                            OnEnd();
                        }

                        // Moves the ball back to origin
                        balls[0].x = ((paddle.x - (ball.size / 2)) + (paddle.width / 2));
                        balls[0].y = (this.Height - paddle.height) - 85;
                    }
                    else if (b.BottomCollision(this))
                    {
                        balls.Remove(b);
                        break;
                    }
                }
            }

            // check to see if game is lost
            if (lives == 0)
            {
                gameTimer.Enabled = false;
                OnEnd();
            }

            //check for ball and paddle collision
            foreach (Ball b in balls) { b.PaddleCollision(paddle, leftArrowDown, rightArrowDown); }

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                foreach (Ball a in balls)
                {
                    if (a.BlockCollision(b))
                    {
                        b.hp--;
                        if (b.hp == 0)
                        {
                            blocks.Remove(b);
                            score += 50;
                            if (rng.Next(1, 9) == 7)
                            powerups.Add(randomGenBoi(b.x, b.y));
                            break;
                        }
                    }
                }

                //if all blocks are broken go to next level
                if (blocks.Count == 0)
                {
                    //TODO NEXT LEVEL
                    break;
                }
            }

            //move powerups
            foreach (PowerUps p in powerups)
            {
                p.Move();
                if (p.y > this.Height)
                {
                    powerups.Remove(p);
                    break;
                }
            }
            //check to see if power ups have hit the paddle
            foreach (PowerUps p in powerups)
            {
                if (p.Collision(paddle))
                {
                    //do some weird shit
                    powerups.Remove(p);
                    break;
                }
            }

            //redraw the screen
            Refresh();
        }

        public void GameScreen_Paint(object sender, PaintEventArgs e)
        {
            // Draws paddle
            drawBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(drawBrush, paddle.x, paddle.y, paddle.width, paddle.height);

            // Draws blocks
            foreach (Block b in blocks)
            {
                switch (b.hp)
                {
                    case 1:
                        drawBrush.Color = Color.Red;
                        break;
                    case 2:
                        drawBrush.Color = Color.Yellow;
                        break;
                    case 3:
                        drawBrush.Color = Color.Green;
                        break;
                }
                e.Graphics.FillRectangle(shadowBrush, b.x + 3, b.y + 3, b.width, b.height);
                e.Graphics.FillRectangle(blockBrush, b.x, b.y, b.width, b.height);
                e.Graphics.FillRectangle(drawBrush, b.x + 1, b.y + 1, b.width - 2, b.height - 2);
            }

            foreach (PowerUps p in powerups) { e.Graphics.FillEllipse(powerBrush, p.x, p.y, 25, 25); }

            // Draws ball(s)
            drawBrush.Color = Color.White;
            foreach (Ball b in balls) { e.Graphics.FillRectangle(drawBrush, b.x, b.y, b.size, b.size); }

            //draw score and lives
            e.Graphics.DrawString("Lives: " + ballStartSpeedX, drawFont, drawBrush, 100, 85);
            e.Graphics.DrawString("Score: " + ballStartSpeedY, drawFont, drawBrush, 100, 100);
        }

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

        #region Death and moving on
        public void NextLevel ()
        {
           level++;

            switch (level)
            {
            case 2:
                LoadLevel("Resources/level2.xml");
                break;
            case 3:
                LoadLevel("Resources / level3.xml");
                break;
            case 4:
                LoadLevel("Resources / level4.xml");
                break;
            case 5:
                LoadLevel("Resources / level5.xml");
                break;
            case 6:
                LoadLevel("Resources / level6.xml");
                break;
            case 7:
                LoadLevel("Resources / level7.xml");
                break;
            default:
                    OnEnd();
                    break;
            } 
        }
        
        public void OnEnd()
        {
            saveScore();

            // Goes to the game over screen
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();

            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }
       
        public void OnDeath()
        {
            ball.x = paddle.x + PADDLEWIDTH / 2 - ball.size /2;
            ball.y = ballStartY;
            balls[0].xSpeed = 0;
            balls[0].ySpeed = 0;
        }
        #endregion

        #region Levels and Scores
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

        public void saveScore()
        {
            highscores.Add(score);

            highscores.Sort();

            XmlWriter writer = XmlWriter.Create("Resources/scores.xml", null);

            writer.WriteStartElement("scores");

            for (int i = 0; i < 10; i++)
            {
                writer.WriteElementString("score", highscores[i].ToString());
            }
            writer.WriteEndElement();

            writer.Close();

        }

        public void loadScore()
        {
            string newScore;
            int intScore;

            XmlReader reader = XmlReader.Create("Resources/scores.xml");


            for (int i = 0; i < 10; i++)
            {
                reader.ReadToFollowing("score");
                newScore = reader.ReadString();

                if (newScore != "")
                {
                    intScore = Convert.ToInt16(newScore);
                    highscores.Add(intScore);
                }
                else
                {
                    break;
                }
            }
            reader.Close();
        }
        #endregion

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
            paddle.width = PADDLEWIDTH;
        }
        #endregion
    }
}
