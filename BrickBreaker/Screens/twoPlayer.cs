using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickBreaker.Screens
{
    public partial class twoPlayer : UserControl
    {
        public twoPlayer()
        {
            InitializeComponent();

            /*
            static Paddle paddle;
            static Paddle paddle2;
            static Ball ball;
            public static int paddleHeight = 20;
        int paddleBoostY = 60;

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

paddle = new Paddle(paddleX, paddleY, paddleWidth, paddleHeight, paddleSpeed, Color.Purple);
paddles.Add(paddle);
            paddle2 = new Paddle(paddleX, paddleY2, paddleWidth, paddleHeight, paddleSpeed, Color.Blue);
paddles.Add(paddle2);

    //loads current level based on whether it's one or two player
            if (Twoplayer == false)
            {
                LoadLevel("Resources/level1.xml");
            }
            else
            {
                LoadLevel("Resources/twoplayerlevel1.xml");
            }

            //move P2 Paddle
            if (aKeyDown && paddle2.x > 0)
            {
                paddle2.Move("left");
            }
            if (dKeyDown && paddle2.x < (this.Width - paddle2.width))
            {
                paddle2.Move("right");
            }

            paddleBrush.Color = paddle.colour;
            e.Graphics.FillRectangle(shadowBrush, paddle.x + 3, paddle.y + 3, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush, paddle.x, paddle.y, paddle.width, paddle.height);
            e.Graphics.FillRectangle(blockBrush2, paddle.x + 1, paddle.y + 1, paddle.width - 2, paddle.height - 2);
            */
        }
    }
}
