namespace RocketPal
{
    partial class Form1
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
            this.NextBallAddressButton = new System.Windows.Forms.Button();
            this.NextCarAddressButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.BallAddressLabel = new System.Windows.Forms.Label();
            this.CarAddressLabel = new System.Windows.Forms.Label();
            this.RotationLabel = new System.Windows.Forms.Label();
            this.ResetBallButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.CarProgressBar = new System.Windows.Forms.ProgressBar();
            this.StartButton = new System.Windows.Forms.Button();
            this.ClockScanInfoPanel = new RocketPal.Components.MemoryScanInfoPanel();
            this.BallScanInfoPanel = new RocketPal.Components.MemoryScanInfoPanel();
            this.CarScanInfoPanel = new RocketPal.Components.MemoryScanInfoPanel();
            this.fieldViewPanel1 = new RocketPal.Components.FieldViewPanel();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // NextBallAddressButton
            // 
            this.NextBallAddressButton.Location = new System.Drawing.Point(489, 9);
            this.NextBallAddressButton.Name = "NextBallAddressButton";
            this.NextBallAddressButton.Size = new System.Drawing.Size(135, 52);
            this.NextBallAddressButton.TabIndex = 1;
            this.NextBallAddressButton.Text = "NextBallAddress";
            this.NextBallAddressButton.UseVisualStyleBackColor = true;
            this.NextBallAddressButton.Click += new System.EventHandler(this.NextBallAddressButton_Click);
            // 
            // NextCarAddressButton
            // 
            this.NextCarAddressButton.Location = new System.Drawing.Point(489, 79);
            this.NextCarAddressButton.Name = "NextCarAddressButton";
            this.NextCarAddressButton.Size = new System.Drawing.Size(135, 51);
            this.NextCarAddressButton.TabIndex = 2;
            this.NextCarAddressButton.Text = "NextCarAddress";
            this.NextCarAddressButton.UseVisualStyleBackColor = true;
            this.NextCarAddressButton.Click += new System.EventHandler(this.NextCarAddressButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(348, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 52);
            this.button1.TabIndex = 3;
            this.button1.Text = "Previous Ball";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(348, 79);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(135, 51);
            this.button2.TabIndex = 4;
            this.button2.Text = "Previous Car";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.PreviousCarAddressButton_Click);
            // 
            // BallAddressLabel
            // 
            this.BallAddressLabel.AutoSize = true;
            this.BallAddressLabel.Location = new System.Drawing.Point(630, 25);
            this.BallAddressLabel.Name = "BallAddressLabel";
            this.BallAddressLabel.Size = new System.Drawing.Size(51, 20);
            this.BallAddressLabel.TabIndex = 5;
            this.BallAddressLabel.Text = "label1";
            // 
            // CarAddressLabel
            // 
            this.CarAddressLabel.AutoSize = true;
            this.CarAddressLabel.Location = new System.Drawing.Point(630, 92);
            this.CarAddressLabel.Name = "CarAddressLabel";
            this.CarAddressLabel.Size = new System.Drawing.Size(51, 20);
            this.CarAddressLabel.TabIndex = 6;
            this.CarAddressLabel.Text = "label2";
            // 
            // RotationLabel
            // 
            this.RotationLabel.AutoSize = true;
            this.RotationLabel.Location = new System.Drawing.Point(12, 9);
            this.RotationLabel.Name = "RotationLabel";
            this.RotationLabel.Size = new System.Drawing.Size(78, 20);
            this.RotationLabel.TabIndex = 7;
            this.RotationLabel.Text = "Rotation: ";
            this.RotationLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.RotationLabel_Paint);
            // 
            // ResetBallButton
            // 
            this.ResetBallButton.Location = new System.Drawing.Point(207, 9);
            this.ResetBallButton.Name = "ResetBallButton";
            this.ResetBallButton.Size = new System.Drawing.Size(135, 52);
            this.ResetBallButton.TabIndex = 8;
            this.ResetBallButton.Text = "Reset";
            this.ResetBallButton.UseVisualStyleBackColor = true;
            this.ResetBallButton.Click += new System.EventHandler(this.ResetBallButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(207, 75);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 52);
            this.button3.TabIndex = 9;
            this.button3.Text = "Reset";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // CarProgressBar
            // 
            this.CarProgressBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.CarProgressBar.Location = new System.Drawing.Point(101, 9);
            this.CarProgressBar.Name = "CarProgressBar";
            this.CarProgressBar.Size = new System.Drawing.Size(100, 25);
            this.CarProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.CarProgressBar.TabIndex = 10;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(16, 40);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(92, 36);
            this.StartButton.TabIndex = 14;
            this.StartButton.Text = "Start";
            this.StartButton.UseVisualStyleBackColor = true;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // ClockScanInfoPanel
            // 
            this.ClockScanInfoPanel.Location = new System.Drawing.Point(690, 123);
            this.ClockScanInfoPanel.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.ClockScanInfoPanel.Name = "ClockScanInfoPanel";
            this.ClockScanInfoPanel.Size = new System.Drawing.Size(596, 48);
            this.ClockScanInfoPanel.TabIndex = 13;
            // 
            // BallScanInfoPanel
            // 
            this.BallScanInfoPanel.Location = new System.Drawing.Point(690, 66);
            this.BallScanInfoPanel.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.BallScanInfoPanel.Name = "BallScanInfoPanel";
            this.BallScanInfoPanel.Size = new System.Drawing.Size(596, 48);
            this.BallScanInfoPanel.TabIndex = 12;
            // 
            // CarScanInfoPanel
            // 
            this.CarScanInfoPanel.Location = new System.Drawing.Point(690, 9);
            this.CarScanInfoPanel.Margin = new System.Windows.Forms.Padding(6, 8, 6, 8);
            this.CarScanInfoPanel.Name = "CarScanInfoPanel";
            this.CarScanInfoPanel.Size = new System.Drawing.Size(596, 48);
            this.CarScanInfoPanel.TabIndex = 11;
            // 
            // fieldViewPanel1
            // 
            this.fieldViewPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.fieldViewPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fieldViewPanel1.GameInstance = null;
            this.fieldViewPanel1.Location = new System.Drawing.Point(0, 286);
            this.fieldViewPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.fieldViewPanel1.Name = "fieldViewPanel1";
            this.fieldViewPanel1.Size = new System.Drawing.Size(1471, 730);
            this.fieldViewPanel1.TabIndex = 0;
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Location = new System.Drawing.Point(16, 136);
            this.ConsoleTextBox.Multiline = true;
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConsoleTextBox.Size = new System.Drawing.Size(608, 141);
            this.ConsoleTextBox.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1471, 1016);
            this.Controls.Add(this.ConsoleTextBox);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.ClockScanInfoPanel);
            this.Controls.Add(this.BallScanInfoPanel);
            this.Controls.Add(this.CarScanInfoPanel);
            this.Controls.Add(this.CarProgressBar);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ResetBallButton);
            this.Controls.Add(this.RotationLabel);
            this.Controls.Add(this.CarAddressLabel);
            this.Controls.Add(this.BallAddressLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.NextCarAddressButton);
            this.Controls.Add(this.NextBallAddressButton);
            this.Controls.Add(this.fieldViewPanel1);
            this.DoubleBuffered = true;
            this.Location = new System.Drawing.Point(3900, 500);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Form1";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Components.FieldViewPanel fieldViewPanel1;
        private System.Windows.Forms.Button NextBallAddressButton;
        private System.Windows.Forms.Button NextCarAddressButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label BallAddressLabel;
        private System.Windows.Forms.Label CarAddressLabel;
        private System.Windows.Forms.Label RotationLabel;
        private System.Windows.Forms.Button ResetBallButton;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar CarProgressBar;
        private Components.MemoryScanInfoPanel CarScanInfoPanel;
        private Components.MemoryScanInfoPanel BallScanInfoPanel;
        private Components.MemoryScanInfoPanel ClockScanInfoPanel;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.TextBox ConsoleTextBox;
    }
}

