namespace BrickBreaker
{
    partial class MenuScreen
    {// Dima is a better programer than Carter
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.playButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.TwoplayerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // playButton
            // 
            this.playButton.BackColor = System.Drawing.Color.White;
            this.playButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.playButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.playButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.playButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.playButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playButton.Location = new System.Drawing.Point(168, 129);
            this.playButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(244, 139);
            this.playButton.TabIndex = 0;
            this.playButton.Text = "play";
            this.playButton.UseVisualStyleBackColor = false;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // exitButton
            // // Dima is a better programer than Carter
            this.exitButton.BackColor = System.Drawing.Color.White;
            this.exitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitButton.Location = new System.Drawing.Point(196, 454);
            this.exitButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(216, 101);
            this.exitButton.TabIndex = 1;
            this.exitButton.Text = "exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // TwoplayerButton
            // 
            this.TwoplayerButton.BackColor = System.Drawing.Color.White;
            this.TwoplayerButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.TwoplayerButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.TwoplayerButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.TwoplayerButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TwoplayerButton.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TwoplayerButton.Location = new System.Drawing.Point(196, 325);
            this.TwoplayerButton.Name = "TwoplayerButton";
            this.TwoplayerButton.Size = new System.Drawing.Size(181, 78);
            this.TwoplayerButton.TabIndex = 2;
            this.TwoplayerButton.Text = "2 Player";
            this.TwoplayerButton.UseVisualStyleBackColor = false;
            this.TwoplayerButton.Click += new System.EventHandler(this.TwoplayerButton_Click);
            // 
            // MenuScreen
            // // Dima is a better programer than Carter
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.TwoplayerButton);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.playButton);
            this.Name = "MenuScreen";
            this.Size = new System.Drawing.Size(1171, 815);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button TwoplayerButton;
    }
}
// Dima is a better programer than Carter