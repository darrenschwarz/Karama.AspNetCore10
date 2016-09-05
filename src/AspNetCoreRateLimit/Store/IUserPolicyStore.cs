namespace AspNetCoreRateLimit
{
    public interface IUserPolicyStore
    {
        bool Exists(string id);
        UserRateLimitPolicy Get(string id);
        void Remove(string id);
        void Set(string id, UserRateLimitPolicy policy);
    }
}