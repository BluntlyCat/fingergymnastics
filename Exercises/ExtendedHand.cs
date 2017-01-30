namespace HSA.FingerGymnastics.Exercises
{
    using Leap;
    using View;
    using DB = DB.Models;

    public class ExtendedHand : BaseGesture
    {
        public ExtendedHand(DB.Gesture gesture, GestureController gestureController, double timeOffset) : base (gestureController, timeOffset, gesture)
        {
            
        }

        protected override bool IsGesture(Hand hand)
        {
            return hand.GrabStrength < 0.1;
        }
    }
}
