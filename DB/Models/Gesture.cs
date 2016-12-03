namespace HSA.FingerGymnastics.DB.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Gesture : Model
    {
        private long id;

        private DateTime startTime;
        private DateTime endTime;
        private DateTime duration;

        public Gesture(long id)
        {
            this.id = id;
        }

        [PrimaryKey]
        public long ID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        [TableColumn("start_time")]
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }

            set
            {
                this.startTime = value;
            }
        }

        [TableColumn("end_time")]
        public DateTime EndTime
        {
            get
            {
                return this.endTime;
            }

            set
            {
                this.endTime = value;
            }
        }

        [TableColumn]
        public DateTime Duration
        {
            get
            {
                return this.duration;
            }

            set
            {
                this.duration = value;
            }
        }
    }
}
