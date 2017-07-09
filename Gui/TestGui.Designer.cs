namespace RocketPal.Gui
{
    partial class TestGui
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
            this.button1 = new System.Windows.Forms.Button();
            this.GoToRootButton = new System.Windows.Forms.Button();
            this.LaunchNewInstanceButton = new System.Windows.Forms.Button();
            this.FindCarButton = new System.Windows.Forms.Button();
            this.CarScanInfoPanel = new RocketPal.Components.MemoryScanInfoPanel();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(237, 115);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GoToRootButton
            // 
            this.GoToRootButton.Location = new System.Drawing.Point(237, 144);
            this.GoToRootButton.Name = "GoToRootButton";
            this.GoToRootButton.Size = new System.Drawing.Size(75, 23);
            this.GoToRootButton.TabIndex = 1;
            this.GoToRootButton.Text = "Go To Root";
            this.GoToRootButton.UseVisualStyleBackColor = true;
            this.GoToRootButton.Click += new System.EventHandler(this.GoToRootButton_Click);
            // 
            // LaunchNewInstanceButton
            // 
            this.LaunchNewInstanceButton.Location = new System.Drawing.Point(197, 218);
            this.LaunchNewInstanceButton.Name = "LaunchNewInstanceButton";
            this.LaunchNewInstanceButton.Size = new System.Drawing.Size(170, 23);
            this.LaunchNewInstanceButton.TabIndex = 2;
            this.LaunchNewInstanceButton.Text = "Launch New Instance";
            this.LaunchNewInstanceButton.UseVisualStyleBackColor = true;
            this.LaunchNewInstanceButton.Click += new System.EventHandler(this.LaunchNewInstanceButton_Click);
            // 
            // FindCarButton
            // 
            this.FindCarButton.Location = new System.Drawing.Point(237, 270);
            this.FindCarButton.Name = "FindCarButton";
            this.FindCarButton.Size = new System.Drawing.Size(75, 23);
            this.FindCarButton.TabIndex = 3;
            this.FindCarButton.Text = "Find Car";
            this.FindCarButton.UseVisualStyleBackColor = true;
            this.FindCarButton.Click += new System.EventHandler(this.FindCarButton_Click);
            // 
            // CarScanInfoPanel
            // 
            this.CarScanInfoPanel.Location = new System.Drawing.Point(67, 412);
            this.CarScanInfoPanel.Name = "CarScanInfoPanel";
            this.CarScanInfoPanel.Size = new System.Drawing.Size(397, 31);
            this.CarScanInfoPanel.TabIndex = 4;
            // 
            // StatusStrip
            // 
            this.StatusStrip.Location = new System.Drawing.Point(0, 228);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(494, 22);
            this.StatusStrip.TabIndex = 5;
            this.StatusStrip.Text = "Idle";
            // 
            // TestGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 250);
            this.Controls.Add(this.StatusStrip);
            this.Controls.Add(this.CarScanInfoPanel);
            this.Controls.Add(this.FindCarButton);
            this.Controls.Add(this.LaunchNewInstanceButton);
            this.Controls.Add(this.GoToRootButton);
            this.Controls.Add(this.button1);
            this.Name = "TestGui";
            this.Text = "TestGui";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button GoToRootButton;
        private System.Windows.Forms.Button LaunchNewInstanceButton;
        private System.Windows.Forms.Button FindCarButton;
        private Components.MemoryScanInfoPanel CarScanInfoPanel;
        private System.Windows.Forms.StatusStrip StatusStrip;
    }
}