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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
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

    /// <summary>
    /// 
    /// </summary>
    public int Count => tags.Count;

    /// <summary>
    /// 
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// 
    /// </summary>
    public ADIFTagCollection() {
      tags = new List<ITag>();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tags"></param>
    public ADIFTagCollection(params ITag[] tags) : this() {
      AddRange(tags);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public virtual void Add(ITag tag) {
      if (tag is null)
        return;

      tags.Add(tag);   
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tags"></param>
    public virtual void AddRange(params ITag[] tags) {
      if (tags != null) {
        foreach (var tag in tags)
          this.Add(tag);
        }
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public virtual bool Contains(ITag tag) {
      return tags.Contains(tag);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
    public virtual bool Contains(string tagName) {
      return tags.FirstOrDefault(t => t.Name.Equals(tagName, 
                                                    StringComparison.OrdinalIgnoreCase)) != null;
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="tag"></param>
    public virtual void Insert(int index, ITag tag) {

      if (tag is null)
        return;

      tags.Insert(index, tag);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public virtual bool Remove(ITag tag) {

      if (tag is null)
        return false;

      return tags.Remove(tag);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public virtual void RemoveAt(int index) {
      tags.RemoveAt(index);
      }

    /// <summary>
    /// 
    /// </summary>
    public virtual void Clear() {
      tags.Clear();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public virtual int IndexOf(ITag tag) {
      if (tag is null)
        return -1;

      return tags.IndexOf(tag);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagName"></param>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public void CopyTo(ITag[] array, int arrayIndex) {
      tags.CopyTo(array, arrayIndex);
      }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerator<ITag> GetEnumerator() {
      return tags.GetEnumerator();
      }

    /// <summary>
    /// 
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() {
      return tags.GetEnumerator();
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagType"></param>
    public virtual bool Contains(Type tagType) {
      return this.FirstOrDefault(t => tagType.Equals(t.GetType())) != null;
      }

    List<ITag> tags;
    }
  }
