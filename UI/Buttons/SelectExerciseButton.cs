namespace HSA.FingerGymnastics.UI.Buttons
{
    using Game;
    using Manager;
    using UnityEngine;

    public class SelectExerciseButton : MonoBehaviour
    {
        public GameObject gameManagerPrefab;

        private GameState gameState;
        private SceneManager sceneManager;

        void Start()
        {
            this.gameState = gameManagerPrefab.GetComponent<GameState>();
            this.sceneManager = gameManagerPrefab.GetComponent<SceneManager>();
        }

        public void LoadExercise()
        {
            GameState.SelectedExercise = this.name;
            sceneManager.LoadExercise();
        }
    }
}
