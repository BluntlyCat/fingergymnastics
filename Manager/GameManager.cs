namespace HSA.FingerGymnastics.Manager
{
    using DB.Models;
    using System.Linq;
    using UnityEngine;

    [RequireComponent(typeof(SettingsManager))]
    [RequireComponent(typeof(SceneManager))]
    public class GameManager : MonoBehaviour
    {
        private static string selectedExercise;
        private static bool exerciseIsActive;
        private static bool hasKinectUser;

        void Start()
        {
        }

        public static string SelectedExercise
        {
            get
            {
                if (selectedExercise == null)
                    return Model.All<Song>().First().Value.UnityObjectName;

                return selectedExercise;
            }

            set
            {
                selectedExercise = value;
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

        public static bool HasKinectUser
        {
            get
            {
                return hasKinectUser;
            }

            set
            {
                hasKinectUser = value;
            }
        }
    }
}
