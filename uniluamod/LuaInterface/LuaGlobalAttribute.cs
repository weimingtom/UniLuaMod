using System;

namespace UniLuaInterface
{
    /// <summary>
    /// Marks a method for global usage in Lua scripts
    /// </summary>
    /// <see cref="LuaRegistrationHelper.TaggedInstanceMethods"/>
    /// <see cref="LuaRegistrationHelper.TaggedStaticMethods"/>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class LuaGlobalAttribute : Attribute
    {
    	private string name;
    	private string description;
    	
        /// <summary>
        /// An alternative name to use for calling the function in Lua - leave empty for CLR name
        /// </summary>
        public string Name { get {return name;} set {name = value;} }

        /// <summary>
        /// A description of the function
        /// </summary>
        public string Description { get { return description;} set {description = value;} }
    }
}
