namespace HSA.FingerGymnastics.Controller
{
    using Exercises;
    using Game;
    using Manager;
    using Mhaze.Unity.DB.Models;
    using System.Linq;
    using UnityEngine;
    using View;
    using DB = DB.Models;

    public class ExerciseController : MonoBehaviour, IExerciseController
    {
        public GameObject gameManagerPrefab;
        public GameObject musicPrefab;
        public GameObject handControllerPrefab;
        public GameObject viewManagerPrefab;

        public double timeOffset = 1d;

        private float indicatorVelocityMS;

        private AudioSource music;

        private SceneManager sceneManager;
        private GameState gameState;
        private LeapHandController controller;
        private ViewManager viewManager;

        private GestureController gestureController;

        private DB.Song song;

        private void InitializeGestures()
        {
            foreach (DB.Gesture gesture in song.Tracks.First().Value.Gestures.Values.OrderBy(g => g.StartTime))
            {
                gestureController.AddGesture(gesture, timeOffset);
            }
        }

        private void Awake()
        {
            gameState = gameManagerPrefab.GetComponent<GameState>();
            controller = handControllerPrefab.GetComponent<LeapHandController>();
            viewManager = viewManagerPrefab.GetComponent<ViewManager>();
            music = musicPrefab.GetComponent<AudioSource>();
            sceneManager = GetComponentInParent<SceneManager>();
            indicatorVelocityMS = (float)GetComponentInParent<DifficultySettingsManager>().ActiveDifficulty;

            song = GameState.SelectedSong;
            GameState.MaxScore = song.Tracks.First().Value.Gestures.Count;
            GameState.Score = 0;
            gestureController = new GestureController(viewManager, indicatorVelocityMS);
            
            music.clip = song.File;
            viewManager.SetScoreText(0, song.Tracks.First().Value.Gestures.Count);

            InitializeGestures();
        }

        private void Update()
        {
            if (controller.IsConnected == false || !(controller.HasOneHand || GameState.Debug))
            {
                PauseExercise();
            }
            else
            {
                if (gestureController.RemovedGestureCount == gestureController.GestureCount && music.isPlaying == false)
                {
                    StopErercise(FinishStates.Finished);
                }

                else if (gestureController.RemovedGestureCount != gestureController.GestureCount && music.isPlaying == false)
                {
                    StartExercise();
                }
            }
        }

        private void FixedUpdate()
        {
            if (music.isPlaying)
            {
                viewManager.UpdateIndicator(SongLength, CurrentTime);
                gestureController.Proof(CurrentTime);
            }
        }

        public void StartExercise()
        {
            music.Play();
        }

        public void PauseExercise()
        {
            music.Pause();
        }

        public void StopErercise(FinishStates state)
        {
            music.Stop();

            switch(state)
            {
                case FinishStates.None:
                    break;

                case FinishStates.Canceled:
                    sceneManager.LoadScene("MainMenu");
                    break;

                case FinishStates.Finished:
                    sceneManager.LoadScene("Finished");
                    break;
            }
        }

        public float CurrentTime
        {
            get
            {
                return music.time;
            }
        }

        public float IndicatorVelocityMs
        {
            get
            {
                return indicatorVelocityMS;
            }
        }

        public float SongLength
        {
            get
            {
                return music.clip.length;
            }
        }

        public DB.Track Track
        {
            get
            {
                return song.Tracks.First().Value;
            }
        }
    }
}
