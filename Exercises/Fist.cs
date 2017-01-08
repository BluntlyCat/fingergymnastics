namespace HSA.FingerGymnastics.Exercises
{
    using Leap;
    using View;
    using DB = DB.Models;

    public class Fist : Gesture
    {
        public Fist(Gesture previous, DB.Gesture gesture, GestureController gestureController, double timeOffset) : base(previous, gesture, gestureController, timeOffset)
        {
        }

        private void Marker_OnMarkerCollision(Marker marker, Hand hand)
        {
            if (hand.GrabStrength == 1)
            {
                this.state = GestureStates.Expired;
                gestureController.ExpireMarker(this);
            }
        }

        public override void AddMarker(Marker marker)
        {
            base.AddMarker(marker);
            marker.OnMarkerCollision += Marker_OnMarkerCollision;
        }
    }
}
