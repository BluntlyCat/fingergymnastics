namespace HSA.FingerGymnastics.UI.Buttons
{
    using Manager;
    using UnityEngine.UI;

    public class HandednessButton : TranslatedUI
    {
        // Use this for initialization
        void Start()
        {
            var languageManager = gameManager.GetComponent<LanguageSettingManager>();
            var handednessManager = gameManager.GetComponent<HandednessSettingManager>();
            var handedness = languageManager.GetTranslation("rightHanded");

            if(handednessManager.RightHanded == false)
                handedness = languageManager.GetTranslation("leftHanded");

            this.GetComponentInChildren<Text>().text = handedness;
        }
    }
}