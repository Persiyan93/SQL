using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MiniORM
{
    public class DbSet<T> : ICollection<T>
        where T :class,new()
    {
        internal ChangeTracker<T> ChangeTracker { get; set; }

        internal IList<T> Entities { get; set; }


        internal DbSet(IEnumerable<T> entites)
        {
            this.Entities = entites.ToList();
            this.ChangeTracker = new ChangeTracker<T>(entites);
        }

        public int Count => this.Entities.Count();

        public bool IsReadOnly => this.Entities.IsReadOnly;

        public void Add(T item)
        {
            if (item==null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null!");
            }
            this.Entities.Add(item);
            this.ChangeTracker.Add(item);
        }

        public void Clear()
        {
            while (this.Entities.Any())
            {
                var entity = this.Entities.First();
                this.Remove(entity);
            }
        }

        public bool Contains(T item)
        {
           return this.Entities.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.Entities.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        public bool Remove(T item)
        {
            if (item==null)
            {
                throw new ArgumentNullException(nameof(item), "Item cannot be null!");

            }
            var removedSuccessfully = this.Entities.Remove(item);
            if (removedSuccessfully)
            {
                ChangeTracker.Remove(item);
            }
            return removedSuccessfully;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}