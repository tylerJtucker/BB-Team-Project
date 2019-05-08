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
    public partial class highscoreScreen : UserControl
    {
        public highscoreScreen()
        {
            InitializeComponent();
            outputLabel.Text = Convert.ToString(GameScreen.highscores);
        }
    }
}
