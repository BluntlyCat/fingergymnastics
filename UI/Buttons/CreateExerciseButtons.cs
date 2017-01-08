namespace HSA.FingerGymnastics.UI.Buttons
{
    using DB.Models;
    using Mhaze.Unity.DB.Models;
    using UnityEngine;
    using UnityEngine.UI;

    public class CreateExerciseButtons : MonoBehaviour
    {
        public GameObject buttonPrefab;

        void Start()
        {
            var songs = Model.All<Song>();

            foreach(var song in songs.Values)
            {
                var button = GameObject.Instantiate(buttonPrefab);
                button.name = song.UnityObjectName;
                button.GetComponentInChildren<Text>().text = song.Title;
                button.transform.SetParent(this.transform);
            }
        }
    }
}
