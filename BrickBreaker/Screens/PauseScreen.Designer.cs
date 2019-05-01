namespace BrickBreaker
{
    partial class PauseScreen
    {
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
            this.exitButton = new System.Windows.Forms.Button();
            this.resumeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.White;
            this.exitButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.exitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exitButton.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.exitButton.Location = new System.Drawing.Point(229, 308);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(181, 78);

            this.exitButton.TabIndex = 2;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // resumeButton
            // 
            this.resumeButton.BackColor = System.Drawing.Color.White;
            this.resumeButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.resumeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resumeButton.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            this.resumeButton.Location = new System.Drawing.Point(235, 192);
            this.resumeButton.Name = "resumeButton";
            this.resumeButton.Size = new System.Drawing.Size(181, 78);

            this.resumeButton.TabIndex = 3;
            this.resumeButton.Text = "Resume";
            this.resumeButton.UseVisualStyleBackColor = false;
            this.resumeButton.Click += new System.EventHandler(this.resumeButton_Click);
            // 
            // PauseScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Desktop;
            this.BackgroundImage = BrickBreaker.Properties.Resources.PauseScreen;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.resumeButton);
            this.Controls.Add(this.exitButton);

      
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "PauseScreen";
            this.Size = new System.Drawing.Size(625, 439);

            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button resumeButton;
    }
}
