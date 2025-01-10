
using org.goodspace.Data.Radio.Adif.Tags;

namespace org.goodspace.Data.Radio.Adif
{

    /// <summary>
    /// Represents a collection of objects that implement the <see cref="ITag"/> interface and which define 
    /// an ADIF header.
    /// </summary>
    public class AdifHeader : AdifTagCollection
    {

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

                if (!value.Header && !value.IsAppDef)
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
        public AdifHeader(params ITag[] tags) : this()
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
                    Add(tag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        public override void Add(ITag tag)
        {
            if (tag is null)
                return;

            if (!tag.Header && !tag.IsAppDef)
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

            if (!tag.Header && !tag.IsAppDef)
                throw new ArgumentException("Cannot insert QSO tag into ADIF header.");

            base.Insert(index, tag);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldId"></param>
        /// <param name="dataType"></param>
        public UserDefTag AddUserDefinedTag(string fieldName, int fieldId, string dataType)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("User-defined field name is required.", nameof(fieldName));

            if (IsUserDefinedTag(fieldId))
                throw new ArgumentException($"Field ID {fieldId} already exists.");

            if (IsUserDefinedTag(fieldName))
                throw new ArgumentException($"Field name {fieldName} already exists.");

            if (dataType == null)
                dataType = string.Empty;

            var userDefTag = new UserDefTag(fieldName, fieldId, dataType);

            Add(userDefTag);
            return userDefTag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="dataType"></param>
        public UserDefTag AddUserDefinedTag(string fieldName, string dataType)
        {
            var fieldId = GetMaxUserDefinedFieldId();
            return AddUserDefinedTag(fieldName, ++fieldId, dataType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="options"></param>
        public UserDefTag AddUserDefinedTag(string fieldName, params string[] options)
        {
            var fieldId = GetMaxUserDefinedFieldId();
            return AddUserDefinedTag(fieldName, ++fieldId, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="upperBound"></param>
        /// <param name="lowerBound"></param>
        public UserDefTag AddUserDefinedTag(string fieldName, double lowerBound, double upperBound)
        {
            var fieldId = GetMaxUserDefinedFieldId();
            return AddUserDefinedTag(fieldName, ++fieldId, lowerBound, upperBound);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldId"></param>
        /// <param name="options"></param>
        public UserDefTag AddUserDefinedTag(string fieldName, int fieldId, params string[] options)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("User-defined field name is required.", nameof(fieldName));

            if (IsUserDefinedTag(fieldId))
                throw new ArgumentException($"Field ID {fieldId} already exists.");

            if (IsUserDefinedTag(fieldName))
                throw new ArgumentException($"Field name {fieldName} already exists.");

            var userDefTag = new UserDefTag(fieldName, fieldId, options);

            Add(userDefTag);
            return userDefTag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="fieldId"></param>
        /// <param name="upperBound"></param>
        /// <param name="lowerBound"></param>
        public UserDefTag AddUserDefinedTag(string fieldName, int fieldId, double lowerBound, double upperBound)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("User-defined field name is required.", nameof(fieldName));

            if (IsUserDefinedTag(fieldId))
                throw new ArgumentException($"Field ID {fieldId} already exists.");

            if (IsUserDefinedTag(fieldName))
                throw new ArgumentException($"Field name {fieldName} already exists.");

            var userDefTag = new UserDefTag(fieldName, fieldId, lowerBound, upperBound);

            Add(userDefTag);
            return userDefTag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public UserDefTag? GetUserDefinedTag(string fieldName)
        {
            if (string.IsNullOrEmpty(fieldName))
                throw new ArgumentException("User-defined field name is required.", nameof(fieldName));

            return this.FirstOrDefault(f => f is UserDefTag userDefTag &&
                                            fieldName.Equals(userDefTag.FieldName, StringComparison.OrdinalIgnoreCase)) is UserDefTag userDefTag ? userDefTag : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldId"></param>
        public UserDefTag? GetUserDefinedTag(int fieldId)
        {
            if (fieldId < 1)
                throw new ArgumentException("User-defined field ID must be greater than zero.", nameof(fieldId));

            return this.FirstOrDefault(f => f is UserDefTag userDefTag &&
                                            fieldId == userDefTag.FieldId) is UserDefTag userDefTag ? userDefTag : null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        public bool IsUserDefinedTag(string fieldName)
        {
            return GetUserDefinedTag(fieldName) != null;
        }

        /// <summary>
        /// 
        /// </summary>
        int GetMaxUserDefinedFieldId()
        {
            var userDefs = this.Where(f => f is UserDefTag userDefTag).Cast<UserDefTag>();

            if (userDefs != null && userDefs.Any())
                return userDefs.Max(f => f.FieldId);

            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldId"></param>
        public bool IsUserDefinedTag(int fieldId)
        {
            return GetUserDefinedTag(fieldId) != null;
        }
    }
}
