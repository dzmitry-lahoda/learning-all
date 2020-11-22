using Olympus.OIMA.RyeNET;

namespace TimeLapseExp.Cameras
{
    public enum ExposureModes
    {
        Manual=RYE_EXPOSURE_MODE.RYE_EXPOSURE_MANUAL,
        Auto=RYE_EXPOSURE_MODE.RYE_EXPOSURE_AUTO,
        AperturePriority=RYE_EXPOSURE_MODE.RYE_EXPOSURE_APERTURE_PRIORITY,
        ShutterPriority=RYE_EXPOSURE_MODE.RYE_EXPOSURE_SHUTTER_PRIORITY
    }
}
