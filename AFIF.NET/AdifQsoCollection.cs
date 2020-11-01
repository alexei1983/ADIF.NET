using System;
using ADIF.NET.Tags;

namespace ADIF.NET {

  public class AdifQsoCollection : AdifTagCollection {

    public AdifQsoCollection() : base() { }

    public AdifQsoCollection(params ITag[] tags) : this() {
      AddRange(tags);
      }

    public override void AddRange(params ITag[] tags) {
      if (tags != null)
        foreach (var tag in tags)
          this.Add(tag);
      }

    public override void Add(ITag tag) {

      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Cannot insert header tag into QSO collection.");

      base.Add(tag);
      }

    public override void Insert(int index, ITag tag) {

      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Cannot insert header tag into QSO collection.");

      base.Insert(index, tag);
      }
    }
  }
