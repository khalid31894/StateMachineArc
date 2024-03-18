using UnityEngine;

namespace ProjectCore.Variables
{
    [CreateAssetMenu(fileName = "v_", menuName = "ProjectCore/Variables/String Persistent")]
    public class DBString : String, IDBVariable
    {
        [SerializeField] protected string Key;

        private void OnEnable()
        {
            Load();
        }

        public void Refresh()
        {
            Load();
        }
        
        public override void SetValue(string value)
        {
            base.SetValue(value);
            Save();
        }

        public override void SetValue(String value)
        {
            base.SetValue(value);
            Save();
        }

        protected virtual void Save()
        {
            DBManager.SetString(this, Key, Value);
        }

        protected virtual void Load()
        {
            if (!string.IsNullOrEmpty(Key) && DBManager.HasKey(this, Key))
            {
                Value = DBManager.GetString(this, Key);
            }
            else
            {
                if (ResetToDefaultOnPlay)
                {
                    Value = DefaultValue;
                }
                else
                {
                    Value = string.Empty;
                }
            }
        }

        void IDBVariable.Update(object value)
        {
            if (value is string)
            {
                SetValue((string)value);
            }
        }

        object IDBVariable.GetValue()
        {
            return GetValue();
        }
    }
}
