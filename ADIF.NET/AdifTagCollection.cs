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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    public ITag GetTag(string tagName)
    {
      if (string.IsNullOrEmpty(tagName))
        throw new ArgumentException("Tag name is required.", nameof(tagName));

      return this.FirstOrDefault(t => tagName.Equals(t.Name, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="tagName"></param>
    public T GetTagValue<T>(string tagName)
    {
      if (string.IsNullOrEmpty(tagName))
        throw new ArgumentException("Tag name is required.", nameof(tagName));

      var tag = GetTag(tagName);

      if (tag != null && tag.Value is T tVal)
        return tVal;

      return default(T);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    public bool Remove(string tagName)
    {
      if (!Contains(tagName))
        return false;

      return Remove(GetTag(tagName));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public virtual bool Replace(ITag tag)
    {
      if (tag == null)
        return false;

      var indexOf = IndexOf(tag.Name);

      if (indexOf >= 0)
      {
        RemoveAt(indexOf);
        Insert(indexOf, tag);
        return true;
      }

      return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public virtual bool AddOrReplace(ITag tag)
    {
      var replaced = Replace(tag);

      if (!replaced)
      {
        Add(tag);
        replaced = true;
      }

      return replaced;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="remove"></param>
    /// <param name="replace"></param>
    public virtual bool Replace(ITag remove, ITag replace)
    {
      if (remove == null || replace == null)
        return false;

      var indexOf = IndexOf(remove);

      if (indexOf >= 0 && Remove(remove))
      {
        Insert(indexOf, replace);
        return true;
      }

      return false;
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
