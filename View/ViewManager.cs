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
        public GameObject indicatorPrefab;
        public GameObject markerPrefab;

        public float indicatorVelocity = .1f;
        public float minIndicatorPosition = -4f;
        public float maxIndicatorPosition = 4f;

        private float indicatorPosition;

        private Image progressBarImage;
        private Text scoreObject;

        private int markerCount = 0;

        void Start()
        {
            logger.AddLogAppender<ConsoleAppender>();

            this.progressBarImage = progressBarPrefab.GetComponent<Image>();
            this.scoreObject = scorePrefab.GetComponent<Text>();

            indicatorPosition = minIndicatorPosition;

            SetIndicatorXPosition(indicatorPosition);
        }
        
        public Marker AddMarker(Gesture gesture)
        {
            var markerModel = GameObject.Instantiate(markerPrefab, gesturePlanePrefab.transform, false);
            var marker = markerModel.GetComponentInChildren<Marker>();

            markerModel.SetActive(true);
            markerModel.name = gesture.GestureModel.ToString();
            markerModel.transform.localPosition = new Vector3(indicatorPrefab.transform.position.x, .5f, 0 + markerCount % 4);

            marker.SetOrientation(indicatorVelocity < 0);
            marker.SetMarkerPreReady();
            marker.SetGestureSprite(gesture.GestureModel.GestureType);

            markerCount++;

            return marker;
        }

        public void DisableMarker(Marker marker)
        {
            marker.gameObject.SetActive(false);
        }

        private void SetIndicatorXPosition(float x)
        {
            indicatorPrefab.transform.localPosition = new Vector3(x, indicatorPrefab.transform.localPosition.y, indicatorPrefab.transform.localPosition.z);
        }
        
        public void UpdateIndicator(float songLength, float currentTime)
        {
            if (indicatorPosition > maxIndicatorPosition)
                indicatorVelocity *= -1;
            else if (indicatorPosition < minIndicatorPosition)
                indicatorVelocity *= -1;

            SetIndicatorXPosition(indicatorPosition += indicatorVelocity);

            this.progressBarImage.fillAmount = (1 / songLength) * currentTime;
        }

        public void SetScoreText(int score, int maxScore)
        {
            this.scoreObject.text = string.Format("{0} / {1}", score, maxScore);
        }
    }
}
