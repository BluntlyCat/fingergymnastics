namespace HSA.FingerGymnastics.DB.Models
{
    using Mhaze.Unity.DB.Models;

    public class HighScore : Model
    {
        private long id;
        private string song;
        private long score;
        private string player;

        public HighScore(long id)
        {
            this.id = id;
        }

        [PrimaryKey]
        public long ID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        [TableColumn]
        public string Song
        {
            get
            {
                return song;
            }

            set
            {
                song = value;
            }
        }

        [TableColumn]
        public long Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }

        [TableColumn]
        public string Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
    }
}
