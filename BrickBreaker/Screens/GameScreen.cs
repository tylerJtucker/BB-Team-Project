
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

        int level = 1;
        int ballStartX, ballStartY, paddleStartX, paddleStartY, ballStartSpeedX = 0, ballStartSpeedY = -10;
        static int bbucks = 0;

        Random rng = new Random();

        // constants
        const int BALLSPEED = 6;
        const int PADDLESPEED = 8;
        const int PADDLEWIDTH = 80;

        // Paddle and Ball objects
        static Paddle paddle;
        static Ball ball;

        // list of all blocks for current level
        List<Block> blocks = new List<Block>();
        static List<Ball> balls = new List<Ball>();
        List<PowerUps> powerups = new List<PowerUps>();

        // Brushes

        SolidBrush drawBrush = new SolidBrush(Color.White);
        Font drawFont = new Font("Arial", 12);

        SolidBrush paddleBrush = new SolidBrush(Color.White);
        SolidBrush ballBrush = new SolidBrush(Color.White);
        SolidBrush blockBrush = new SolidBrush(Color.Black);
        SolidBrush blockBrush2 = new SolidBrush(Color.White);
        SolidBrush shadowBrush = new SolidBrush(Color.LightGray);
        SolidBrush powerBrush = new SolidBrush(Color.White);
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
            leftArrowDown = rightArrowDown = false;

            // setup starting paddle values and create paddle object
            int paddleWidth = 80;
            int paddleHeight = 20;
            paddleStartX = ((this.Width / 2) - (paddleWidth / 2));
            paddleStartY = (this.Height - paddleHeight) - 60;
            int paddleSpeed = 8;

            paddle = new Paddle(paddleStartX, paddleStartY, paddleWidth, paddleHeight, paddleSpeed, Color.White);


            // setup starting ball values
            ballStartX = this.Width / 2 - 10;
            ballStartY = this.Height - paddle.height - 85;

            // Creates a new ball
            int ballSize = 20;
            ball = new Ball(ballStartX, ballStartY, 0, 0, ballSize);
            balls.Clear();
            balls.Add(ball);

            //loads current level
            LoadLevel("Resources/level2.xml");

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
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.A:
                    aKeyDown = false;
                    break;
                case Keys.D:
                    dKeyDown = false;
                    break;
                default:
                    break;
            }
        }


        private void gameTimer_Tick(object sender, EventArgs e)
        {
            // Move the paddle
            if (upArrowDown && onPaddle)
            {
                balls[0].xSpeed = ballStartSpeedX;
                balls[0].ySpeed = ballStartSpeedY;
                onPaddle = false;
            }

            if (onPaddle)
            {
                balls[0].x = paddle.x + PADDLEWIDTH / 2;
            }

            if (leftArrowDown && paddle.x > 0) { paddle.Move("left"); }

            if (rightArrowDown && paddle.x < (this.Width - paddle.width)) { paddle.Move("right"); }

            if (pauseArrowDown)
            {
                PauseScreen ps = new PauseScreen();
                Form form = this.FindForm();

                gameTimer.Enabled = false;

                form.Controls.Add(ps);
                form.Controls.Remove(this);

                ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);
            }

            if (aKeyDown && onPaddle)
            {
                if (ballStartSpeedX > -8 && ballStartSpeedX <= 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX > -8 && ballStartSpeedX > 0)
                {
                    ballStartSpeedX--;
                    ballStartSpeedY--;
                }
            }
            if (dKeyDown && onPaddle)
            {
                if (ballStartSpeedX < 8 && ballStartSpeedX >= 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY++;
                }
                else if (ballStartSpeedX < 8 && ballStartSpeedX < 0)
                {
                    ballStartSpeedX++;
                    ballStartSpeedY--;
                }
            }
           

            // Move ball
            foreach (Ball b in balls) { b.Move(); }

            // Check for collision with all walls
            foreach (Ball b in balls)
            {
                //Check for ball hitting top and side walls
                b.WallCollision(this);

                // Check for ball hitting bottom of screen
                if (b.BottomCollision(this) && balls.Count == 1)
                {
                    lives--;

                    // Moves the ball back to origin
                    onPaddle = true;
                    OnDeath();

                    if (lives == 0)
                    {
                        gameTimer.Enabled = false;
                        OnEnd();
                    }
                }
                else if (b.BottomCollision(this))
                {
                    balls.Remove(b);
                    break;
                }
            }

            // Check for collision of ball with paddle, (incl. paddle movement)
            foreach (Ball b in balls) { b.PaddleCollision(paddle, leftArrowDown, rightArrowDown); }

            // Check if ball has collided with any blocks
            foreach (Block b in blocks)
            {
                if (ball.BlockCollision(b))
                {
                    //removing block logic
                    b.hp--;
                    if (b.hp == 0)
                    {
                        blocks.Remove(b);
                        score += 50;
                        if (rng.Next(1, 9) == 7)
                        powerups.Add(randomGenBoi(b.x, b.y));
                        break;
                    }

                    //if all blocks are broken go to next level
                    if (blocks.Count == 0)
                    {
                        NextLevel();
                    }
                }
            }

            foreach (PowerUps p in powerups)
            {
                p.Move();
                if (p.y > this.Height)
                {

                }
            }

            foreach (PowerUps p in powerups)
            {
                if (p.Collision(paddle))
                {
                    //do some weird shit
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

            foreach (PowerUps p in powerups)
            {
                e.Graphics.FillEllipse(powerBrush, p.x, p.y, 25, 25);
            }

            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(shadowBrush, paddle.x + 3, paddle.y + 3, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush, paddle.x, paddle.y, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush2, paddle.x + 1, paddle.y + 1, paddle.width - 2, paddle.height - 2);
            
            // Draws blocks


            // Draws ball(s)
            drawBrush.Color = Color.White;
            foreach (Ball b in balls) { e.Graphics.FillRectangle(drawBrush, b.x, b.y, b.size, b.size); }

            //draw score and lives
            e.Graphics.DrawString("Lives: " + ballStartSpeedX, drawFont, drawBrush, 100, 85);
            e.Graphics.DrawString("Score: " + ballStartSpeedY, drawFont, drawBrush, 100, 100);

        }

        public void OnEnd()
        {
            // Goes to the game over screen
            Form form = this.FindForm();
            MenuScreen ps = new MenuScreen();

            ps.Location = new Point((form.Width - ps.Width) / 2, (form.Height - ps.Height) / 2);

            form.Controls.Add(ps);
            form.Controls.Remove(this);
        }


        public void NextLevel()
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

            paddle.x = paddleStartX; paddle.y = paddleStartY;
            onPaddle = true;
            balls.Clear();
            ball = new Ball(ballStartX, ballStartY, 6, 6, 20);
            balls.Add(ball);
            
                /*
                // Draws ball
                e.Graphics.FillEllipse(shadowBrush, ball.x + 3, ball.y + 3, ball.size, ball.size);
                e.Graphics.FillEllipse(blockBrush, ball.x, ball.y, ball.size, ball.size);
                e.Graphics.FillEllipse(blockBrush2, ball.x + 1, ball.y + 1, ball.size - 2, ball.size - 2);
                */
        }
        
        public void OnDeath ()
        {
            ball.x = paddle.x + PADDLEWIDTH/2 ;
            ball.y = ballStartY;
            balls[0].xSpeed = 0;
            balls[0].ySpeed = 0;
        }

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

        public PowerUps randomGenBoi (int _x, int _y)
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

        public static void GiveBBuck (int bigmonies)
        {
            bbucks += bigmonies;
        }
        #endregion
    }
}
