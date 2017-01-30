namespace HSA.FingerGymnastics.UI.Buttons
{
    using DB.Models;
    using Game;
    using Manager;
    using Mhaze.Unity.DB.Models;
    using UnityEngine.UI;

    public class StartExerciseButton : TranslatedUI
    {
        public string exerciseName;

        private SceneManager sceneManager;

        // Use this for initialization
        void Start()
        {
            var languageManager = gameManager.GetComponent<LanguageSettingManager>();
            this.sceneManager = gameManager.GetComponent<SceneManager>();
            this.GetComponentInChildren<Text>().text = languageManager.GetTranslation("start");
        }

        public void StartExercise()
        {
            if(exerciseName != "")
            {
                GameState.SelectedSong = Model.GetModel<Song>(this.exerciseName);
                sceneManager.LoadExercise();
            }
        }
    }
}
