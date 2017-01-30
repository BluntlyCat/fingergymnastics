namespace HSA.FingerGymnastics.UI
{
    using DB.Models;
    using Game;
    using Mhaze.Unity.DB.Models;
    using UnityEngine;
    using UnityEngine.EventSystems;

    public class MainMenu : MonoBehaviour
    {
        public GameObject eventSystemPrefab;
        public GameObject firstEntry;

        private EventSystem eventSystem;

        private void Start()
        {
            var lastItem = LastSelected.GetLastItem();

            eventSystem = eventSystemPrefab.GetComponent<EventSystem>();

            if(lastItem.Name == "")
                eventSystem.firstSelectedGameObject = firstEntry;
            else
            {
                eventSystem.firstSelectedGameObject = GameObject.Find(lastItem.Name);
            }
        }
    }
}