namespace BoardGame
{
    partial class HupszCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HupszCard));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HupszAction = new System.Windows.Forms.Label();
            this.OK = new System.Windows.Forms.Button();
            this.PlayerOrRobotName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Bernard MT Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(237, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Hupsz...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bernard MT Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Action:";
            // 
            // HupszAction
            // 
            this.HupszAction.AutoSize = true;
            this.HupszAction.Font = new System.Drawing.Font("Bernard MT Condensed", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HupszAction.Location = new System.Drawing.Point(38, 146);
            this.HupszAction.Name = "HupszAction";
            this.HupszAction.Size = new System.Drawing.Size(41, 22);
            this.HupszAction.TabIndex = 2;
            this.HupszAction.Text = "Text";
            // 
            // OK
            // 
            this.OK.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("OK.BackgroundImage")));
            this.OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.OK.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.OK.Location = new System.Drawing.Point(450, 191);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(110, 110);
            this.OK.TabIndex = 3;
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // PlayerOrRobotName
            // 
            this.PlayerOrRobotName.AutoSize = true;
            this.PlayerOrRobotName.Font = new System.Drawing.Font("Bernard MT Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerOrRobotName.Location = new System.Drawing.Point(211, 63);
            this.PlayerOrRobotName.Name = "PlayerOrRobotName";
            this.PlayerOrRobotName.Size = new System.Drawing.Size(203, 28);
            this.PlayerOrRobotName.TabIndex = 6;
            this.PlayerOrRobotName.Text = "PlayerOrRobotName";
            // 
            // HupszCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(572, 313);
            this.Controls.Add(this.PlayerOrRobotName);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.HupszAction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.ForeColor = System.Drawing.Color.Gold;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "HupszCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "HupszCard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label HupszAction;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Label PlayerOrRobotName;
    }
}