using Catalog.Api.Data.Interface;
using Catalog.Api.Entities;
using Catalog.Api.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.Api.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly IMongoDatabase _database;
        public CatalogContext(ICatalogDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get { return _database.GetCollection<Product>("Products"); } }
    }
}
