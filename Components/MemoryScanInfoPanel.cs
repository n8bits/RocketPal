using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RocketPal.Components
{
    public partial class MemoryScanInfoPanel : UserControl
    {
        public MemoryScanInfoPanel()
        {
            InitializeComponent();
        }

        private void MemoryScanInfoPanel_Click(object sender, EventArgs e)
        {

        }


        public delegate void InvokeDelegate();


        public void SetCurrentBlock(int completed, int total)
        {
            var percentComplete = (float)completed / (float)total;
            this.TopLevelControl.BeginInvoke(new InvokeDelegate(() =>
            {
                this.ProgressBar.Value = ProgressBar.Value = (int)(ProgressBar.Maximum * percentComplete);
                this.CurrentBlockLabel.Text = "Block " + completed + " of " + total;
                this.Update();
            }));

            
        }
    }
}
