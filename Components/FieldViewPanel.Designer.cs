namespace RocketPal.Components
{
    partial class FieldViewPanel
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
            this.ClockLabel = new System.Windows.Forms.Label();
            this.ErrorLabel = new System.Windows.Forms.Label();
            this.BoostLabel = new System.Windows.Forms.Label();
            this.TargetDistanceLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClockLabel
            // 
            this.ClockLabel.AutoSize = true;
            this.ClockLabel.Location = new System.Drawing.Point(3, 0);
            this.ClockLabel.Name = "ClockLabel";
            this.ClockLabel.Size = new System.Drawing.Size(36, 20);
            this.ClockLabel.TabIndex = 11;
            this.ClockLabel.Text = "300";
            // 
            // ErrorLabel
            // 
            this.ErrorLabel.AutoSize = true;
            this.ErrorLabel.Location = new System.Drawing.Point(3, 20);
            this.ErrorLabel.Name = "ErrorLabel";
            this.ErrorLabel.Size = new System.Drawing.Size(36, 20);
            this.ErrorLabel.TabIndex = 13;
            this.ErrorLabel.Text = "300";
            // 
            // BoostLabel
            // 
            this.BoostLabel.AutoSize = true;
            this.BoostLabel.Location = new System.Drawing.Point(3, 40);
            this.BoostLabel.Name = "BoostLabel";
            this.BoostLabel.Size = new System.Drawing.Size(36, 20);
            this.BoostLabel.TabIndex = 14;
            this.BoostLabel.Text = "300";
            // 
            // TargetDistanceLabel
            // 
            this.TargetDistanceLabel.AutoSize = true;
            this.TargetDistanceLabel.Location = new System.Drawing.Point(3, 60);
            this.TargetDistanceLabel.Name = "TargetDistanceLabel";
            this.TargetDistanceLabel.Size = new System.Drawing.Size(18, 20);
            this.TargetDistanceLabel.TabIndex = 15;
            this.TargetDistanceLabel.Text = "0";
            // 
            // FieldViewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TargetDistanceLabel);
            this.Controls.Add(this.BoostLabel);
            this.Controls.Add(this.ErrorLabel);
            this.Controls.Add(this.ClockLabel);
            this.Name = "FieldViewPanel";
            this.Size = new System.Drawing.Size(1023, 932);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FieldViewPanel_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ClockLabel;
        private System.Windows.Forms.Label ErrorLabel;
        private System.Windows.Forms.Label BoostLabel;
        private System.Windows.Forms.Label TargetDistanceLabel;
    }
}
