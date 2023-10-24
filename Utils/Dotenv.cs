using System;
using System.Collections.Generic;
using System.IO;

namespace hcode.Utils
{
    public class Dotenv
    {
        public Dotenv()
        {
            LoadEnvFile();
        }

        public Dictionary<string, string> EnvVariables { get; private set; } = new Dictionary<string, string>();

        private void LoadEnvFile()
        {
            var envFilePath = Path.Combine(Directory.GetCurrentDirectory(), "../.env");

            if (!File.Exists(envFilePath))
            {
                return;
            }

            var lines = File.ReadAllLines(envFilePath);

            foreach (var line in lines)
            {
                var parts = line.Split('=');
                if (parts.Length == 2)
                {
                    EnvVariables[parts[0]] = parts[1];
                }
            }
        }

        public Dictionary<string, string> GetEnvVariable()
        {
            return EnvVariables;
        }

        public string GetEnvVariable(string envName)
        {
            var envVariable = GetEnvVariable();

            if (envVariable.ContainsKey(envName))
            {
                return EnvVariables[envName];
            }
            return null;
        }

    }
}
