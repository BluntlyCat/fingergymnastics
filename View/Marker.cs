namespace HSA.FingerGymnastics.View
{
    using Exercises;
    using Mhaze.Unity.Logging;
    using System;
    using Calc;
    using UnityEngine;

    public class Marker : MonoBehaviour
    {
        protected static Logger<Marker> logger = new Logger<Marker>();
        
        public Color inactiveColor;
        public Color leftDirectionColor;
        public Color rightDirectionColor;

        public Sprite fist;
        public Sprite extendedHand;

        public GameObject scoreSpritePrefab;
        public GameObject handSpritePrefab;
        public GameObject countDownPrefab;

        private ViewManager viewManager;
        private CountDown countDown;

        private AudioSource sound;
        private AudioClip hitSound;
        private AudioClip expiredSound;

        private Animator scoreAnimator;

        private SpriteRenderer scoreSpriteRenderer;
        private SpriteRenderer handSpriteRenderer;

        private SpriteCollider spriteCollider;

        private Calc calc;

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
            countDown = countDownPrefab.GetComponent<CountDown>();
        }

        public void Init(Gesture gesture, float maxX, float minX, float velocity, int count, int swapRange, ViewManager viewManager, bool left)
        {
            calc = new Calc(maxX, minX, velocity);

            gameObject.SetActive(true);
            gameObject.name = gesture.GestureModel.ToString();

            this.viewManager = viewManager;
            this.SetOrientation(left);
            this.SetMarkerPreReady(GestureStates.PreReady);
            this.SetGestureSprite(gesture.GestureModel.GestureType);

            var offset = this.GetOffset();

            gameObject.transform.localPosition = new Vector3(calc.GetXByTime(gesture.StartPosition + offset), -1 + count % swapRange, 0);
        }

        private float GetOffset()
        {
            var pixelPerMS = GetSpeed();
            var width = handSpriteRenderer.bounds.extents.x;

            return width / pixelPerMS;
        }

        private float GetSpeed()
        {
            float x0 = calc.GetXByTime(0);
            float x1 = calc.GetXByTime(1);

            return x1 - x0;
        }
        
        private void SetColor(SpriteRenderer renderer, GestureStates state)
        {
            switch(state)
            {
                case GestureStates.PreReady:
                    renderer.color = this.inactiveColor;
                    break;

                case GestureStates.Active:
                    if (isLeft)
                        renderer.color = this.leftDirectionColor;
                    else
                        renderer.color = this.rightDirectionColor;
                    break;
            }
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
            this.handSpriteRenderer.flipX = !left;
        }

        public void SetMarkerPreReady(GestureStates state)
        {
            SetColor(handSpriteRenderer, state);
            SetAlpha(handSpriteRenderer, .25f);
        }

        public void SetMarkerActive(DateTime time, Gesture gesture, GestureStates state)
        {
            SetColor(handSpriteRenderer, state);
            SetAlpha(handSpriteRenderer, 1f);
            spriteCollider.SetGesture(gesture);
            handSpritePrefab.GetComponent<BoxCollider>().enabled = true;
            countDown.SetCountDown(time, gesture);
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

        public CountDown CountDown
        {
            get
            {
                return countDown;
            }
        }
    }
}
