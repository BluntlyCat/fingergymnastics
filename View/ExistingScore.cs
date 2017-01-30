namespace HSA.FingerGymnastics.View
{
    using DB.Models;
    using UnityEngine;
    using UnityEngine.UI;

    public class ExistingScore : MonoBehaviour
    {
        public GameObject songPrefab;
        public GameObject scorePrefab;
        public GameObject playerPrefab;

        private Text song;
        private Text score;
        private Text player;

        private void Awake()
        {
            song = songPrefab.GetComponent<Text>();
            score = scorePrefab.GetComponent<Text>();
            player = playerPrefab.GetComponent<Text>();
        }

        public void SetTexts(HighScore highscore)
        {
            song.text = highscore.Song;
            score.text = highscore.Score.ToString();
            player.text = highscore.Player;
        }
    }
}
