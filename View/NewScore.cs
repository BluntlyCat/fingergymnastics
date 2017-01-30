namespace HSA.FingerGymnastics.View
{
    using DB.Models;
    using Game;
    using UnityEngine;
    using UnityEngine.UI;

    public class NewScore : MonoBehaviour
    {
        public GameObject songPrefab;
        public GameObject scorePrefab;
        public GameObject playerPrefab;

        private Text song;
        private Text score;
        private InputField player;

        private HighScore highScore;

        private void Awake()
        {
            song = songPrefab.GetComponent<Text>();
            score = scorePrefab.GetComponent<Text>();
            player = playerPrefab.GetComponent<InputField>();

            song.text = GameState.SelectedSong.Title;
            score.text = GameState.Score.ToString();
        }

        private void Update()
        {
            if(highScore != null && player.text != "" && Input.GetKeyDown(KeyCode.Return))
            {
                highScore.Player = player.text;
                highScore.Save();
                player.readOnly = true;
            }
        }

        public void SetHighScore(HighScore highScore)
        {
            this.highScore = highScore;
        }

        public InputField InputField
        {
            get
            {
                return player;
            }
        }
    }
}
