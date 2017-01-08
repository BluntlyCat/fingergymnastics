namespace HSA.FingerGymnastics.Exercises
{
    using System;
    using Leap;
    using DB = DB.Models;
    using View;

    public class ExtendedHand : Gesture
    {
        public ExtendedHand(Gesture previous, DB.Gesture gesture, GestureController gestureController, double timeOffset) : base (previous, gesture, gestureController, timeOffset)
        {
            
        }

        private void Marker_OnMarkerCollision(Marker marker, Hand hand)
        {
            if (hand.GrabStrength == 0)
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
