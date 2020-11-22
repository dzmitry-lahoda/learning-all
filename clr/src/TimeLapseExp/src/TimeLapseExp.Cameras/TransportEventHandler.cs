using System;

namespace TimeLapseExp.Cameras
{
    /// <summary>
    /// Represents the method that will handle a OlympusRyeTransport's events 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">TransportEventArgs</param>
    public delegate void TransportEventHandler(Object sender, TransportEventArgs e);
}
