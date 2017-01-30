namespace HSA.FingerGymnastics.View
{
    using DB.Models;
    using Game;
    using Mhaze.Unity.DB.Models;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class ScoreView : MonoBehaviour
    {
        public GameObject newScorePrefab;
        public GameObject scorePrefab;
        public GameObject eventSystemPrefab;

        private EventSystem eventSystem;
        private KeyValuePair<object, HighScore>[] existingScores;
        private HighScore currentScore;

        private void Start()
        {
            var scores = Model.All<HighScore>();
            var lastIdTmp = Model.GetLastId<HighScore>();

            long lastId = 1;

            if (lastIdTmp != null)
                lastId = (long)lastIdTmp + 1;

            eventSystem = eventSystemPrefab.GetComponent<EventSystem>();
            currentScore = new HighScore(lastId);
            currentScore.Song = GameState.SelectedSong.Title;
            currentScore.Score = GameState.Score;

            scores.Add(currentScore.ID, currentScore);
            existingScores = scores.OrderBy(s => s.Value.Score).ToArray();

            foreach (var kv in existingScores)
            {
                var score = kv.Value;

                if(score.ID == currentScore.ID)
                {
                    var scoreObject = Instantiate(newScorePrefab, this.gameObject.transform, false);
                    var scoreScript = scoreObject.GetComponent<NewScore>();

                    eventSystem.firstSelectedGameObject = scoreScript.InputField.gameObject;
                    scoreScript.SetHighScore(score);
                }
                else
                {
                    var scoreObject = Instantiate(scorePrefab, this.gameObject.transform, false);
                    var scoreScript = scoreObject.GetComponent<ExistingScore>();

                    scoreScript.SetTexts(score);
                }
            }
        }
    }
}
