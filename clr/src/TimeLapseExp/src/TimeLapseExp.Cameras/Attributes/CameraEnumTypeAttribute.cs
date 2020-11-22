using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.Cameras.Attributes
{
    /// <summary>
    /// This attribute used to add new enums associated with new 
    /// camera models.
    ///  </summary>
    public class CameraEnumTypeAttribute:Attribute
    {
        private Type _cameraContollerBaseType;

        private Type _enumType;

        

    }
}
