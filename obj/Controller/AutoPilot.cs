using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WindowsInput;
using RocketPal.Models.GameObjects;

namespace RocketPal.Controller
{
    public class AutoPilot
    {
        public GameInstance GameInstance { get; set; }

        public RocketLeagueController Controller { get; set; }

        private BackgroundWorker aiWorker;

        private static float[] pidValues = {1.0f, 1.0f, 1.0f};

        private float errorIntegrator = 0;

        public float lastError;

        private float[] lastBallLocation = {0f, 0f};

        private float lastCarX = 0;

        private bool engaged = false;

        private bool drivingBlind = false;

        private bool avoidingIdle = false;

        public float ProportionalConstant = 1f / 12f;

        public float DerivativeConstant = 1f / 500f;

        public float IntegratorConstant = 1f/1000000f;

        public float PowerSlideErrorThreshold = 45f;

        public int millisecondDelay = 50;

        public Location CurrentTarget;

        private BackgroundWorker targeter;

        public bool IsEngaged
        {
            get
            {
                return this.engaged;
            }
        }

        public AutoPilot(GameInstance instance)
        {
            this.GameInstance = instance;


        }

        public void DriveBlindlyUntilBallIsFound()
        {
            if (!this.drivingBlind && engaged)
            {
                
               // this.Controller.Boost = true;
                this.drivingBlind = true;
            }
        }
        
        public void ResetController()
        {
            this.Controller = new RocketLeagueController();
            Controller.EnableSteeringAndThrottle();
        }

        public void Engage()
        {
            if (!engaged)
            {
                this.engaged = true;
                this.ResetController();

                aiWorker = new BackgroundWorker();
                aiWorker.DoWork += this.AiWorkerThread;
                aiWorker.RunWorkerAsync();
            }
        }

        public void Disengage()
        {
            this.engaged = false;

            if (Controller != null)
            {
                this.Controller.Connected = false;
                this.Controller = null;
            }
        }

        public void AvoidIdle()
        {
            if(!this.avoidingIdle)
            {
                this.avoidingIdle = true;
                this.Controller.Throttle = .3f;
                this.Controller.SteeringPosition = -.8f;
            }
        }

        private void AiWorkerThread(object sender, DoWorkEventArgs args)
        {
            lastError = 0;

            while (engaged)
            {
                if (this.GameInstance.CarFound() != true && this.avoidingIdle)
                {
                    var increment = (float) (new Random().Next(-100, 100))/4000f;

                    this.Controller.SteeringPosition += (float)increment;   
                    
                    Thread.Sleep(millisecondDelay);
                    continue;
                }
                else
                {
                    this.avoidingIdle = false;
                }

                // No car and no ball found, floor it
                if (!this.GameInstance.CarFound() && this.drivingBlind)
                {
                    //this.Controller.Boost = true;
                }
                else if (this.GameInstance.CarFound())
                {   
                    // If the car is found but not the ball, and the current target is the ball (null), then switch the target to the nearest peanut
                    if (!this.GameInstance.BallFound() && this.CurrentTarget == null && this.drivingBlind)
                    {
                        GoToTarget(Peanut.NearestPeanut(this.GameInstance.PlayerCar.Location));
                    }
                    // If both the car and the ball are found, stop driving blindly and resume normal AI
                    else if (this.GameInstance.GameReady)
                    {
                        this.drivingBlind = false;
                    }

                    Random random = new Random();
                    var rand = random.Next(250);

                    if (CurrentTarget == null && rand == 22)
                    {
                        GoToTarget(Peanut.NearestPeanut(this.GameInstance.PlayerCar.Location));
                    }
                    try
                    {
                        var error = this.GetAngleError();
                        errorIntegrator += (error*-IntegratorConstant);
                        if (Math.Abs(errorIntegrator) >= 4)
                        {
                            errorIntegrator = 0;
                        }

                        var dError = lastError - error;
                        this.lastError = error;
                        var distance = (float) this.GameInstance.DistanceToBall();

                        Controller.Throttle = (distance < 1000 && Math.Abs(error) > 45) ? (distance/1000f) : 1;

                        if (Math.Abs(error) < 15)
                        {
                            Controller.Throttle = 1f;

                            if (distance > 4500 || distance < 300)
                            {
                                this.Controller.FrontFlip();
                            }
                            else
                            {
                                Controller.Boost = true;
                            }
                        }
                        else
                        {
                            Controller.Boost = false;
                        }

                        this.ApplyPowerSlideIfNecessary(error);

                        if (error > 0)
                        {

                            Controller.SteeringPosition = (dError*this.DerivativeConstant) +
                                                          (error*(-ProportionalConstant)) + (errorIntegrator);
                        }
                        else if (error < 0)
                        {
                            Controller.SteeringPosition = (dError*this.DerivativeConstant) +
                                                          (error*(-ProportionalConstant)) + (errorIntegrator);
                        }
                        this.JumpOffWallIfNecessary();
                    }
                    catch (NullReferenceException ex)
                    {
                        //this.DriveBlindlyUntilBallIsFound();
                    }

                }

                //CheckStaleCar();
                //CheckStaleBall();
                Thread.Sleep(millisecondDelay);
            }
            
        }

        public void ApplyPowerSlideIfNecessary(float error)
        {
            if (Math.Abs(error) > this.PowerSlideErrorThreshold && Math.Abs(error) < 100f)
            {
                this.Controller.SetPowerSlide(true);
            }
            else
            {
                this.Controller.SetPowerSlide(false);
            }
        }

        public void JumpOffWallIfNecessary()
        {
            if (Math.Abs((this.GameInstance.PlayerCar.X)) > GameObject.WallBoundryX || Math.Abs((this.GameInstance.PlayerCar.Y)) > GameObject.WallBoundryY)
            {
                this.Controller.Jump();
            }
        }

        public void GoToTarget(Location location)
        {
            this.CurrentTarget = location;

            this.targeter = new BackgroundWorker();
            targeter.DoWork += this.TargetingThread;
            targeter.RunWorkerAsync();
        }

        public void TargetingThread(object sender, DoWorkEventArgs args)
        {
            try
            {
                while (this.DistanceToTarget > this.CurrentTarget.Radius)
                {
                    Thread.Sleep(1);
                }
            }
            catch (NullReferenceException ex)
            {
                
            }
           

            this.CurrentTarget = null;
        }

        public double DistanceToTarget
        {
            get
            {
                try
                {
                    double distance = 0;

                    if (this.CurrentTarget == null && this.GameInstance.BallFound())
                    {
                        distance = this.GameInstance.DistanceToBall();
                    }else if (this.CurrentTarget != null)
                    {
                        this.GameInstance.PlayerCar.Location.DistanceTo(this.CurrentTarget);
                    }

                    if (distance < this.CurrentTarget.Radius)
                    {
                        this.CurrentTarget = null;
                    }

                    return distance;
                }
                catch(Exception e)
                {
                    return 0;
                }

            }
        }
        public float GetAngleError()
        {
            var angle = this.CurrentTarget == null ? this.GameInstance.AngleToBall : this.GameInstance.AngleToLocation(this.CurrentTarget);
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

            return (float) error;
        }

    }
}
