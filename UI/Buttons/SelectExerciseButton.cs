namespace HSA.FingerGymnastics.UI.Buttons
{
    using DB.Models;
    using Game;
    using Manager;
    using Mhaze.Unity.DB.Models;
    using UnityEngine;

    public class SelectExerciseButton : MonoBehaviour
    {
        public GameObject gameManagerPrefab;

        private SceneManager sceneManager;

        void Start()
        {
            this.sceneManager = gameManagerPrefab.GetComponent<SceneManager>();
        }

        public void LoadExercise()
        {
            GameState.SelectedSong = Model.GetModel<Song>(this.name);
            sceneManager.LoadExercise();
        }
    }
}
