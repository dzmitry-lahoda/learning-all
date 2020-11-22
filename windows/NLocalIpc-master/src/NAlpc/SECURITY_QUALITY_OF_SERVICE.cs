using System.Runtime.InteropServices;

namespace NAlpc
{

    public enum SECURITY_IMPERSONATION_LEVEL
    {

        SecurityAnonymous,

        SecurityIdentification,

        SecurityImpersonation,

        SecurityDelegation,
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct SECURITY_QUALITY_OF_SERVICE
    {
        public uint Length;
        public SECURITY_IMPERSONATION_LEVEL ImpersonationLevel;
        public bool ContextTrackingMode;
        public bool EffectiveOnly;

        public static SECURITY_QUALITY_OF_SERVICE Create(SECURITY_IMPERSONATION_LEVEL SecurityImpersonation, bool EffectiveOnly, bool DynamicTracking)
        {
            var SecurityQos = new SECURITY_QUALITY_OF_SERVICE();
            unsafe
            {
                SecurityQos.Length = (uint)sizeof(SECURITY_QUALITY_OF_SERVICE);//12 in 64 bits
            }

            SecurityQos.ImpersonationLevel = SecurityImpersonation;
            SecurityQos.EffectiveOnly = EffectiveOnly;
            SecurityQos.ContextTrackingMode = DynamicTracking;
            return SecurityQos;
        }
    };
}