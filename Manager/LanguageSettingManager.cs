﻿namespace HSA.FingerGymnastics.Manager
{
    using DB.Models;
    using Mhaze.Unity.DB.Models;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class LanguageSettingManager : MonoBehaviour
    {
        private static UnityKeyValue activeLanguange;
        private static IDictionary<object, Translation> translations;

        private static SceneManager sceneManager;
        private static IList languages;

        void Awake()
        {
            var settingsManager = this.GetComponent<SettingsManager>();
            var languageKeyValue = settingsManager.GetKeyValue("fgeditor", "languages");

            activeLanguange = settingsManager.GetKeyValue("fgeditor", "language");
            sceneManager = this.GetComponent<SceneManager>();
            languages = languageKeyValue.GetValue<IList>();
            translations = Model.All<Translation>(activeLanguange.GetValue<string>());
        }

        public string ActiveLanguage
        {
            get
            {
                return activeLanguange.GetValue<string>();
            }
        }

        public void SwitchLanguage(GameObject button)
        {
            int index = 0;
            
            foreach (var language in languages)
            {
                index++;

                if (language.ToString() == activeLanguange.GetValue<string>())
                    break;
            }

            var newLanguage = languages[index % languages.Count].ToString();
            activeLanguange.SetValue<string>(newLanguage);
            LastSelected.SetLastItem(button.name);
            sceneManager.ReloadScene();
        }

        public static string GetActiveLanguage()
        {
            if (activeLanguange == null)
                return "en_gb";

            return activeLanguange.GetValue<string>();
        }

        public string GetTranslation(string key)
        {
            if (translations.ContainsKey(key))
                return translations[key].Text;

            return key;
        }
    }
}
