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
            var models = Model.All<UnityKeyValue>();
            
            foreach(var model in models)
            {
                UE.Debug.Log(model);
            }
        }
    }
}