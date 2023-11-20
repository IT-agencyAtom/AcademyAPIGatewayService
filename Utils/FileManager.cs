using System.Diagnostics;
using System.Text.Json;

namespace CrmIntegration.Utils
{
    public static class FileManager
    {
        private static string GetCredentialsFilePath(Integrations integration) => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), typeof(FileManager).Namespace, "data", $"{integration}-credentials.json");

        public static void SaveTokens<T>(Integrations integration, T tokens)
        {
            try
            {
                if (tokens == null)
                {
                    Debug.WriteLine("Tokens required");
                    return;
                }

                var credentialJson = JsonSerializer.Serialize(tokens, options: new JsonSerializerOptions
                {
                    WriteIndented = true
                });

                var credentialsFilePath = GetCredentialsFilePath(integration);
                Directory.CreateDirectory(Path.GetDirectoryName(credentialsFilePath));
                File.WriteAllText(credentialsFilePath, credentialJson);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public static void ClearTokens(Integrations integration)
        {
            var path = GetCredentialsFilePath(integration);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static T GetTokens<T>(Integrations integration)
        {
            try
            {
                var credentialsFilePath = GetCredentialsFilePath(integration);
                if (File.Exists(credentialsFilePath))
                {
                    var json = File.ReadAllText(credentialsFilePath);
                    var credential = JsonSerializer.Deserialize<T>(json);
                    return credential;
                }
            }
            catch (Exception ex)
            {
                Debug.Write(ex);
            }

            return default;
        }
    }
}
