﻿
namespace Catalog.Api.Settings
{
    public class CatalogDatabaseSettings : ICatalogDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
