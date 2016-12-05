namespace HSA.FingerGymnastics.DB.Models
{
    public class Translation : UnityModel
    {
        private string text;

        public Translation(string unityObjectName) : base(unityObjectName) {}

        [TranslationColumn("msgstr")]
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                this.text = value;
            }
        }
    }
}
