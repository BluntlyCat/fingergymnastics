namespace HSA.FingerGymnastics.DB.Models
{
    using Mhaze.Unity.DB.Models;

    public class LastSelected : Model
    {
        private long id;
        private string name;

        public LastSelected(long id)
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
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public static void SetLastItem(string name)
        {
            LastSelected lastSelected;
            object lastID = GetLastId<LastSelected>();

            if (lastID == null)
                lastSelected = new LastSelected(1L);
            else
                lastSelected = GetModel<LastSelected>(lastID);

            lastSelected.Name = name;
            lastSelected.Save();
        }

        public static LastSelected GetLastItem()
        {
            LastSelected lastSelected;
            object lastID = GetLastId<LastSelected>();

            if (lastID == null)
            {
                lastSelected = new LastSelected(1L);
                lastSelected.Name = "";
                lastSelected.Save();
            }
            else
                lastSelected = GetModel<LastSelected>(lastID);
            
            return lastSelected;
        }
    }
}
