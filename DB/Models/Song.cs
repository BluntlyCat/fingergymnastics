using System.Collections.Generic;
using UnityEngine;

namespace HSA.FingerGymnastics.DB.Models
{
    public class Song : UnityModel
    {
        private AudioClip file;
        private Dictionary<long, Track> tracks;

        public Song(string unityObjectName) : base(unityObjectName) {}

        [TableColumn]
        [Resource]
        public AudioClip File
        {
            get
            {
                return file;
            }

            set
            {
                this.file = value;
            }
        }

        [ManyToManyRelation(
            "unityObjectName",
            "song",
            "song_id",
            "song_tracks",
            "track_id",
            "track",
            "id"
        )]
        public Dictionary<long, Track> Tracks
        {
            get
            {
                return this.tracks;
            }

            set
            {
                this.tracks = value;
            }
        }
    }
}
