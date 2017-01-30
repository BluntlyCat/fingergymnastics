namespace HSA.FingerGymnastics.Manager
{
    using DB.Models;
    using Mhaze.Unity.Logging;
    using UnityEngine;
    using USM = UnityEngine.SceneManagement;

    public class SceneManager : MonoBehaviour
    {
        private static Logger<SceneManager> logger = new Logger<SceneManager>();

        private static string sceneName;

        // Use this for initialization
        void Awake()
        {
            logger.AddLogAppender<ConsoleAppender>();
            sceneName = USM.SceneManager.GetActiveScene().name;

            logger.Debug(string.Format("Load scene: {0}", sceneName));
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                this.LoadScene("MainMenu");
            }
        }

        public static string SceneName
        {
            get
            {
                return sceneName;
            }
        }

        private void LoadNewScene(string scene)
        {
            USM.SceneManager.LoadScene(scene);
        }

        public void LoadExercise()
        {
            LastSelected.SetLastItem("");
            this.LoadNewScene("Exercise");
        }

        public void ReloadScene()
        {
            this.LoadNewScene(sceneName);
        }

        public void Quit()
        {
            LastSelected.SetLastItem("");
            Application.Quit();
        }

        public void LoadScene(string sceneName)
        {
            LastSelected.SetLastItem("");
            this.LoadNewScene(sceneName);
        }

        public void LoadScene(GameObject gameObject)
        {
            this.LoadNewScene(gameObject.name);
        }
    }
}
