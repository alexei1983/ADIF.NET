using System;
using System.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// Represents a collection of objects that implement the <see cref="ITag"/> interface and which define 
  /// an ADIF header.
  /// </summary>
  public class ADIFHeader : ADIFTagCollection {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public override ITag this[int index]
    {

      get
      {
        return base[index];
      }

      set
      {

        if (value is null)
          return;

        if (!value.Header)
          throw new ArgumentException("Cannot insert QSO tag into ADIF header.");

        base[index] = value;
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFHeader"/> class.
    /// </summary>
    public ADIFHeader() : base() { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFHeader"/> class.
    /// </summary>
    /// <param name="tags">Initial <see cref="ITag"/> options to add to the collection.</param>
    public ADIFHeader(params ITag[] tags) : this()
    {
      AddRange(tags);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tags"></param>
    public override void AddRange(params ITag[] tags)
    {
      if (tags != null)
        foreach (var tag in tags)
          this.Add(tag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public override void Add(ITag tag)
    {

      if (tag is null)
        return;

      if (!tag.Header)
        throw new ArgumentException("Cannot insert QSO tag into ADIF header.");

      base.Add(tag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="tag"></param>
    public override void Insert(int index, ITag tag)
    {

      if (tag is null)
        return;

      if (!tag.Header)
        throw new ArgumentException("Cannot insert QSO tag into ADIF header.");

      base.Insert(index, tag);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    public UserDefTag GetUserDefinedTag(string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentException("User-defined field name is required.", nameof(fieldName));

      return this.FirstOrDefault(f => f is UserDefTag userDefTag && 
                                      fieldName.Equals(userDefTag.FieldName, StringComparison.OrdinalIgnoreCase)) as UserDefTag;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="fieldName"></param>
    public bool IsUserDefinedTag(string fieldName)
    {
      return GetUserDefinedTag(fieldName) != null;
    }
  }
}
