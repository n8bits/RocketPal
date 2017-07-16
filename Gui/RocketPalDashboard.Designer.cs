namespace RocketPal.Gui
{
    partial class RocketPalDashboard
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
            this.StartButton = new System.Windows.Forms.Button();
            this.FindClockButton = new System.Windows.Forms.Button();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.Status = new System.Windows.Forms.ToolStripStatusLabel();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.SteeringSlider = new System.Windows.Forms.TrackBar();
            this.ClockSearchProgress = new RocketPal.Components.MemoryScanInfoPanel();
            this.RunBlindBotButton = new System.Windows.Forms.Button();
            this.StatusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SteeringSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(193, 98);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(173, 60);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "Find Match";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // FindClockButton
            // 
            this.FindClockButton.Location = new System.Drawing.Point(635, 98);
            this.FindClockButton.Name = "FindClockButton";
            this.FindClockButton.Size = new System.Drawing.Size(118, 45);
            this.FindClockButton.TabIndex = 2;
            this.FindClockButton.Text = "Locate Clock";
            this.FindClockButton.UseVisualStyleBackColor = true;
            this.FindClockButton.Click += new System.EventHandler(this.FindClockButton_Click);
            // 
            // StatusStrip
            // 
            this.StatusStrip.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Status});
            this.StatusStrip.Location = new System.Drawing.Point(0, 241);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(776, 22);
            this.StatusStrip.TabIndex = 6;
            this.StatusStrip.Text = "Idle";
            // 
            // Status
            // 
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(42, 17);
            this.Status.Text = "Ready.";
            // 
            // trackBar1
            // 
            this.trackBar1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.trackBar1.LargeChange = 2;
            this.trackBar1.Location = new System.Drawing.Point(79, 28);
            this.trackBar1.Minimum = -10;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(287, 45);
            this.trackBar1.TabIndex = 8;
            this.trackBar1.ValueChanged += new System.EventHandler(this.trackBar1_ValueChanged);
            // 
            // SteeringSlider
            // 
            this.SteeringSlider.Location = new System.Drawing.Point(404, 28);
            this.SteeringSlider.Minimum = -10;
            this.SteeringSlider.Name = "SteeringSlider";
            this.SteeringSlider.Size = new System.Drawing.Size(332, 45);
            this.SteeringSlider.TabIndex = 9;
            this.SteeringSlider.ValueChanged += new System.EventHandler(this.SteeringSlider_ValueChanged);
            // 
            // ClockSearchProgress
            // 
            this.ClockSearchProgress.Location = new System.Drawing.Point(36, 190);
            this.ClockSearchProgress.Name = "ClockSearchProgress";
            this.ClockSearchProgress.Size = new System.Drawing.Size(397, 31);
            this.ClockSearchProgress.TabIndex = 1;
            // 
            // RunBlindBotButton
            // 
            this.RunBlindBotButton.Location = new System.Drawing.Point(635, 149);
            this.RunBlindBotButton.Name = "RunBlindBotButton";
            this.RunBlindBotButton.Size = new System.Drawing.Size(118, 45);
            this.RunBlindBotButton.TabIndex = 10;
            this.RunBlindBotButton.Text = "Run Blind Bot";
            this.RunBlindBotButton.UseVisualStyleBackColor = true;
            this.RunBlindBotButton.Click += new System.EventHandler(this.RunBlindBotButton_Click);
            // 
            // RocketPalDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 263);
            this.Controls.Add(this.RunBlindBotButton);
            this.Controls.Add(this.SteeringSlider);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.FindClockButton);
            this.Controls.Add(this.ClockSearchProgress);
            this.Controls.Add(this.StartButton);
            this.Name = "RocketPalDashboard";
            this.Text = "RocketPalDashboard";
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SteeringSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartButton;
        private Components.MemoryScanInfoPanel ClockSearchProgress;
        private System.Windows.Forms.Button FindClockButton;
        private System.Windows.Forms.StatusStrip StatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel Status;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.TrackBar SteeringSlider;
        private System.Windows.Forms.Button RunBlindBotButton;
    }
}