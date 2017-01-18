namespace HSA.FingerGymnastics.DB.Models
{
    using Exercises;
    using Mhaze.Unity.DB.Models;
    using System;

    public class Gesture : Model
    {
        private long id;

        private DateTime startTime;
        private DateTime endTime;
        private DateTime duration;
        private long gestureType;

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

        [TableColumn("kind")]
        private long LongGestureType
        {
            get
            {
                return this.gestureType;
            }

            set
            {
                this.gestureType = value;
            }
        }

        public Gestures GestureType
        {
            get
            {
                return (Gestures)this.gestureType;
            }

            set
            {
                this.gestureType = (long)value;
            }
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}ms", id, startTime.TimeOfDay.TotalMilliseconds);
        }
    }
}
