namespace Farflyg.Server.CyclocityClient
{
    using System;
    using System.Configuration;
    using System.IO;

    /// <Immutabler />
    public sealed partial class CyclocityCredentials
    {
        public static string ApiKeyConfigurationName => typeof(CyclocityCredentials).FullName + "." + nameof(ApiKey);
        public static string ApiKeyFilePathConfigurationName => typeof(CyclocityCredentials).FullName + "." + nameof(ApiKey) + "FilePath";
        public static string ContractConfigurationName => typeof(CyclocityCredentials).FullName + "." + nameof(Contract);
        public static string BaseUrlConfigurationName => typeof(CyclocityCredentials).FullName + "." + nameof(BaseUrl);

        public string BaseUrl { get; }

        public string ApiKey { get; }

        public string Contract { get; }

        public static string DefaultBaseUrl => "https://api.jcdecaux.com/vls/v1/";

        public static bool IsBaseUrlValid(string baseUrl) => !string.IsNullOrEmpty(baseUrl) && Uri.IsWellFormedUriString(baseUrl, UriKind.Absolute);

        public static bool IsApiKeyValid(string apiKey) => !string.IsNullOrEmpty(apiKey);

        public static bool IsContractValid(string contract) => !string.IsNullOrEmpty(contract);

        public static CyclocityCredentials GetFromConfiguration()
        {
            var _apiKey = ConfigurationManager.AppSettings[ApiKeyConfigurationName];
            var _apiKeyFilePath = ConfigurationManager.AppSettings[ApiKeyFilePathConfigurationName];
            var _contract = ConfigurationManager.AppSettings[ContractConfigurationName];
            var _baseUrl = ConfigurationManager.AppSettings[BaseUrlConfigurationName];

            if (!IsApiKeyValid(_apiKey) && !string.IsNullOrEmpty(_apiKeyFilePath))
            {
                _apiKey = File.ReadAllText(_apiKeyFilePath);
            }

            var _credentials = Create(_apiKey, _contract);

            if (IsBaseUrlValid(_baseUrl))
            {
                _credentials = _credentials.WithBaseUrl(_baseUrl);
            }

            return _credentials;
        }
    }
}
