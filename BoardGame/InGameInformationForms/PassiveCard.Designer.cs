namespace BoardGame
{
    partial class PassiveCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PassiveCard));
            this.label1 = new System.Windows.Forms.Label();
            this.Message = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.AbilityText = new System.Windows.Forms.Label();
            this.Ok = new System.Windows.Forms.Button();
            this.PlayerOrRobotName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bernard MT Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(133, 72);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(227, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Egy Passive kártyát húztál,";
            // 
            // Message
            // 
            this.Message.AutoSize = true;
            this.Message.Font = new System.Drawing.Font("Bernard MT Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Message.Location = new System.Drawing.Point(51, 189);
            this.Message.Name = "Message";
            this.Message.Size = new System.Drawing.Size(114, 22);
            this.Message.TabIndex = 1;
            this.Message.Text = "A képességed:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Bernard MT Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(133, 111);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(221, 25);
            this.label3.TabIndex = 2;
            this.label3.Text = "így egy új képességed lett!";
            // 
            // AbilityText
            // 
            this.AbilityText.AutoSize = true;
            this.AbilityText.Font = new System.Drawing.Font("Bernard MT Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AbilityText.Location = new System.Drawing.Point(134, 220);
            this.AbilityText.Name = "AbilityText";
            this.AbilityText.Size = new System.Drawing.Size(56, 22);
            this.AbilityText.TabIndex = 3;
            this.AbilityText.Text = "Ability";
            // 
            // Ok
            // 
            this.Ok.BackColor = System.Drawing.Color.Transparent;
            this.Ok.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Ok.BackgroundImage")));
            this.Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Ok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Ok.Location = new System.Drawing.Point(385, 207);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(110, 110);
            this.Ok.TabIndex = 4;
            this.Ok.UseVisualStyleBackColor = false;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // PlayerOrRobotName
            // 
            this.PlayerOrRobotName.AutoSize = true;
            this.PlayerOrRobotName.Font = new System.Drawing.Font("Bernard MT Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerOrRobotName.Location = new System.Drawing.Point(157, 25);
            this.PlayerOrRobotName.Name = "PlayerOrRobotName";
            this.PlayerOrRobotName.Size = new System.Drawing.Size(203, 28);
            this.PlayerOrRobotName.TabIndex = 5;
            this.PlayerOrRobotName.Text = "PlayerOrRobotName";
            // 
            // PassiveCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(507, 329);
            this.Controls.Add(this.PlayerOrRobotName);
            this.Controls.Add(this.Ok);
            this.Controls.Add(this.AbilityText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Message);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Ravie", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Gold;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PassiveCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PassiveCard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label AbilityText;
        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label PlayerOrRobotName;
    }
}