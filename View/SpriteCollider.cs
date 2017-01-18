namespace HSA.FingerGymnastics.View
{
    using LM = Leap;
    using Exercises;
    using Mhaze.Unity.Logging;
    using UnityEngine;

    public delegate void CollisionEventHandler(Marker marker, Gesture gesture, LM.Hand hand);

    public class SpriteCollider : MonoBehaviour
    {
        protected static Logger<SpriteCollider> logger = new Logger<SpriteCollider>();

        public event CollisionEventHandler OnMarkerCollision;

        private Gesture gesture;
        private bool canCollide = true;

        private void Start()
        {
            logger.AddLogAppender<ConsoleAppender>();
        }

        void OnCollisionEnter(Collision collision)
        {
            if (canCollide)
            {
                var parent = GetParent("RigidHand(Clone)", collision.collider.transform);

                if (parent == null)
                    return;

                LM.Hand hand = parent.GetComponent<RigidHand>().GetLeapHand();
                Marker marker = this.GetComponentInParent<Marker>();

                logger.Debug("Collision");

                if (hand.IsLeft == marker.IsLeft)
                {
                    logger.Debug("Collision with correct hand");

                    if (OnMarkerCollision != null)
                        OnMarkerCollision(marker, gesture, hand);
                }
            }
        }

        private Transform GetParent(string name, Transform gameObject)
        {
            if (gameObject.name != name && gameObject.transform.parent != null)
                return GetParent(name, gameObject.transform.parent);

            else if (gameObject.name == name)
                return gameObject;

            return null;
        }

        public void SetGesture(Gesture gesture)
        {
            this.gesture = gesture;
        }

        public bool CanCollide
        {
            get
            {
                return canCollide;
            }

            set
            {
                canCollide = value;
            }
        }
    }
}
