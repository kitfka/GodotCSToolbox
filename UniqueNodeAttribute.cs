using System;
namespace GodotCSToolbox
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public class UniqueNodeAttribute : Attribute
    {
        public UniqueNodeAttribute() { }
    }
}
