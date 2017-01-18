namespace HSA.FingerGymnastics.View
{
    using Exercises;
    using Mhaze.Unity.Logging;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    public delegate void CountDownEventHandler(Gesture gesture);

    public class CountDown : MonoBehaviour
    {
        protected static Logger<CountDown> logger = new Logger<CountDown>();

        public event CountDownEventHandler OnTimeOut;

        private Image countDownImage;
        private DateTime endTime;
        private Gesture gesture;
        private bool timeSet;

        private float totalMilliseconds;

        private void Start()
        {
            logger.AddLogAppender<ConsoleAppender>();
            countDownImage = GetComponent<Image>();
        }

        public void SetCountDown(DateTime time, Gesture gesture)
        {
            endTime = time.Add(DateTime.Now.TimeOfDay);
            totalMilliseconds = (float)time.TimeOfDay.TotalMilliseconds;
            this.gesture = gesture;

            timeSet = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (OnTimeOut != null && timeSet)
            {
                double millisecondsLeft = endTime.TimeOfDay.TotalMilliseconds - DateTime.Now.TimeOfDay.TotalMilliseconds;
                countDownImage.fillAmount = (float)millisecondsLeft / totalMilliseconds;

                if (millisecondsLeft <= 0)
                    OnTimeOut(gesture);
            }
        }
    }
}