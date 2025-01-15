
namespace org.goodspace.Data.Radio.Adif.Attributes
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    /// <param name="name"></param>

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class AlternateNameAttribute(string name) : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = name;
    }
}
