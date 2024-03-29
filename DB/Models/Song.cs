﻿namespace HSA.FingerGymnastics.DB.Models
{
    using Mhaze.Unity.DB.Models;
    using System.Collections.Generic;
    using UnityEngine;

    public class Song : UnityModel
    {
        private string title;
        private AudioClip file;
        private Dictionary<long, Track> tracks;

        public Song(string unityObjectName) : base(unityObjectName) { }

        [TableColumn]
        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                this.title = value;
            }
        }

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

        public double GetLength(long trackId)
        {
            return Tracks[trackId].Length;
        }
    }
}
