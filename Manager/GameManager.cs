namespace HSA.FingerGymnastics.Manager
{
    using UnityEngine;

    [RequireComponent(typeof(SettingsManager))]
    [RequireComponent(typeof(SceneManager))]
    public class GameManager : MonoBehaviour
    {
        private static bool exerciseIsActive;
        private static bool hasKinectUser;

        void Start()
        {
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
