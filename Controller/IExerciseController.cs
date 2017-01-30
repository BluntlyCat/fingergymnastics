namespace HSA.FingerGymnastics.Controller
{
    public interface IExerciseController
    {
        float CurrentTime { get; }

        float IndicatorVelocityMs { get; }
    }
}
