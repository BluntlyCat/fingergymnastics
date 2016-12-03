namespace HSA.FingerGymnastics.DB
{
    using System.Linq;
    using UE = UnityEngine;
    using Models;
    using UnityEngine;
    using System.Collections.Generic;
    public class Modeltest : UE.MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            var model = Model.All<Song>();
            var source = this.GetComponent<AudioSource>();
            source.clip = model.First<KeyValuePair<object, Song>>().Value.File;
            source.Play();
        }
    }
}