namespace HSA.FingerGymnastics.DB.Models
{
    using System;
    using System.Collections.Generic;

    public class UnitySettingGroup : Model
    {
        private string name;
        private Dictionary<string, UnityKeyValue> keyValue;

        public UnitySettingGroup(string name)
        {
            this.name = name;
        }

        [PrimaryKey]
        public string Name
        {
            get
            {
                return this.name;
            }

            private set
            {
                this.name = value;
            }
        }

        [ManyToManyRelation(
            "name",
            "unitysettinggroup",
            "unitysettinggroup_id",
            "unitysettinggroup_settings",
            "unitykeyvalue_id",
            "unitykeyvalue",
            "key"
        )]
        public Dictionary<string, UnityKeyValue> KeyValue
        {
            get
            {
                return this.keyValue;
            }

            set
            {
                this.keyValue = value;
            }
        }

        public object GetValue(string key)
        {
            if (keyValue.ContainsKey(key))
                return keyValue[key].GetValue();

            throw new Exception(string.Format("Setting with key '{0}' not found", key));
        }

        public T GetValue<T>(string key)
        {
            if (keyValue.ContainsKey(key))
                return keyValue[key].GetValue<T>();

            throw new Exception(string.Format("Setting with key '{0}' not found", key));
        }

        public void SetValue<T>(string key, T value)
        {
            if (keyValue.ContainsKey(key))
                keyValue[key].SetValue(value);

            throw new Exception(string.Format("Setting with key '{0}' not found", key));
        }
    }
}
