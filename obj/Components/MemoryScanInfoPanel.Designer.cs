namespace RocketPal.Components
{
    partial class MemoryScanInfoPanel
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
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.CurrentBlockLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgressBar.Location = new System.Drawing.Point(127, 3);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(267, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // CurrentBlockLabel
            // 
            this.CurrentBlockLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentBlockLabel.AutoSize = true;
            this.CurrentBlockLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CurrentBlockLabel.Location = new System.Drawing.Point(10, 3);
            this.CurrentBlockLabel.Name = "CurrentBlockLabel";
            this.CurrentBlockLabel.Size = new System.Drawing.Size(111, 20);
            this.CurrentBlockLabel.TabIndex = 1;
            this.CurrentBlockLabel.Text = "Block 1 of 2345";
            // 
            // MemoryScanInfoPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CurrentBlockLabel);
            this.Controls.Add(this.ProgressBar);
            this.Name = "MemoryScanInfoPanel";
            this.Size = new System.Drawing.Size(397, 31);
            this.Click += new System.EventHandler(this.MemoryScanInfoPanel_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label CurrentBlockLabel;
    }
}
