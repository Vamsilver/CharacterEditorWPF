using CharacterEditorCore.Items;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore.MongoDb
{
    public class MongoDb
    {
        public static void AddToDataBase(Character character)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Characters");
            var collection = database.GetCollection<Character>("CharactersCollection");
            collection.InsertOne(character);
        }

        public static void ReplaceOneInDataBase(Character character)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Characters");
            var filter = new BsonDocument("_id", character._id);
            var collection = database.GetCollection<Character>("CharactersCollection");
            collection.ReplaceOne(filter, character);
        }

        public static Character FindById(string id)
        {
            var client = new MongoClient("mongodb://localhost");
            var filter = new BsonDocument("_id", ObjectId.Parse(id));
            var database = client.GetDatabase("Characters");
            var collection = database.GetCollection<Character>("CharactersCollection");
            return collection.Find(filter).FirstOrDefault();
        }


        public static IMongoCollection<Character> GetCollection()
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Characters");
            return database.GetCollection<Character>("CharactersCollection");
        }

        public static void ReplaceOneParametr(Character character, string property, List<Item> items)
        {
            var client = new MongoClient("mongodb://localhost");
            var database = client.GetDatabase("Characters");
            var collection = database.GetCollection<Character>("CharactersCollection");
            var updateDefenition = Builders<Character>.Update.Set(property, items);
            collection.UpdateOne(x => x._id == character._id, updateDefenition);
        }
    }
}
