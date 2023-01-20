namespace MagicVilla_Web.Utility
{
    public static class StaticDetails
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public readonly static string SessionToken = "JWTToken";
    }
}