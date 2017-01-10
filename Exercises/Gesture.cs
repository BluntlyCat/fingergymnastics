namespace HSA.FingerGymnastics.Exercises
{
    using Leap;
    using Mhaze.Unity.Logging;
    using View;
    using DB = DB.Models;

    public abstract class Gesture
    {
        protected static Logger<Gesture> logger = new Logger<Gesture>();

        protected DB.Gesture gestureModel;

        protected Marker marker;
        protected GestureController gestureController;

        protected GestureStates state = GestureStates.NotAdded;

        private double timeOffset;
        
        public Gesture(DB.Gesture gesture, GestureController gestureController, double timeOffset)
        {
            logger.AddLogAppender<ConsoleAppender>();

            this.gestureModel = gesture;
            this.gestureController = gestureController;
            this.timeOffset = timeOffset;
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
            }
        }

        public virtual void AddMarker(Marker marker)
        {
            this.state = GestureStates.PreReady;
            this.marker = marker;
        }
        
        public void SetReady()
        {
            this.Marker.SetMarkerReady();
        }
        
        public void SetState(GestureStates state)
        {
            this.state = state;
        }
        
        public GestureStates State
        {
            get
            {
                return this.state;
            }
        }

        protected abstract bool IsGesture(Hand hand);
    }
}
