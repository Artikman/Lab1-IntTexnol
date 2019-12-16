using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kritner.MongoDb.GettingStarted.ConsoleApp
{
    public class MyHelloWorldMongoThing {
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string MyDatas { get; set; }
        public ObjectId Id { get; set; }
    }

    class Program
    {
        static async Task Main(string[] args)
        {
            int n = 10;
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine("What would you like to enter client for MyDatas?");
                //получить ввод от пользователя
                var myDatas = Console.ReadLine();
                //создать объект с данными, которые были введены
                var obj = new MyHelloWorldMongoThing()
                {
                    MyDatas = myDatas
                };
                //получить монгоклинета, используя строку подключения по умолчанию
                var mongo = new MongoClient();
                //получить(и создать, если не существует) базу данных из монгоклиента
                var db = mongo.GetDatabase("MyHelloWorldDb");
                //получить коллекцию MyHelloWorldMongoThing(и создать, если она не существует)
                //Использование пустого фильтра, чтобы все учитывалось в фильтре
                var collection = db.GetCollection<MyHelloWorldMongoThing>("myHelloWorldCollection");
                //подсчитать предметы в коллекции до вставки
                var count = collection.CountDocuments(new FilterDefinitionBuilder<MyHelloWorldMongoThing>().Empty);
                //добавить введенный элемент в коллекцию
                collection.InsertOne(obj);
                //подсчитать предметы в коллекции после вставки
                count = collection.CountDocuments(new FilterDefinitionBuilder<MyHelloWorldMongoThing>().Empty);

                Console.WriteLine("Received data from MongoDB: ");
                await GetMessages();
                var messages = GetMessages().Result;
                foreach (var message in messages)
                    Console.WriteLine(message.MyDatas);
            }
           
            Console.ReadKey();
        }

        public static async Task<ICollection<MyHelloWorldMongoThing>> GetMessages()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("MyHelloWorldDb");
            var collection = db.GetCollection<MyHelloWorldMongoThing>("myHelloWorldCollection");
            var all = await collection.FindAsync(Builders<MyHelloWorldMongoThing>.Filter.Empty);
            return all.ToList();
        }
    }
}