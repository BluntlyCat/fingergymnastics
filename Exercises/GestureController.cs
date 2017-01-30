namespace HSA.FingerGymnastics.Exercises
{
    using Game;
    using System;
    using System.Collections.Generic;
    using View;
    using DB = DB.Models;

    public class GestureController
    {
        private IList<BaseGesture> gestures;
        private IList<BaseGesture> removedGestures;

        private ViewManager viewManager;
        private float indicatorVelocityMS;

        public GestureController(ViewManager viewManager, float indicatorVelocityMS)
        {
            this.gestures = new List<BaseGesture>();
            this.removedGestures = new List<BaseGesture>();
            this.viewManager = viewManager;
            this.indicatorVelocityMS = indicatorVelocityMS;
        }

        public void AddGesture(DB.Gesture gesture, double timeOffset)
        {
            BaseGesture newGesture = null;

            switch (gesture.GestureType)
            {
                case Gestures.ExtendedHand:
                    newGesture = new ExtendedHand(gesture, this, timeOffset);
                    break;

                case Gestures.Fist:
                    newGesture = new Fist(gesture, this, timeOffset);
                    break;
            }

            gestures.Add(newGesture);
        }

        public void ExpireMarker(BaseGesture gesture)
        {
            GameState.Score = gesture.Marker.DisableMarker(GameState.Score, GameState.MaxScore, gesture.State);
            removedGestures.Add(gesture);
        }

        public void Proof(float time)
        {
            try
            {
                foreach (BaseGesture gesture in gestures)
                {
                    switch (gesture.State)
                    {
                        case GestureStates.NotAdded:
                            gesture.Proof(time);
                            break;

                        case GestureStates.Add:
                            Marker marker = viewManager.AddMarker(gesture, indicatorVelocityMS);
                            gesture.AddMarker(marker);
                            gesture.SetState(GestureStates.PreReady);
                            break;

                        case GestureStates.PreReady:
                            gesture.Proof(time);
                            break;

                        case GestureStates.Ready:
                            gesture.SetActive();
                            gesture.Proof(time);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int GestureCount
        {
            get
            {
                return gestures.Count;
            }
        }

        public int RemovedGestureCount
        {
            get
            {
                return removedGestures.Count;
            }
        }
    }
}
