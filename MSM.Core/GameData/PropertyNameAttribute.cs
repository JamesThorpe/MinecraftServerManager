using System;

namespace MSM.Core.GameData {
    /// <summary>
    /// Specifies the name of the property as rendered in server.properties
    /// </summary>
    internal class ServerPropertyAttribute : Attribute
    {
        public string Name { get; }
        public bool IsCommon { get; }

        public ServerPropertyAttribute(string propertyName, bool isCommon = false, bool isManaged = false)
        {
            Name = propertyName;
            IsCommon = isCommon;
        }
    }
}