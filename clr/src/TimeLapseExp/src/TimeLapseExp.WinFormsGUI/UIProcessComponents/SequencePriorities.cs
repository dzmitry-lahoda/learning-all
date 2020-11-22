namespace TimeLapseExp.WinFormsGUI
{
    /// <summary>
    /// If exposure, camera makes N pictures with different exposure time, than changes focus position.
    /// Otherwise, camera makes M pictures with different focus position, than changes  exposure time.
    /// </summary>
    public enum SequencePriorities
    {
        Exposure,
        Focus
    }
}