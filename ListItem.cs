using System.Collections.ObjectModel;

namespace Common
{
    /// <summary>
    /// This class and its associated ListItem class can be used to source a ListBox or ListView.
    /// This provides an advantage over an Enum-based list as it provides for the need to have an
    /// index value needed for identifying the item in the list as well as a value for use in display
    /// in the list or elsewhere.  In particular, this is helpful when the values are just numeric; e.g.,
    /// a list of rotation values: 0, 90, 180, 270.  Numeric values cannot be used as the names of enum
    /// items but can be used in these classes.
    /// </summary>
    public class ListItems : ObservableCollection<ListItem>
    {
        public ListItem Find(string key)
        {
            foreach (var r in this)
            {
                if (r.Key == key)
                    return r;
            }

            return null;
        }

        public int Add(string value)
        {
            var index = Count;
            Add(index, value, value);

            return index;
        }

        public int Add(string key, string value)
        {
            var index = Count;
            Add(index, key, value);

            return index;
        }

        public int Add(int index, string value)
        {
            Add(index, value, value);

            return index;
        }

        public int Add(int index, string key, string value)
        {
            Add(new ListItem(index, key, value));

            return index;
        }
    }

    public class ListItem
    {
        public int Index { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public ListItem(int index, string value)
        {
            Index = index;
            Key = value;
            Value = value;
        }

        public ListItem(int index, string key, string value)
        {
            Index = index;
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return Key.ToString();
        }
    }

}
