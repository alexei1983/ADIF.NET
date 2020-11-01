using System;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// Represents a collection of objects that implement the <see cref="ITag"/> interface and which define 
  /// an ADIF header.
  /// </summary>
  public class AdifHeader : AdifTagCollection {

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public override ITag this[int index] {

      get {
        return base[index];
        }

      set {

        if (value is null)
          return;

        if (!value.Header)
          throw new ArgumentException("Cannot insert QSO tag into ADIF header.");

        base[index] = value;
        }
      }

    /// <summary>
    /// Creates a new instance of the <see cref="AdifHeader"/> class.
    /// </summary>
    public AdifHeader() : base() { }

    /// <summary>
    /// Creates a new instance of the <see cref="AdifHeader"/> class.
    /// </summary>
    /// <param name="tags">Initial <see cref="ITag"/> options to add to the collection.</param>
    public AdifHeader(params ITag[] tags) : this() {
      AddRange(tags);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tags"></param>
    public override void AddRange(params ITag[] tags) {
      if (tags != null)
        foreach (var tag in tags)
          this.Add(tag);
      }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tag"></param>
    public override void Add(ITag tag) {

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
    public override void Insert(int index, ITag tag) {

      if (tag is null)
        return;

      if (!tag.Header)
        throw new ArgumentException("Cannot insert QSO tag into ADIF header.");

      base.Insert(index, tag);
      }
    }
  }
