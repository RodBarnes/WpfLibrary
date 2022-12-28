using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace Common
{
    /// <summary>
    /// Found this example of a means to replace ObservableCollection<T> with
    /// something that supports adding a range to the collection.
    /// See this article: https://baumbartsjourney.wordpress.com/2009/06/01/an-alternative-to-observablecollection/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObservableList<T> : IList<T>, INotifyCollectionChanged { }

    public class ObservableList<T> : List<T>, IObservableList<T>, INotifyPropertyChanged
    {
        public ObservableList()
        {
            IsNotifying = true;

            // As a gimmick, I wanted to bind to the Count property, so I
            // use the OnPropertyChanged event from the INotifyPropertyChanged
            // interface to notify about Count changes.
            CollectionChanged += new NotifyCollectionChangedEventHandler(
                delegate (object sender, NotifyCollectionChangedEventArgs e)
                {
                    if (sender is null)
                    {
                        throw new ArgumentNullException(nameof(sender));
                    }

                    if (e is null)
                    {
                        throw new ArgumentNullException(nameof(e));
                    }

                    NotifyPropertyChanged("Count");
                }
            );
        }

        #region Properties
        
        public bool IsNotifying { get; set; }
 
        #endregion

        #region Adding and removing items

        public new void Add(T item)
        {
            base.Add(item);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            base.AddRange(collection);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, new List<T>(collection)));
        }

        public new void Clear()
        {
            base.Clear();
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public new void Insert(int i, T item)
        {
            base.Insert(i, item);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
        }

        public new void InsertRange(int i, IEnumerable<T> collection)
        {
            base.InsertRange(i, collection);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, collection));
        }

        public new void Remove(T item)
        {
            base.Remove(item);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item));
        }

        public new void RemoveAll(Predicate<T> match)
        {
            var backup = FindAll(match);
            base.RemoveAll(match);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, backup));
        }

        public new void RemoveAt(int i)
        {
            var backup = this[i];
            base.RemoveAt(i);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, backup));
        }

        public new void RemoveRange(int index, int count)
        {
            var backup = GetRange(index, count);
            base.RemoveRange(index, count);
            NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, backup));
        }

        public new T this[int index]
        {
            get => base[index];
            set
            {
                T oldValue = base[index];
                NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, value, oldValue));
            }
        }

        #endregion

        #region INotifyCollectionChanged Members

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        protected void NotifyCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (IsNotifying && CollectionChanged != null)
                try
                {
                    CollectionChanged(this, e);
                }
                catch (NotSupportedException)
                {
                    NotifyCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
                }
        }
        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
