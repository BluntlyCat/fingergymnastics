namespace HSA.FingerGymnastics.DB.Models
{
    using Mhaze.Unity.DB.Models;
    using System.Collections.Generic;

    public class Track : Model
    {
        private long id;

        private Dictionary<long, Gesture> gestures;

        public Track(long id)
        {
            this.id = id;
        }

        [PrimaryKey]
        public long ID
        {
            get
            {
                return this.id;
            }

            set
            {
                this.id = value;
            }
        }

        [ManyToManyRelation(
            "id",
            "track",
            "track_id",
            "track_gestures",
            "gesture_id",
            "gesture",
            "id"
        )]
        public Dictionary<long, Gesture> Gestures
        {
            get
            {
                return this.gestures;
            }

            set
            {
                this.gestures = value;
            }
        }
    }
}
