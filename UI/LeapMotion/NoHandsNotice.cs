namespace HSA.FingerGymnastics.UI.LeapMotion
{
    using FingerGymnastics.Controller;
    using Game;
    using UnityEngine;

    public class NoHandsNotice : MonoBehaviour
    {

        /** The speed to fade the object alpha from 0 to 1. */
        public float fadeInTime = 1.0f;
        /** The speed to fade the object alpha from 1 to 0. */
        public float fadeOutTime = 1.0f;
        /** The easing curve. */
        public AnimationCurve fade;
        /** A delay before beginning the fade-in effect. */
        public int waitFrames = 10;
        /** An alternative image to use when the hardware is embedded in a keyboard or laptop. */
        public Texture2D embeddedReplacementImage;
        /** The fully on texture tint color. */
        public Color onColor = Color.white;

        public GameObject handControllerPrefab;

        private float fadedIn = 0.0f;
        private int frames_disconnected_ = 0;
        private LeapHandController controller;

        void Start()
        {
            SetAlpha(0.0f);
            controller = handControllerPrefab.GetComponent<LeapHandController>();
        }

        void SetAlpha(float alpha)
        {
            GetComponent<GUITexture>().color = Color.Lerp(Color.clear, onColor, alpha);
        }

        void Update()
        {
            if (controller.HasOneHand || GameState.Debug)
                frames_disconnected_ = 0;
            else
                frames_disconnected_++;

            if (frames_disconnected_ < waitFrames)
                fadedIn -= Time.deltaTime / fadeOutTime;
            else
                fadedIn += Time.deltaTime / fadeInTime;

            fadedIn = Mathf.Clamp(fadedIn, 0.0f, 1.0f);
            SetAlpha(fade.Evaluate(fadedIn));
        }
    }
}