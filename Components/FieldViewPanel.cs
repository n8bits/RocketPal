using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RocketPal.Models.GameObjects;

namespace RocketPal.Components
{
    public partial class FieldViewPanel : UserControl
    {
        private static int FieldWidth = 8000;

        private static int FieldLength = 12000;

        private bool drawFinished = true;

        private Point lastBallLocation;

        public GameInstance GameInstance { get; set; }

        private Timer timer = new Timer();

        public FieldViewPanel()
        {
            InitializeComponent();

            
            
        }

        public int DrawWidth => (int)(this.Width * .55f);

        public int DrawHeight => (int)(this.DrawWidth * .75f);

        public int DrawLocationX => (this.Width - this.DrawWidth) / 2;

        public int DrawLocationY => 10;

        public void Start()
        {
            timer.Enabled = true;
            timer.Tick += new EventHandler(onTick);
            timer.Interval = 100;
        }

        private void onTick(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void FieldViewPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this.timer.Enabled == false)
            {
                return;
            }
            if (this.drawFinished = false)
            {
                return;
            }
            this.drawFinished = false;

            if (true)
            {
                try
                {
                    var graphics = e.Graphics;

                graphics.DrawRectangle(Pens.Black, this.DrawLocationX, this.DrawLocationY, this.DrawWidth, this.DrawHeight);

                GameObject[] objects = { this.GameInstance.PlayerCar, this.GameInstance.Ball };

                //if (!GameObject.CheckObjectsWithinBounds(objects))
                //{
                //    return;
                //}

                var carLocation = new Point(0,0);
                var ballLocation = new Point(0,0);

                if (this.GameInstance.CarFound())
                {
                    carLocation = this.TranslatePoint(-this.GameInstance.PlayerCar.X, this.GameInstance.PlayerCar.Y);
                    graphics.FillEllipse(Brushes.Blue, carLocation.X - 5, carLocation.Y - 5, 10, 10);
                }

                if (this.GameInstance.BallFound())
                {
                    ballLocation = this.TranslatePoint(-this.GameInstance.Ball.X, this.GameInstance.Ball.Y);
                    graphics.FillEllipse(Brushes.Red, ballLocation.X - 5, ballLocation.Y - 5, 10, 10);
                }

                if (GameInstance != null && GameInstance.PlayerCar != null && GameInstance.Ball != null)
                {
                   
                        var arrowLength = 30;
                        var arrowX = carLocation.X + (this.GameInstance.PlayerCar.RotationCos * arrowLength * 1);
                        var arrowY = carLocation.Y - (this.GameInstance.PlayerCar.RotationSin * arrowLength * 1);

                        var angle = this.GameInstance.AngleToBall;
                        var carAngle = this.GameInstance.PlayerCar.RotationZ;
                        var angle1 = (angle - carAngle);
                        var angle2 = 360 - angle1;
                        var error = angle1;

                        if (error < -180)
                        {
                            error = -(-360 - error);
                        }
                        else if (error > 180)
                        {
                            error = -(360 - error);
                        }

                        graphics.DrawString(error + "", this.Parent.Font, Brushes.Black, 100, 100);
                        graphics.DrawString((360 - (carAngle - angle)) + "", this.Parent.Font, Brushes.Black, 100, 300);

                        graphics.DrawLine(Pens.Blue, carLocation.X, carLocation.Y, (int)arrowX, (int)arrowY);
                    }
                    
                }catch (OverflowException ex)
                {

                }



            }
            if (this.GameInstance.Clock != null)
            {
                this.ClockLabel.Text = "" + (int)this.GameInstance.Clock.TimeRemaining;
            }

            this.ErrorLabel.Text = this.GameInstance.ap.lastError +"";
            this.BoostLabel.Text = this.GameInstance.Boost == null ? "300" : this.GameInstance.Boost.RemainingBoost + "";
            this.TargetDistanceLabel.Text = this.GameInstance.ap.DistanceToTarget + "";
            this.drawFinished = true;
        }

        public Point TranslatePoint(float y, float x)
        {
            float shiftX = x + (FieldLength / 2);
            float shiftY = y + (FieldWidth / 2);

            var drawX = ((shiftX / FieldLength) * this.DrawWidth) + DrawLocationX;
            var drawY = ((shiftY / FieldWidth) * this.DrawHeight) + DrawLocationY;

            return new Point((int)drawX, (int)drawY);
        }
    }
}
