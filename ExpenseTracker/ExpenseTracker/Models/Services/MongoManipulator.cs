﻿using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Diagnostics.Eventing.Reader;
using System.Security.AccessControl;
using System.Security.Principal;

/* Package install command on Package manager 
Install-Package MongoDB.Bson;
Install-Package MongoDB.Driver.Core;
Install-Package MongoDB.Driver
*/

namespace ExpenseTracker.Models.Services
{
    public static class MongoManipulator
    {
        private static string? DATABASE_NAME;
        private static string? DATABASE_ADDRESS;
        private static MongoServerAddress? address;
        private static MongoClientSettings? settings;
        private static MongoClient? client;
        private static IMongoDatabase? database;
        private static IConfiguration? config;

        public static void Initialize(IConfiguration configuration)
        {
            config = configuration;
            var sections = config.GetSection("ConnectionStrings");
            DATABASE_ADDRESS = sections.GetValue<string>("MongoAddress");
            DATABASE_NAME = sections.GetValue<string>("MongoDatabaseName");
            address = new MongoServerAddress(DATABASE_ADDRESS);
            settings = new MongoClientSettings() { Server = address };
            client = new MongoClient(settings);
            database = client.GetDatabase(DATABASE_NAME);
			Console.WriteLine($"MongoDB Address: {DATABASE_ADDRESS}");
			Console.WriteLine($"MongoDB Database: {DATABASE_NAME}");
		}
        private static IMongoDatabase GetDB()
        {
            if (database == null)
                throw new Exception("Database is uninitialized");
            else
                return database;
         }


		public static void Save<T>(T entity) where T : DB_SaveableObject    
		{
			string collectionName = typeof(T).Name; 
			var collection = GetDB().GetCollection<T>(collectionName);
			if (entity._id == ObjectId.Empty) 
				collection.InsertOne(entity);
			else
			{
				var filter = Builders<T>.Filter.Eq(e => e._id, entity._id);
				collection.ReplaceOne(filter, entity, new ReplaceOptions { IsUpsert = true });
			}
		}

		public static T GetObjectById<T>(T entity) where T : DB_SaveableObject
		{
			string collectionName = typeof(T).Name;
			var collection = GetDB().GetCollection<T>(collectionName);
			var filter = Builders<T>.Filter.Eq(e => e._id, entity._id);
            return collection.Find(filter).FirstOrDefault();
		}

		public static async Task<List<T>> GetAllObjects<T>() where T : DB_SaveableObject
		{
			string collectionName = typeof(T).Name;
			var collection = GetDB().GetCollection<T>(collectionName);
			return await collection.Find(_ => true).ToListAsync();
		}

        private static void Delete<T>(T entity) where T : DB_SaveableObject
        {
            string collectionName = typeof(T).Name;
            var collection = GetDB().GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq(e => e._id, entity._id);
            collection.DeleteOne(filter);
        }

    }
}
