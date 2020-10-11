namespace BoardGame
{
    partial class StealPassiveAbilityFromRobotsView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StealPassiveAbilityFromRobotsView));
            this.Cancel = new System.Windows.Forms.Button();
            this.Steal = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.RobotsPassiveAbilities = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PlayerName = new System.Windows.Forms.Label();
            this.ButtonTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Cancel
            // 
            this.Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel.BackgroundImage")));
            this.Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel.Location = new System.Drawing.Point(480, 305);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(110, 110);
            this.Cancel.TabIndex = 19;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // Steal
            // 
            this.Steal.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Steal.BackgroundImage")));
            this.Steal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Steal.Location = new System.Drawing.Point(341, 305);
            this.Steal.Name = "Steal";
            this.Steal.Size = new System.Drawing.Size(110, 110);
            this.Steal.TabIndex = 18;
            this.Steal.UseVisualStyleBackColor = true;
            this.Steal.Click += new System.EventHandler(this.Steal_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Bernard MT Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(42, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 22);
            this.label5.TabIndex = 17;
            this.label5.Text = "Kiválasztható lehetőségek:";
            // 
            // RobotsPassiveAbilities
            // 
            this.RobotsPassiveAbilities.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(36)))));
            this.RobotsPassiveAbilities.Font = new System.Drawing.Font("Bernard MT Condensed", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RobotsPassiveAbilities.ForeColor = System.Drawing.Color.Gold;
            this.RobotsPassiveAbilities.FormattingEnabled = true;
            this.RobotsPassiveAbilities.Location = new System.Drawing.Point(341, 237);
            this.RobotsPassiveAbilities.Name = "RobotsPassiveAbilities";
            this.RobotsPassiveAbilities.Size = new System.Drawing.Size(249, 21);
            this.RobotsPassiveAbilities.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bernard MT Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(123, 122);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(377, 25);
            this.label1.TabIndex = 12;
            this.label1.Text = "De vigyázz csak 1 et lophatsz, jól fontold meg!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bernard MT Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(41, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(549, 25);
            this.label2.TabIndex = 11;
            this.label2.Text = "Most tudsz lopni mástól egy képességet. Válaszd ki hogy mi legyen! ";
            // 
            // PlayerName
            // 
            this.PlayerName.AutoSize = true;
            this.PlayerName.Font = new System.Drawing.Font("Bernard MT Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerName.Location = new System.Drawing.Point(248, 29);
            this.PlayerName.Name = "PlayerName";
            this.PlayerName.Size = new System.Drawing.Size(124, 28);
            this.PlayerName.TabIndex = 10;
            this.PlayerName.Text = "playerName";
            // 
            // StealPassiveAbilityFromRobotsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(616, 427);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.Steal);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.RobotsPassiveAbilities);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PlayerName);
            this.ForeColor = System.Drawing.Color.Gold;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "StealPassiveAbilityFromRobotsView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StealPassiveAbilityFromRobotsView";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Button Steal;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox RobotsPassiveAbilities;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label PlayerName;
        private System.Windows.Forms.ToolTip ButtonTip;
    }
}