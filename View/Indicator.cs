namespace HSA.FingerGymnastics.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using UnityEngine;
    using Calc;
    using Controller;

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

            calc = new Calc(viewManager.maxIndicatorPosition, viewManager.minIndicatorPosition, viewManager.indicatorVelocityMS);

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
