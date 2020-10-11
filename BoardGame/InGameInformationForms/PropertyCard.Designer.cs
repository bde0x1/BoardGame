namespace BoardGame
{
    partial class PropertyCard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyCard));
            this.Ok = new System.Windows.Forms.Button();
            this.PlayerOrRobotName = new System.Windows.Forms.Label();
            this.CardText = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Price = new System.Windows.Forms.Label();
            this.TurnText = new System.Windows.Forms.Label();
            this.FinishedValue = new System.Windows.Forms.Label();
            this.Cancel = new System.Windows.Forms.Button();
            this.ButtonTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // Ok
            // 
            this.Ok.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Ok.BackgroundImage")));
            this.Ok.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Ok.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Ok.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Ok.Location = new System.Drawing.Point(12, 209);
            this.Ok.Name = "Ok";
            this.Ok.Size = new System.Drawing.Size(110, 110);
            this.Ok.TabIndex = 5;
            this.Ok.UseVisualStyleBackColor = true;
            this.Ok.Click += new System.EventHandler(this.Ok_Click);
            // 
            // PlayerOrRobotName
            // 
            this.PlayerOrRobotName.AutoSize = true;
            this.PlayerOrRobotName.Font = new System.Drawing.Font("Bernard MT Condensed", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerOrRobotName.Location = new System.Drawing.Point(163, 18);
            this.PlayerOrRobotName.Name = "PlayerOrRobotName";
            this.PlayerOrRobotName.Size = new System.Drawing.Size(203, 28);
            this.PlayerOrRobotName.TabIndex = 6;
            this.PlayerOrRobotName.Text = "PlayerOrRobotName";
            // 
            // CardText
            // 
            this.CardText.AutoSize = true;
            this.CardText.Font = new System.Drawing.Font("Bernard MT Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardText.Location = new System.Drawing.Point(12, 66);
            this.CardText.Name = "CardText";
            this.CardText.Size = new System.Drawing.Size(72, 19);
            this.CardText.TabIndex = 7;
            this.CardText.Text = "What is it?";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Bernard MT Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(224, 249);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 25);
            this.label2.TabIndex = 8;
            this.label2.Text = "Ár: ";
            // 
            // Price
            // 
            this.Price.AutoSize = true;
            this.Price.Font = new System.Drawing.Font("Bernard MT Condensed", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Price.Location = new System.Drawing.Point(263, 249);
            this.Price.Name = "Price";
            this.Price.Size = new System.Drawing.Size(22, 25);
            this.Price.TabIndex = 9;
            this.Price.Text = "0";
            // 
            // TurnText
            // 
            this.TurnText.AutoSize = true;
            this.TurnText.Font = new System.Drawing.Font("Bernard MT Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TurnText.Location = new System.Drawing.Point(12, 104);
            this.TurnText.Name = "TurnText";
            this.TurnText.Size = new System.Drawing.Size(72, 19);
            this.TurnText.TabIndex = 10;
            this.TurnText.Text = "What is it?";
            // 
            // FinishedValue
            // 
            this.FinishedValue.AutoSize = true;
            this.FinishedValue.Font = new System.Drawing.Font("Bernard MT Condensed", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FinishedValue.Location = new System.Drawing.Point(12, 143);
            this.FinishedValue.Name = "FinishedValue";
            this.FinishedValue.Size = new System.Drawing.Size(72, 19);
            this.FinishedValue.TabIndex = 11;
            this.FinishedValue.Text = "What is it?";
            // 
            // Cancel
            // 
            this.Cancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("Cancel.BackgroundImage")));
            this.Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Cancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.Cancel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Cancel.Location = new System.Drawing.Point(398, 209);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(110, 110);
            this.Cancel.TabIndex = 12;
            this.Cancel.UseVisualStyleBackColor = true;
            this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
            // 
            // PropertyCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(76)))), ((int)(((byte)(36)))));
            this.ClientSize = new System.Drawing.Size(520, 331);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.FinishedValue);
            this.Controls.Add(this.TurnText);
            this.Controls.Add(this.Price);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CardText);
            this.Controls.Add(this.PlayerOrRobotName);
            this.Controls.Add(this.Ok);
            this.ForeColor = System.Drawing.Color.Gold;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PropertyCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PropertyCard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Ok;
        private System.Windows.Forms.Label PlayerOrRobotName;
        private System.Windows.Forms.Label CardText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label Price;
        private System.Windows.Forms.Label TurnText;
        private System.Windows.Forms.Label FinishedValue;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.ToolTip ButtonTip;
    }
}