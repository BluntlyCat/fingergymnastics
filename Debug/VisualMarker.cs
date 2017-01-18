namespace Assets.Scripts.Debug
{
    using HSA.FingerGymnastics.Controller;
    using HSA.FingerGymnastics.DB.Models;
    using Mhaze.Unity.DB.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using UnityEngine.UI;

    public class VisualMarker : MonoBehaviour
    {
        public GameObject markerPrefab;
        public GameObject exerciseControllerPrefab;

        public Color colorOdd;
        public Color colorEven;

        private ExerciseController exerciseController;
        private IDictionary<Gesture, GameObject> gestures = new Dictionary<Gesture, GameObject>();

        private void Start()
        {
            var songs = Model.All<Song>();
            var track = songs.Values.First().Tracks[1];
            var gestures = track.Gestures.Values.OrderBy(g => g.StartTime).ToArray();
            var width = GetComponent<RectTransform>().rect.width;
            var height = GetComponent<RectTransform>().rect.height / gestures.Length;

            float y = -height / 2;
            int count = 1;

            exerciseController = exerciseControllerPrefab.GetComponent<ExerciseController>();

            foreach (var g in gestures)
            {
                var marker = GameObject.Instantiate(markerPrefab, this.transform, false);
                var rectTransform = marker.GetComponent<RectTransform>();

                float x = (float)g.EndTime.TimeOfDay.TotalSeconds;

                this.gestures.Add(g, marker);
                rectTransform.sizeDelta = new Vector2(x, height);
                rectTransform.anchoredPosition = new Vector3((float)g.StartTime.TimeOfDay.TotalSeconds, y, 0);

                marker.GetComponent<Image>().color = count % 2 == 0 ? colorEven : colorOdd;
                marker.SetActive(true);
                y -= height;
                count++;
            }
        }

        private void Update()
        {
            foreach (var g in gestures)
            {
                var image = g.Value.GetComponent<Image>();



                if (exerciseController.CurrentTime >= g.Key.StartTime.TimeOfDay.TotalSeconds && exerciseController.CurrentTime < g.Key.EndTime.TimeOfDay.TotalSeconds)
                {

                }
            }
        }
    }
}
