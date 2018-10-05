namespace webapi
{
    public class ApiKey
    {
        public string key;
        public string adminKey;

        public ApiKey(string apikey, string apiAdminKey) 
        {
            key = apikey;
            adminKey = apiAdminKey;
        }
    }
}
