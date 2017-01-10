namespace HSA.FingerGymnastics.Exercises
{
    using Controller;
    using Game;
    using System;
    using System.Collections.Generic;
    using View;
    using DB = DB.Models;

    public class GestureController
    {
        private IList<Gesture> gestures;
        private IList<Gesture> removedGestures;

        private ViewManager viewManager;

        public GestureController(ViewManager viewManager)
        {
            this.gestures = new List<Gesture>();
            this.removedGestures = new List<Gesture>();

            this.viewManager = viewManager;
        }

        public void AddGesture(DB.Gesture gesture, double timeOffset)
        {
            Gesture newGesture = null;

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

        public void ExpireMarker(Gesture gesture)
        {
            GameState.Score = gesture.Marker.DisableMarker(GameState.Score, GameState.MaxScore, gesture.State);
            removedGestures.Add(gesture);
        }

        public void Proof(float time)
        {
            try
            {
                foreach (Gesture gesture in gestures)
                {
                    switch (gesture.State)
                    {
                        case GestureStates.NotAdded:
                            gesture.Proof(time);
                            break;

                        case GestureStates.Add:
                            Marker marker = viewManager.AddMarker(gesture);
                            gesture.AddMarker(marker);
                            gesture.SetState(GestureStates.PreReady);
                            break;

                        case GestureStates.PreReady:
                            gesture.Proof(time);
                            break;

                        case GestureStates.Ready:
                            gesture.SetReady();
                            gesture.Proof(time);
                            break;

                        case GestureStates.Expired:
                            gesture.SetState(GestureStates.NotHit);
                            ExpireMarker(gesture);
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
