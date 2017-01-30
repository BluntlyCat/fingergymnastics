namespace HSA.FingerGymnastics.Manager
{
    using DB.Models;
    using Exercises;
    using Mhaze.Unity.DB.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DifficultySettingsManager : MonoBehaviour
    {
        private static UnityKeyValue activeDifficulty;
        private static IDictionary<object, Translation> translations;

        private static SceneManager sceneManager;
        private static IList<Difficulties> difficulties;

        void Awake()
        {
            var settingsManager = this.GetComponent<SettingsManager>();
            var difficultiesKeyValue = settingsManager.GetKeyValue("fgeditor", "difficulties");

            activeDifficulty = settingsManager.GetKeyValue("fgeditor", "difficulty");
            sceneManager = this.GetComponent<SceneManager>();
            difficulties = new List<Difficulties>();

            foreach (var difficulty in difficultiesKeyValue.GetValue<IList>())
            {
                difficulties.Add((Difficulties)Enum.Parse(typeof(Difficulties), difficulty.ToString()));
            }

            translations = Model.All<Translation>(LanguageSettingManager.GetActiveLanguage());
        }

        public Difficulties ActiveDifficulty
        {
            get
            {
                if (activeDifficulty == null)
                    return Difficulties.easy;

                return (Difficulties)Enum.Parse(typeof(Difficulties), activeDifficulty.GetValue<string>());
            }
        }

        public void SwitchDifficulty(GameObject button)
        {
            int index = 0;

            foreach (var difficulty in difficulties)
            {
                index++;

                if (difficulty == ActiveDifficulty)
                    break;
            }

            var newDifficulty = difficulties[index % difficulties.Count].ToString();
            activeDifficulty.SetValue<string>(newDifficulty);

            LastSelected.SetLastItem(button.name);
            sceneManager.ReloadScene();
        }
        
        public string GetTranslation(Difficulties difficulty)
        {
            var key = difficulty.ToString();

            if (translations.ContainsKey(key))
                return translations[key].Text;

            return key;
        }
    }
}
