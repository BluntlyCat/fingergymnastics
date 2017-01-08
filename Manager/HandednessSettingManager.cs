namespace HSA.FingerGymnastics.Manager
{
    using Mhaze.Unity.DB.Models;
    using UnityEngine;

    public class HandednessSettingManager : MonoBehaviour
    {
        private static UnityKeyValue rightHanded;

        private static SceneManager sceneManager;
        private static bool initialized;

        void Awake()
        {
            var settingsManager = this.GetComponent<SettingsManager>();

            rightHanded = settingsManager.GetKeyValue("fgeditor", "rightHanded");
            sceneManager = this.GetComponent<SceneManager>();
        }

        public bool RightHanded
        {
            get
            {
                return rightHanded.GetValue<bool>();
            }
        }

        public void SwitchHandedness()
        {
            rightHanded.SetValue(!rightHanded.GetValue<bool>());
            sceneManager.ReloadScene();
        }
    }
}
