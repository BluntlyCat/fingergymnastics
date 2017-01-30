namespace HSA.FingerGymnastics.View
{
    using Exercises;
    using Mhaze.Unity.Logging;
    using UnityEngine;
    using UnityEngine.UI;

    public class ViewManager : MonoBehaviour
    {
        protected static Logger<ViewManager> logger = new Logger<ViewManager>();

        public GameObject gesturePlanePrefab;
        public GameObject progressBarPrefab;
        public GameObject scorePrefab;
        public GameObject markerPrefab;

        public float minIndicatorPosition = -4f;
        public float maxIndicatorPosition = 4f;

        public int markerTopPosition = -1;
        public int markerSwapRange = 4;

        private Image progressBarImage;
        private Text timeText;
        private Text scoreObject;

        private int markerCount = 0;

        void Awake()
        {
            logger.AddLogAppender<ConsoleAppender>();

            this.progressBarImage = progressBarPrefab.GetComponent<Image>();
            this.timeText = progressBarPrefab.GetComponentInChildren<Text>();
            this.scoreObject = scorePrefab.GetComponent<Text>();
        }

        public Marker AddMarker(BaseGesture gesture, float indicatorVelocityMS)
        {
            var markerModel = GameObject.Instantiate(markerPrefab, gesturePlanePrefab.transform, false);
            var marker = markerModel.GetComponentInChildren<Marker>();

            marker.Init(gesture, maxIndicatorPosition, minIndicatorPosition, indicatorVelocityMS, markerCount, markerTopPosition, markerSwapRange, this);

            markerCount++;

            return marker;
        }
        
        public void UpdateIndicator(float songLength, float currentTime)
        {
            this.progressBarImage.fillAmount = (1 / songLength) * currentTime;
            this.timeText.text = string.Format("{0}s", currentTime.ToString("0.00"));
        }

        public void SetScoreText(int score, int maxScore)
        {
            this.scoreObject.text = string.Format("{0} / {1}", score, maxScore);
        }
    }
}
