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
            }
        }

        private void CountDown_OnTimeOut(Gesture gesture)
        {
            this.state = GestureStates.NotHit;
            marker.Collider.CanCollide = false;
            gestureController.ExpireMarker(gesture);
        }

        public virtual void AddMarker(Marker marker)
        {
            this.state = GestureStates.PreReady;
            this.marker = marker;

            marker.CountDown.OnTimeOut += CountDown_OnTimeOut;
        }
        
        public void SetActive()
        {
            this.state = GestureStates.Active;
            this.Marker.SetMarkerActive(gestureModel.Duration, this, state);
            marker.Collider.OnMarkerCollision += Marker_OnMarkerCollision;
        }

        private void Marker_OnMarkerCollision(Marker marker, Gesture gesture, Hand hand)
        {
            if (gesture.IsGesture(hand) && this.state == GestureStates.Active)
            {
                this.state = GestureStates.Hit;
                marker.Collider.CanCollide = false;
                gestureController.ExpireMarker(gesture);
            }
            else
                marker.Collider.CanCollide = true;
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

        public float StartPosition
        {
            get
            {
                return (float)gestureModel.StartTime.TimeOfDay.TotalMilliseconds;
            }
        }

        protected abstract bool IsGesture(Hand hand);
    }
}
