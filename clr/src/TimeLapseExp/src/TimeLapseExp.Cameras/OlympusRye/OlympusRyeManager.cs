using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TimeLapseExp.Cameras.OlympusRye
{
    /// <summary>
    /// Reserved for multiple cameras control.
    /// After getting list of cameras user should explicitly say which he wants to use.
    /// So if at last one chosen camera will broke - everything brokes.
    /// Manager controls ever CRyeNET.CurrentCamera and CRyeNET.Index sets,
    /// saves activity and is used in realization of "transactions".
    /// </summary>
    public sealed class OlympusRyeManager
    {
    }
}
