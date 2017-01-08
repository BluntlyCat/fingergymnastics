namespace HSA.FingerGymnastics.Exercises
{
    using Leap;
    using DB = DB.Models;
    using View;

    public abstract class Gesture
    {
        protected Gesture next;
        protected Gesture previous;

        protected DB.Gesture gestureModel;

        protected Marker marker;
        protected GestureController gestureController;

        protected GestureStates state = GestureStates.NotAdded;

        private double timeOffset;
        
        public Gesture(Gesture previous, DB.Gesture gesture, GestureController gestureController, double timeOffset)
        {
            this.previous = previous;
            this.gestureModel = gesture;
            this.gestureController = gestureController;
            this.timeOffset = timeOffset;
        }

        public Gesture Next
        {
            get
            {
                return next;
            }

            set
            {
                next = value;
            }
        }

        public Gesture Previous
        {
            get
            {
                return previous;
            }

            set
            {
                previous = value;
            }
        }

        public DB.Gesture GestureModel
        {
            get
            {
                return gestureModel;
            }
        }

        public Marker Marker
        {
            get
            {
                return marker;
            }
        }

        public void Proof(float time)
        {
            switch(state)
            {
                case GestureStates.NotAdded:
                    if (time >= gestureModel.StartTime.TimeOfDay.TotalSeconds - timeOffset && time < gestureModel.StartTime.TimeOfDay.TotalSeconds)
                        this.state = GestureStates.Add;
                    break;

                case GestureStates.PreReady:
                    if (time >= gestureModel.StartTime.TimeOfDay.TotalSeconds && time <= gestureModel.EndTime.TimeOfDay.TotalSeconds)
                        this.state = GestureStates.Ready;
                    break;

                case GestureStates.Ready:
                    if (time > gestureModel.EndTime.TimeOfDay.TotalSeconds)
                        this.state = GestureStates.Expired;
                    break;

                case GestureStates.Removed:
                    break;
            }
        }

        public virtual void AddMarker(Marker marker)
        {
            this.state = GestureStates.PreReady;
            this.marker = marker;
        }

        public void SetPreReady()
        {
            this.state = GestureStates.PreReady;
        }

        public void SetReady()
        {
            this.Marker.SetMarkerReady();
        }

        public void SetRemoved()
        {
            this.state = GestureStates.Removed;
        }
        
        public GestureStates State
        {
            get
            {
                return this.state;
            }
        }
    }
}
