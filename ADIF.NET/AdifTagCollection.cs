using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// Represents a collection of objects that implement the <see cref="ITag"/> interface.
  /// </summary>
  public class ADIFTagCollection : ICollection<ITag>, IEnumerable, IEnumerable<ITag>, IList<ITag> {

    public virtual ITag this[int index] {
      get {
        return tags[index];
        }

      set {

        if (value is null)
          return;

        tags[index] = value;
        }
      }

    public int Count => tags.Count;

    public bool IsReadOnly => false;

    public ADIFTagCollection() {
      tags = new List<ITag>();
      }

    public ADIFTagCollection(params ITag[] tags) : this() {
      AddRange(tags);
      }

    public virtual void Add(ITag tag) {
      if (tag is null)
        return;

      tags.Add(tag);   
      }

    public virtual void AddRange(params ITag[] tags) {
      if (tags != null) {
        foreach (var tag in tags)
          this.Add(tag);
        }
      }

    public virtual bool Contains(ITag tag) {
      return tags.Contains(tag);
      }

    public virtual bool Contains(string tagName) {
      return tags.FirstOrDefault(t => t.Name.Equals(tagName, 
                                                    StringComparison.OrdinalIgnoreCase)) != null;
      }

    public virtual void Insert(int index, ITag tag) {

      if (tag is null)
        return;

      tags.Insert(index, tag);
      }

    public virtual bool Remove(ITag tag) {

      if (tag is null)
        return false;

      return tags.Remove(tag);
      }

    public virtual void RemoveAt(int index) {
      tags.RemoveAt(index);
      }

    public virtual void Clear() {
      tags.Clear();
      }

    public virtual int IndexOf(ITag tag) {
      if (tag is null)
        return -1;

      return tags.IndexOf(tag);
      }

    public virtual int IndexOf(string tagName)
    {
      if (string.IsNullOrEmpty(tagName))
        return -1;

      var tag = this.FirstOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));

      if (tag != null)
        return IndexOf(tag);

      return -1;
    }

    public void CopyTo(ITag[] array, int arrayIndex) {
      tags.CopyTo(array, arrayIndex);
      }

    public IEnumerator<ITag> GetEnumerator() {
      return tags.GetEnumerator();
      }

    IEnumerator IEnumerable.GetEnumerator() {
      return tags.GetEnumerator();
      }

    public virtual bool Contains(Type tagType) {
      return this.FirstOrDefault(t => tagType.Equals(t.GetType())) != null;
      }

    List<ITag> tags;
    }
  }
