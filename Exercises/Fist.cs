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
    }
}
