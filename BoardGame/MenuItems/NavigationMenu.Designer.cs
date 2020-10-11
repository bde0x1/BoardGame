namespace BoardGame
{
    partial class NavigationMenu
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NavigationMenu));
            this.label1 = new System.Windows.Forms.Label();
            this.GoToTheGame = new System.Windows.Forms.Button();
            this.GoToSettings = new System.Windows.Forms.Button();
            this.GoToRules = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(204, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(183, 73);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hello";
            // 
            // GoToTheGame
            // 
            this.GoToTheGame.BackColor = System.Drawing.Color.Transparent;
            this.GoToTheGame.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GoToTheGame.BackgroundImage")));
            this.GoToTheGame.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.GoToTheGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.GoToTheGame.ForeColor = System.Drawing.Color.Transparent;
            this.GoToTheGame.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.GoToTheGame.Location = new System.Drawing.Point(67, 539);
            this.GoToTheGame.Name = "GoToTheGame";
            this.GoToTheGame.Size = new System.Drawing.Size(110, 110);
            this.GoToTheGame.TabIndex = 1;
            this.GoToTheGame.UseVisualStyleBackColor = false;
            this.GoToTheGame.Click += new System.EventHandler(this.GoToTheGame_Click);
            // 
            // GoToSettings
            // 
            this.GoToSettings.BackColor = System.Drawing.Color.Transparent;
            this.GoToSettings.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GoToSettings.BackgroundImage")));
            this.GoToSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.GoToSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.GoToSettings.ForeColor = System.Drawing.Color.White;
            this.GoToSettings.Location = new System.Drawing.Point(183, 539);
            this.GoToSettings.Name = "GoToSettings";
            this.GoToSettings.Size = new System.Drawing.Size(110, 110);
            this.GoToSettings.TabIndex = 2;
            this.GoToSettings.UseVisualStyleBackColor = false;
            this.GoToSettings.Click += new System.EventHandler(this.GoToSettings_Click);
            // 
            // GoToRules
            // 
            this.GoToRules.BackColor = System.Drawing.Color.Transparent;
            this.GoToRules.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("GoToRules.BackgroundImage")));
            this.GoToRules.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.GoToRules.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.GoToRules.ForeColor = System.Drawing.Color.White;
            this.GoToRules.Location = new System.Drawing.Point(299, 539);
            this.GoToRules.Name = "GoToRules";
            this.GoToRules.Size = new System.Drawing.Size(110, 110);
            this.GoToRules.TabIndex = 3;
            this.GoToRules.UseVisualStyleBackColor = false;
            this.GoToRules.Click += new System.EventHandler(this.GoToRules_Click);
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Transparent;
            this.Exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Exit.BackgroundImage")));
            this.Exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Exit.ForeColor = System.Drawing.Color.White;
            this.Exit.Location = new System.Drawing.Point(415, 539);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(110, 110);
            this.Exit.TabIndex = 20;
            this.Exit.UseVisualStyleBackColor = false;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(141, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(314, 33);
            this.label2.TabIndex = 21;
            this.label2.Text = "Üdvözlünk a játékban";
            // 
            // NavigationMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.GoToRules);
            this.Controls.Add(this.GoToSettings);
            this.Controls.Add(this.GoToTheGame);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NavigationMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NavigationMenu";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GoToTheGame;
        private System.Windows.Forms.Button GoToSettings;
        private System.Windows.Forms.Button GoToRules;
        private System.Windows.Forms.Button Exit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip ButtonTip;
    }
}