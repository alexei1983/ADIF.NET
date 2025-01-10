
namespace org.goodspace.Data.Radio.Adif.Attributes
{
    /// <summary>
    /// 
    /// </summary>

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AlternateNameAttribute : Attribute
    {

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public AlternateNameAttribute(string name)
        {
            Name = name;
        }
    }
}
