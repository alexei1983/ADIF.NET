using System;
using System.Linq;
using ADIF.NET.Tags;

namespace ADIF.NET {

  /// <summary>
  /// Contains the ADIF tags describing a QSO.
  /// </summary>
  public class ADIFQSO : ADIFTagCollection {

    public override ITag this[int index] {

      get => base[index];

      set
      {
        if (value is null)
          return;

        if (value.Header)
          throw new ArgumentException("Cannot insert header tag into QSO.");

        if (Contains(value.GetType()))
          throw new ArgumentException($"QSO already contains tag '{value.Name}'");

        base[index] = value;
      }
    }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFQSO"/> class.
    /// </summary>
    public ADIFQSO() : base() { }

    /// <summary>
    /// Creates a new instance of the <see cref="ADIFQSO"/> class.
    /// </summary>
    /// <param name="tags">Tags to add to the QSO.</param>
    public ADIFQSO(params ITag[] tags) : this()
    {
      AddRange(tags);
    }

    /// <summary>
    /// Adds multiple tags to the current <see cref="ADIFQSO"/>.
    /// </summary>
    /// <param name="tags">Tags to add to the QSO.</param>
    public override void AddRange(params ITag[] tags)
    {
      if (tags != null)
        foreach (var tag in tags)
          Add(tag);
    }

    /// <summary>
    /// Adds a single tag to the current <see cref="ADIFQSO"/>.
    /// </summary>
    /// <param name="tag">Tag to add to the QSO.</param>
    public override void Add(ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Cannot insert header tag into QSO.");

      if (Contains(tag.Name))
        throw new ArgumentException($"QSO already contains tag '{tag.Name}'");

      base.Add(tag);
    }

    /// <summary>
    /// Adds a <see cref="CallTag"/> to the current QSO.
    /// </summary>
    /// <param name="call">Callsign to add as the contacted station.</param>
    public void AddCall(string call)
    {
      if (!Contains(typeof(CallTag)))
        Add(new CallTag(call));
    }

    /// <summary>
    /// Adds a <see cref="QSODateTag"/> to the current QSO.
    /// </summary>
    /// <param name="qsoDate">Date to add as the QSO date.</param>
    public void AddQSODate(DateTime qsoDate)
    {
      if (!Contains(typeof(QSODateTag)))
        Add(new QSODateTag(qsoDate));
    }

    /// <summary>
    /// Adds a <see cref="TimeOnTag"/> to the current QSO.
    /// </summary>
    /// <param name="timeOn">Time to add as the QSO time-on.</param>
    public void AddTimeOn(DateTime timeOn)
    {
      if (!Contains(typeof(TimeOnTag)))
        Add(new TimeOnTag(timeOn));
    }

    /// <summary>
    /// Adds a <see cref="ModeTag"/> to the current QSO.
    /// </summary>
    /// <param name="mode">Mode to add to the current QSO.</param>
    public void AddMode(string mode)
    {
      if (!Contains(typeof(ModeTag)))
        Add(new ModeTag(mode));
    }

    /// <summary>
    /// Adds a <see cref="BandTag"/> to the current QSO.
    /// </summary>
    /// <param name="band">Band to add to the current QSO.</param>
    public void AddBand(string band)
    {
      if (!Contains(typeof(BandTag)))
        Add(new BandTag(band));
    }

    /// <summary>
    /// Adds an <see cref="OperatorTag"/> to the current QSO.
    /// </summary>
    /// <param name="operatorCall">Callsign to add to the QSO as the operator.</param>
    public void AddOperator(string operatorCall)
    {
      if (!Contains(typeof(OperatorTag)))
        Add(new OperatorTag(operatorCall));
    }

    /// <summary>
    /// Adds a <see cref="CommentTag"/> to the current QSO.
    /// </summary>
    /// <param name="comment">Comment to add to the QSO.</param>
    public void AddComment(string comment)
    {
      if (!Contains(typeof(CommentTag)))
        Add(new CommentTag(comment));
    }

    /// <summary>
    /// Adds a <see cref="FreqTag"/> to the current QSO, optionally setting the band.
    /// </summary>
    /// <param name="frequency">Frequency to set for the current QSO.</param>
    /// <param name="setBand">Whether or not to set the band for the current QSO based on the frequency.</param>
    public void AddFreq(double frequency, bool setBand = false)
    {
      if (!Contains(typeof(FreqTag)))
        Add(new FreqTag(frequency));

      if (setBand)
      {
        var band = Band.Get(frequency);
        if (band != null)
          AddBand(band.Name);
      }
    }

    /// <summary>
    /// Adds a <see cref="NameTag"/> to the current QSO.
    /// </summary>
    /// <param name="name">Personal name of the contacted station.</param>
    public void AddName(string name)
    {
      if (!Contains(typeof(NameTag)))
        this.Add(new NameTag(name));
    }

    /// <summary>
    /// Retrieves the date and time that the QSO started.
    /// </summary>
    public DateTime? GetQSODateTimeOn()
    {
      DateTime? result = null;

      if (Contains(typeof(QSODateTag)))
      {
        var qsoDate = this.FirstOrDefault(q => q.GetType().Equals(typeof(QSODateTag)))?
                                                          .Value as DateTime?;

        if (qsoDate.HasValue && Contains(typeof(TimeOnTag)))
        {
          var timeOn = this.FirstOrDefault(q => q.GetType().Equals(typeof(TimeOnTag)))?
                                                           .Value as DateTime?;
          if (timeOn.HasValue)
          {
            result = new DateTime(qsoDate.Value.Year,
                                  qsoDate.Value.Month,
                                  qsoDate.Value.Day,
                                  timeOn.Value.Hour,
                                  timeOn.Value.Minute,
                                  timeOn.Value.Second);
          }
        }
      }
      return result;
    }

    /// <summary>
    /// Retrieves the date and time that the QSO ended.
    /// </summary>
    public DateTime? GetQSODateTimeOff()
    {
      DateTime? result = null;

      if (Contains(typeof(QSODateOffTag)))
      {
        var qsoDate = this.FirstOrDefault(q => q.GetType().Equals(typeof(QSODateOffTag)))?
                                                          .Value as DateTime?;

        if (qsoDate.HasValue && Contains(typeof(TimeOffTag)))
        {
          var timeOn = this.FirstOrDefault(q => q.GetType().Equals(typeof(TimeOffTag)))?
                                                           .Value as DateTime?;
          if (timeOn.HasValue)
          {
            result = new DateTime(qsoDate.Value.Year,
                                  qsoDate.Value.Month,
                                  qsoDate.Value.Day,
                                  timeOn.Value.Hour,
                                  timeOn.Value.Minute,
                                  timeOn.Value.Second);
          }
        }
      }
      return result;
    }

    /// <summary>
    /// Inserts a tag into the current <see cref="ADIFQSO"/> at the specified index.
    /// </summary>
    /// <param name="index">Index at which the tag will be inserted.</param>
    /// <param name="tag">Tag that will be inserted into the QSO.</param>
    public override void Insert(int index, ITag tag)
    {
      if (tag is null)
        return;

      if (tag.Header)
        throw new ArgumentException("Cannot insert header tag into QSO.");

      if (Contains(tag.GetType()))
        throw new ArgumentException($"QSO already contains tag '{tag.Name}'");

      base.Insert(index, tag);
    }

    /// <summary>
    /// 
    /// </summary>
    public bool ValidateValues(bool throwOnInvalid = false)
    {
      foreach (var tag in this)
      {
        if (!tag.ValidateValue())
        {
          if (throwOnInvalid)
            throw new Exception($"Invalid value for ADIF tag '{tag.Name}': {tag.TextValue}");

          return false;
        }
      }
      return true;
    }

    /// <summary>
    /// Determines whether or not the current <see cref="ADIFQSO"/> contains the specified tags.
    /// </summary>
    /// <param name="tagNames">Names of tags to check.</param>
    public bool HasTags(params string[] tagNames)
    {
      if (tagNames == null)
        throw new ArgumentNullException(nameof(tagNames), "Tag name(s) are required.");

      foreach (var tagName in tagNames)
        if (!Contains(tagName))
          return false;

      return true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tagTypes"></param>
    public bool HasTags(params Type[] tagTypes)
    {
      if (tagTypes == null)
        throw new ArgumentNullException(nameof(tagTypes), "Tag type(s) are required.");

      foreach (var tagType in tagTypes)
        if (!Contains(tagType))
          return false;

      return true;
    }
  }
}
