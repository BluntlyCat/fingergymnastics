﻿namespace HSA.FingerGymnastics.Game
{
    using DB.Models;
    using Manager;
    using Mhaze.Unity.DB.Models;
    using System.Linq;
    using UnityEngine;

    [RequireComponent(typeof(SettingsManager))]
    [RequireComponent(typeof(SceneManager))]
    public class GameState : MonoBehaviour
    {
        private static Song selectedSong;
        private static bool exerciseIsActive;
        private static int score;
        private static int maxScore;
        
#if UNITY_EDITOR
        private static bool debug = true;
#else
        private static bool debug = false;
#endif
        public static Song SelectedSong
        {
            get
            {
                if (selectedSong == null)
                    return Model.All<Song>().First().Value;

                return selectedSong;
            }

            set
            {
                selectedSong = value;
            }
        }

        public static bool ExerciseIsActive
        {
            get
            {
                return exerciseIsActive;
            }

            set
            {
                exerciseIsActive = value;
            }
        }

        public static int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        public static int MaxScore
        {
            get
            {
                return maxScore;
            }

            set
            {
                maxScore = value;
            }
        }

        public static bool Debug
        {
            get
            {
                return debug;
            }
        }
    }
}
