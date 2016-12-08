namespace HSA.FingerGymnastics.UI.Buttons
{
    using Manager;
    using UnityEngine;

    public class SelectExerciseButton : MonoBehaviour
    {
        public GameObject gameManagerPrefab;

        private GameManager gameManager;
        private SceneManager sceneManager;

        void Start()
        {
            this.gameManager = gameManagerPrefab.GetComponent<GameManager>();
            this.sceneManager = gameManagerPrefab.GetComponent<SceneManager>();
        }

        public void LoadExercise()
        {
            GameManager.SelectedExercise = this.name;
            sceneManager.LoadExercise();
        }
    }
}
