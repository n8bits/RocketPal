using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RocketPal.Memory
{
    public class FloatRange
    {
        private float minimumValue;

        private float maximumValue;

        public float[] RangePairs { get; set; }

        public bool IsNan { get; set; }


        public FloatRange(float minumumValue, float maximumValue)
        {
            this.MinumumValue = minumumValue;
            this.MaximumValue = maximumValue;
        }

        public FloatRange()
        {

        }

        public static FloatRange Any()
        {
            return new FloatRange();
        }

        public static FloatRange NaN()
        {
            var range = new FloatRange();
            range.IsNan = true;
            
            return range;
        }

        public float MinumumValue
        {
            get { return RangePairs?[0] ?? this.minimumValue; }

            set
            {
                this.minimumValue = value;
            }
        }

        public float MaximumValue
        {
            get { return RangePairs?.Last() ?? this.maximumValue; }

            set
            {
                this.maximumValue = value;
            }
        }


        public FloatRange(float[] rangePairs)
        {
            this.RangePairs = rangePairs;
        }

        public bool Contains(float value)
        {
            if (value == float.NaN)
            {
                if (this.IsNan)
                {
                    return true;
                }

                return false;
            }

            if (RangePairs == null && this.minimumValue == 0 && this.maximumValue == 0)
            {
                return true;
            }

            if (RangePairs == null)
            {
                return value >= this.MinumumValue && value <= this.MaximumValue;
            }
            else
            {
                for (int i = 0; i < this.RangePairs.Count(); i += 2)
                {
                    if (value >= this.RangePairs[i] && value <= this.RangePairs[i + 1])
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
