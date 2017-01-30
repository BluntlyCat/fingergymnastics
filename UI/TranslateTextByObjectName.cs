namespace HSA.FingerGymnastics.UI
{
    using Manager;
    using UnityEngine;
    using UnityEngine.UI;

    public class TranslateTextByObjectName : MonoBehaviour
    {
        public GameObject gameManager;

        private LanguageSettingManager languageManager;

        private void Start()
        {
            languageManager = gameManager.GetComponent<LanguageSettingManager>();

            try
            {
                GetComponent<Text>().text = languageManager.GetTranslation(this.name);
            }

            catch
            {
                ;
            }
        }
    }
}
