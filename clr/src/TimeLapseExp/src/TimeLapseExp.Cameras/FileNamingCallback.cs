using System;
using System.Collections.Generic;
using System.Text;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Transforms file named stored on the card to name under which image saved on other data storage.
    /// </summary>
    /// <param name="fileNameOnMemoryCard"></param>
    /// <returns></returns>
    public delegate String FileNamingCallback(String fileNameOnMemoryCard);
}