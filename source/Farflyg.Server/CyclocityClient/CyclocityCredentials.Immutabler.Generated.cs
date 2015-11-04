namespace Farflyg.Server.CyclocityClient
{
    partial class CyclocityCredentials
    {
        private CyclocityCredentials(string @baseUrl, string @apiKey, string @contract)
        {
            BaseUrl = @baseUrl;
            ApiKey = @apiKey;
            Contract = @contract;
        }

        public static CyclocityCredentials Create(string @apiKey, string @contract)
        {
            if (!IsApiKeyValid(@apiKey))
                throw new System.ArgumentException();
            if (!IsContractValid(@contract))
                throw new System.ArgumentException();
            var _instance = new CyclocityCredentials(DefaultBaseUrl, @apiKey, @contract);
            return _instance;
        }

        public CyclocityCredentials WithBaseUrl(string @baseUrl)
        {
            if (!IsBaseUrlValid(@baseUrl))
                throw new System.ArgumentException();
            var _instance = new CyclocityCredentials(@baseUrl, ApiKey, Contract);
            return _instance;
        }

        public CyclocityCredentials WithApiKey(string @apiKey)
        {
            if (!IsApiKeyValid(@apiKey))
                throw new System.ArgumentException();
            var _instance = new CyclocityCredentials(BaseUrl, @apiKey, Contract);
            return _instance;
        }

        public CyclocityCredentials WithContract(string @contract)
        {
            if (!IsContractValid(@contract))
                throw new System.ArgumentException();
            var _instance = new CyclocityCredentials(BaseUrl, ApiKey, @contract);
            return _instance;
        }
    }
}