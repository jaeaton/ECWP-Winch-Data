using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MaxDataPointModel
    {
        /// <summary>
        /// Data associated with the maximum payout (tension, speed, payout, date, and time)
        /// </summary>
        public DataPointModel MaxPayout { get; set; } = new DataPointModel();
        /// <summary>
        /// Data associated with the maximum tension (tension, speed, payout, date, and time)
        /// </summary>
        public DataPointModel MaxTension { get; set; } = new DataPointModel();
        /// <summary>
        /// Data associated with the maximum speed (tension, speed, payout, date, and time)
        /// </summary>
        public DataPointModel MaxSpeed { get; set; } = new DataPointModel();

        public void Clear()
        {
            MaxPayout = new DataPointModel();
            MaxTension = new DataPointModel();
            MaxSpeed = new DataPointModel();
        }

    }
}
