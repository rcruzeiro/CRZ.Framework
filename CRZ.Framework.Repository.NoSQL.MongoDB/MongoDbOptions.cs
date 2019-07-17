using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CRZ.Framework.Repository.NoSQL.MongoDB
{
    public class MongoDbOptions
    {
        protected string SectionName { get; }

        public string ConnectionString { get; protected set; }

        public string Host { get; protected set; }

        public string Port { get; protected set; }

        public string Database { get; protected set; }

        public string Collection { get; protected set; }

        public string Username { get; protected set; }

        public string Password { get; protected set; }

        public MongoDbOptions(string host,
                              string database,
                              string collection,
                              string port = "27017",
                              string username = null,
                              string password = null)
        {
            Host = host ?? throw new ArgumentNullException(nameof(host));
            Database = database ?? throw new ArgumentNullException(database);
            Collection = collection ?? throw new ArgumentNullException(collection);
            Port = port;
            Username = username;
            Password = password;

            CreateConnectionString();
        }

        public MongoDbOptions(IConfiguration configuration, string sectionName = "MongoDb")
        {
            SectionName = sectionName;

            new ConfigureFromConfigurationOptions<MongoDbOptions>(
                configuration.GetSection(SectionName)).Configure(this);

            CreateConnectionString();
        }

        private void CreateConnectionString()
        {
            if (!string.IsNullOrWhiteSpace(Username) &&
               !string.IsNullOrWhiteSpace(Password))
                ConnectionString = $"mongodb://{Username}:{Password}@{Host}:{Port}";
            else
                ConnectionString = $"mongodb://{Host}:{Port}";
        }
    }
}
