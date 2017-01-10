namespace HSA.FingerGymnastics.View
{
    using Exercises;
    using Leap;
    using Mhaze.Unity.Logging;
    using UnityEngine;
    
    public class Marker : MonoBehaviour
    {
        protected static Logger<Marker> logger = new Logger<Marker>();
        
        public Color leftDirectionColor;
        public Color rightDirectionColor;

        public Sprite fist;
        public Sprite extendedHand;

        public GameObject scoreSpritePrefab;
        public GameObject handSpritePrefab;

        private ViewManager viewManager;

        private AudioSource sound;
        private AudioClip hitSound;
        private AudioClip expiredSound;

        private Animator scoreAnimator;

        private SpriteRenderer scoreSpriteRenderer;
        private SpriteRenderer handSpriteRenderer;

        private SpriteCollider spriteCollider;

        private bool isLeft;
        private bool isActive = true;

        void Awake()
        {
            logger.AddLogAppender<ConsoleAppender>();

            sound = this.GetComponent<AudioSource>();
            hitSound = Resources.Load<AudioClip>("Sounds/hit");
            expiredSound = Resources.Load<AudioClip>("Sounds/missed");
            scoreSpriteRenderer = scoreSpritePrefab.GetComponent<SpriteRenderer>();
            handSpriteRenderer = handSpritePrefab.GetComponent<SpriteRenderer>();
            scoreAnimator = scoreSpritePrefab.GetComponent<Animator>();
            spriteCollider = GetComponentInChildren<SpriteCollider>();
        }
        
        private void SetColor(SpriteRenderer renderer)
        {
            if (isLeft)
                renderer.color = this.leftDirectionColor;
            else
                renderer.color = this.rightDirectionColor;
        }

        private void SetAlpha(SpriteRenderer renderer, float alpha)
        {
            renderer.color = new Color(handSpriteRenderer.color.r, handSpriteRenderer.color.g, handSpriteRenderer.color.b, alpha);
        }

        public void SetGestureSprite(Gestures gesture)
        {
            switch (gesture)
            {
                case Gestures.ExtendedHand:
                    handSpriteRenderer.sprite = extendedHand;
                    break;

                case Gestures.Fist:
                    handSpriteRenderer.sprite = fist;
                    break;
            }
        }

        public void SetOrientation(bool left)
        {
            this.isLeft = left;
            this.handSpriteRenderer.flipX = left;
        }

        public void SetMarkerPreReady()
        {
            SetColor(handSpriteRenderer);
            SetColor(scoreSpriteRenderer);
            SetAlpha(handSpriteRenderer, .5f);
        }

        public void SetMarkerReady()
        {
            SetAlpha(handSpriteRenderer, 1f);
            handSpritePrefab.GetComponent<BoxCollider>().enabled = true;
        }
        
        public int DisableMarker(int score, int maxScore, GestureStates state)
        {
            if (isActive)
            {
                isActive = false;
                this.handSpritePrefab.SetActive(false);

                switch(state)
                {
                    case GestureStates.NotHit:
                        sound.clip = expiredSound;
                        this.scoreSpritePrefab.SetActive(false);
                        break;

                    case GestureStates.Hit:
                        score++;
                        sound.clip = hitSound;
                        viewManager.SetScoreText(score, maxScore);
                        scoreSpritePrefab.SetActive(true);
                        scoreAnimator.SetBool("Score", true);
                        break;
                }

                sound.Play();
            }

            return score;
        }

        public ViewManager ViewManager
        {
            get
            {
                return this.viewManager;
            }

            set
            {
                this.viewManager = value;
            }
        }

        public bool IsLeft
        {
            get
            {
                return isLeft;
            }
        }

        public SpriteCollider Collider
        {
            get
            {
                return spriteCollider;
            }
        }
    }
}
