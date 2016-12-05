namespace HSA.FingerGymnastics.UI.Buttons
{
    using Manager;
    using UnityEngine.UI;

    public class LanguageButton : TranslatedUI
    {
        // Use this for initialization
        void Start()
        {
            var languageManager = gameManager.GetComponent<LanguageSettingManager>();

            var language = languageManager.GetTranslation("language");
            var activeLanguage = languageManager.GetTranslation(languageManager.ActiveLanguage);

            this.GetComponentInChildren<Text>().text = string.Format("{0}: {1}", language, activeLanguage);
        }
    }
}