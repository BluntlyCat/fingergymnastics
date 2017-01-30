namespace HSA.FingerGymnastics.UI.Buttons
{
    using Manager;
    using UnityEngine.UI;

    public class DifficultyButton : TranslatedUI
    {
        // Use this for initialization
        void Start()
        {
            var difficultyManager = gameManager.GetComponent<DifficultySettingsManager>();
            var languageManager = gameManager.GetComponent<LanguageSettingManager>();

            var difficulty = languageManager.GetTranslation("difficulty");
            var activeDifficulty = languageManager.GetTranslation(difficultyManager.ActiveDifficulty.ToString());

            this.GetComponentInChildren<Text>().text = string.Format("{0}: {1}", difficulty, activeDifficulty);
        }
    }
}