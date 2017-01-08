namespace HSA.FingerGymnastics.Exercises
{
    using Leap;
    using System;
    using UnityEngine;
    using View;
    using DB = DB.Models;

    public class GestureController
    {
        private Gesture head;
        private Gesture current;
        private Gesture tail;

        private ViewManager viewManager;

        private int maxScore = 0;
        private int score = 0;

        public GestureController(ViewManager viewManager, int maxScore)
        {
            this.viewManager = viewManager;
            this.maxScore = maxScore;
        }

        public void AddGesture(DB.Gesture gesture, double timeOffset)
        {
            Gesture newGesture = null;

            switch (gesture.GestureType)
            {
                case Gestures.ExtendedHand:
                    newGesture = new ExtendedHand(tail, gesture, this, timeOffset);
                    break;

                case Gestures.Fist:
                    newGesture = new Fist(tail, gesture, this, timeOffset);
                    break;
            }

            if (head == null)
                head = current = newGesture;

            if (tail != null)
                tail.Next = newGesture;

            tail = newGesture;
        }

        public void RemoveGesture(Gesture gesture)
        {
            gesture.Previous.Next = gesture.Next;
            gesture.Next.Previous = gesture.Previous;

            gesture = null;
        }

        public void ExpireMarker(Gesture gesture)
        {
            PlaySound(gesture.Marker);
            viewManager.SetScoreText(++score, maxScore);

            if (gesture.Marker.isActiveAndEnabled)
                viewManager.DisableMarker(gesture.Marker);
        }

        public void Proof(float time)
        {
            try
            {
                for (Gesture current = head; current != null; current = current.Next)
                {
                    switch (current.State)
                    {
                        case GestureStates.NotAdded:
                            current.Proof(time);
                            break;

                        case GestureStates.Add:
                            current.AddMarker(viewManager.AddMarker(current));
                            current.SetPreReady();
                            break;

                        case GestureStates.PreReady:
                            current.Proof(time);
                            break;

                        case GestureStates.Ready:
                            current.SetReady();
                            current.Proof(time);
                            break;

                        case GestureStates.Expired:
                            if (current.Marker.NotHit)
                                PlaySound(current.Marker);

                            viewManager.DisableMarker(current.Marker);
                            current.SetRemoved();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool SetNext()
        {
            current = current.Next;

            return current != null;
        }

        public void PlaySound(Marker marker)
        {
            marker.PlaySound();
        }

        public Gesture First
        {
            get
            {
                return head;
            }
        }

        public Gesture Last
        {
            get
            {
                return tail;
            }
        }

        public Gesture Current
        {
            get
            {
                return current;
            }
        }
    }
}
