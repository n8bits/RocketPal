using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RocketPal.Controller;
using RocketPal.Models.GameObjects;

namespace RocketPal
{
    public partial class Form1 : Form
    {
        private GameInstance instance;

        public Form1()
        {
            InitializeComponent();
           
        }

        private void NextBallAddressButton_Click(object sender, EventArgs e)
        {
            this.fieldViewPanel1.GameInstance.Ball.UseNextAddress();
            this.UpdateAddressLabels();
        }

        private void NextCarAddressButton_Click(object sender, EventArgs e)
        {
            this.fieldViewPanel1.GameInstance.PlayerCar.UseNextAddress();
            this.UpdateAddressLabels();
        }

        private void PreviousCarAddressButton_Click(object sender, EventArgs e)
        {
            this.UpdateAddressLabels();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.UpdateAddressLabels();
        }

        public void UpdateAddressLabels()
        {
            this.CarAddressLabel.Text = this.fieldViewPanel1.GameInstance.PlayerCar.MemoryAddress + "";
            this.BallAddressLabel.Text = this.fieldViewPanel1.GameInstance.Ball.MemoryAddress + "";
        }

        private void RotationLabel_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void ResetBallButton_Click(object sender, EventArgs e)
        {
            Ball ball = Ball.GetBall();
            this.fieldViewPanel1.GameInstance.Ball = ball;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Car car = Car.GetCar();
            this.fieldViewPanel1.GameInstance.PlayerCar = car;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            this.StartButton.Enabled = false;
            this.instance = new GameInstance();
            instance.CarInfoPanel = this.CarScanInfoPanel;
            instance.BallInfoPanel = this.BallScanInfoPanel;
            instance.ClockInfoPanel = this.ClockScanInfoPanel;
            instance.Console = this.ConsoleTextBox;

            instance.Start();
            instance.ap.AvoidIdle();
            this.fieldViewPanel1.GameInstance = instance;
            this.fieldViewPanel1.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (this.instance != null && this.instance.gameOver)
            {
                instance.LogMessage("Game is over, resetting...");
                this.instance = new GameInstance();
                instance.CarInfoPanel = this.CarScanInfoPanel;
                instance.BallInfoPanel = this.BallScanInfoPanel;
                instance.ClockInfoPanel = this.ClockScanInfoPanel;
                instance.Console = this.ConsoleTextBox;

                instance.Start();
                instance.ap.AvoidIdle();
                this.fieldViewPanel1.GameInstance = instance;
                this.fieldViewPanel1.Start();
                instance.LogMessage("Fresh instance!");
            }
        }
    }
}
