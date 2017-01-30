namespace HSA.FingerGymnastics.View
{
    using Calc;
    using Controller;
    using UnityEngine;

    public class Indicator : MonoBehaviour
    {
        public GameObject exerciseControllerPrefab;
        public GameObject viewManagerPrefab;

        private IExerciseController exerciseController;
        private ViewManager viewManager;
        
        private Calc calc;

        private void Awake()
        {
            exerciseController = exerciseControllerPrefab.GetComponent<ExerciseController>();
            viewManager = viewManagerPrefab.GetComponent<ViewManager>();

            calc = new Calc(viewManager.maxIndicatorPosition, viewManager.minIndicatorPosition, exerciseController.IndicatorVelocityMs);

            SetIndicatorXPosition(calc.X);
        }

        private void Update()
        {
            float tms = exerciseController.CurrentTime * 1000;

            SetIndicatorXPosition(calc.GetXByTime(tms));
        }

        private void SetIndicatorXPosition(float x)
        {
            transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
        }

        public bool IsLeft
        {
            get
            {
                return calc.IsLeft;
            }
        }
    }
}
