namespace HSA.FingerGymnastics.Manager
{
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
            this.LoadNewScene("Exercise");
        }

        public void ReloadScene()
        {
            this.LoadNewScene(sceneName);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void LoadScene(string sceneName)
        {
            this.LoadNewScene(sceneName);
        }

        public void LoadScene(GameObject gameObject)
        {
            this.LoadNewScene(gameObject.name);
        }
    }
}
