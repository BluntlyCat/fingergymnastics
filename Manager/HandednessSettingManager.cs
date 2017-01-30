namespace HSA.FingerGymnastics.Manager
{
    using DB.Models;
    using Mhaze.Unity.DB.Models;
    using UnityEngine;

    public class HandednessSettingManager : MonoBehaviour
    {
        private static UnityKeyValue rightHanded;

        private static SceneManager sceneManager;

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

        public void SwitchHandedness(GameObject button)
        {
            rightHanded.SetValue(!rightHanded.GetValue<bool>());
            LastSelected.SetLastItem(button.name);
            sceneManager.ReloadScene();
        }
    }
}
