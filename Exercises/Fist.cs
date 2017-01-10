namespace HSA.FingerGymnastics.Exercises
{
    using Leap;
    using View;
    using DB = DB.Models;

    public class Fist : Gesture
    {
        public Fist(DB.Gesture gesture, GestureController gestureController, double timeOffset) : base(gesture, gestureController, timeOffset)
        {
        }

        protected override bool IsGesture(Hand hand)
        {
            return hand.GrabStrength > 0.9;
        }

        private void Marker_OnMarkerCollision(Marker marker, Hand hand)
        {
            if (IsGesture(hand) && this.state == GestureStates.Ready)
            {
                this.state = GestureStates.Hit;
                marker.Collider.CanCollide = false;
                gestureController.ExpireMarker(this);
            }
            else
                marker.Collider.CanCollide = true;
        }

        public override void AddMarker(Marker marker)
        {
            base.AddMarker(marker);
            marker.Collider.OnMarkerCollision += Marker_OnMarkerCollision;
        }
    }
}
