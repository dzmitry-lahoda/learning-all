namespace NDceRpc.Microsoft.Interop
{
    /// <summary>
    /// The authorization service constants represent the authorization services passed to various run-time functions.
    /// </summary>
    public enum RPC_C_AUTHZ : uint
    {
        /// <summary>
        /// Server performs no authorization.
        /// </summary>
        RPC_C_AUTHZ_NONE = 0, 
        RPC_C_AUTHZ_NAME = 1,//Server performs authorization based on the client's principal name.
        RPC_C_AUTHZ_DCE = 2,// Server performs authorization checking using the client's DCE privilege attribute certificate (PAC) information, which is sent to the server with each remote procedure call made using the binding handle. Generally, access is checked against DCE access control lists (ACLs).
        RPC_C_AUTHZ_DEFAULT = 0xffffffff
    }
}