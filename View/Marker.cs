namespace HSA.FingerGymnastics.View
{
    using Exercises;
    using Leap;
    using Mhaze.Unity.Logging;
    using UnityEngine;

    public delegate void CollisionEventHandler(Marker marker, Hand hand);

    public class Marker : MonoBehaviour
    {
        protected static Logger<Marker> logger = new Logger<Marker>();

        public event CollisionEventHandler OnMarkerCollision;

        public Color leftDirection;
        public Color rightDirection;

        public Sprite fist;
        public Sprite extendedHand;

        private AudioSource sound;
        private AudioClip hitSound;
        private AudioClip expiredSound;

        private SpriteRenderer spriteRenderer;

        private bool removed = false;
        private bool notHit = true;

        private double startTime = 0;
        private double endTime = 0;

        private bool isLeft;

        void Awake()
        {
            logger.AddLogAppender<ConsoleAppender>();

            sound = this.GetComponentInParent<AudioSource>();
            hitSound = Resources.Load<AudioClip>("Sounds/hit");
            expiredSound = Resources.Load<AudioClip>("Sounds/missed");
            spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        void OnCollisionEnter(Collision collision)
        {
            var parent = GetParent(collision.collider.transform);

            if (parent == null)
                return;

            Hand hand = parent.GetComponent<RigidHand>().GetLeapHand();

            if (hand.IsLeft == isLeft && notHit)
            {
                notHit = false;

                if (OnMarkerCollision != null)
                    OnMarkerCollision(this, parent.GetComponent<RigidHand>().GetLeapHand());
            }
        }

        private Transform GetParent(Transform gameObject)
        {
            if (gameObject.name != "RigidHand(Clone)" && gameObject.transform.parent != null)
                return GetParent(gameObject.transform.parent);

            else if (gameObject.name == "RigidHand(Clone)")
                return gameObject;

            return null;
        }

        private void SetColor()
        {
            if (isLeft)
                spriteRenderer.color = this.leftDirection;
            else
                spriteRenderer.color = this.rightDirection;

            SetAlpha(.5f);
        }

        private void SetAlpha(float alpha)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
        }

        public void SetGestureSprite(Gestures gesture)
        {
            switch (gesture)
            {
                case Gestures.ExtendedHand:
                    spriteRenderer.sprite = extendedHand;
                    break;

                case Gestures.Fist:
                    spriteRenderer.sprite = fist;
                    break;
            }
        }

        public void SetOrientation(bool left)
        {
            this.isLeft = left;
            this.spriteRenderer.flipX = left;
        }

        public void SetMarkerPreReady()
        {
            SetColor();
            SetAlpha(.5f);
        }

        public void SetMarkerReady()
        {
            SetAlpha(1f);
            GetComponent<MeshCollider>().enabled = true;
        }
        
        public void PlaySound()
        {
            if (notHit)
                sound.clip = expiredSound;
            else
                sound.clip = hitSound;

            sound.Play();
        }

        public bool Removed
        {
            get
            {
                return removed;
            }

            set
            {
                removed = value;
            }
        }

        public bool NotHit
        {
            get
            {
                return notHit;
            }
        }

        public double StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        public double EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
            }
        }

        public bool IsLeft
        {
            get
            {
                return this.isLeft;
            }
        }
    }
}
