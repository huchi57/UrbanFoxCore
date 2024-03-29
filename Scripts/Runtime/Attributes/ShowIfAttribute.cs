using System;

namespace UrbanFox
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ShowIfAttribute : BaseCompareAttribute
    {
        public ShowIfAttribute(string comparePropertyName, object compareValue) : base(comparePropertyName, compareValue) { }
    }
}
