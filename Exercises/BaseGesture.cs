namespace HSA.FingerGymnastics.Exercises
{
    using Leap;
    using Mhaze.Unity.Logging;
    using View;
    using DB = DB.Models;

    public abstract class BaseGesture : DB.Gesture
    {
        protected Marker marker;
        protected GestureController gestureController;

        protected GestureStates state = GestureStates.NotAdded;

        private double timeOffset;
        
        public BaseGesture(GestureController gestureController, double timeOffset, DB.Gesture gesture) : base (gesture)
        {
            logger.AddLogAppender<ConsoleAppender>();

            this.gestureController = gestureController;
            this.timeOffset = timeOffset;
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
                    if (time >= this.StartTime.TimeOfDay.TotalSeconds - timeOffset && time < this.StartTime.TimeOfDay.TotalSeconds)
                        this.state = GestureStates.Add;
                    break;

                case GestureStates.PreReady:
                    if (time >= this.StartTime.TimeOfDay.TotalSeconds && time <= this.EndTime.TimeOfDay.TotalSeconds)
                        this.state = GestureStates.Ready;
                    break;
            }
        }

        private void CountDown_OnTimeOut(BaseGesture gesture)
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
            this.Marker.SetMarkerActive(this.Duration, this, state);
            marker.Collider.OnMarkerCollision += Marker_OnMarkerCollision;
        }

        private void Marker_OnMarkerCollision(Marker marker, BaseGesture gesture, Hand hand)
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
                return (float)this.StartTime.TimeOfDay.TotalMilliseconds;
            }
        }

        protected abstract bool IsGesture(Hand hand);
    }
}
