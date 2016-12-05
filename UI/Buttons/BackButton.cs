namespace HSA.FingerGymnastics.UI.Buttons
{
    using Manager;
    using UnityEngine.UI;

    public class BackButton : TranslatedUI
    {
        // Use this for initialization
        void Start()
        {
            var languageManager = gameManager.GetComponent<LanguageSettingManager>();
            this.GetComponentInChildren<Text>().text = languageManager.GetTranslation("back");
        }
    }
}