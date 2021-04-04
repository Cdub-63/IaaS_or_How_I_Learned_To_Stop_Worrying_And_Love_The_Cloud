namespace Azure_LearningActivity_CosmosDB
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Azure.Documents.Client;
    using Newtonsoft.Json;

    public class Order
    {
        public string Partition { get; set; }
        public int OrderId { get; set; }
        public Address Address { get; set; }
        public List<OrderLineItem> OrderLineItems { get; set; }
    }
    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
    }
    public class OrderLineItem
    {
        public int OrderLineItemId { get; set; }
        public int Price { get; set; }
    }

    class Program
    {
        static string EndpointUrl = "";
        static string PrimaryKey = "";
        static string databaseName = "db1";
        static string collectionName = "collection1";

        static Uri collectionUri = 
            UriFactory.CreateDocumentCollectionUri(
                databaseName, 
                collectionName);

        static DocumentClient client =
            new DocumentClient(new Uri(EndpointUrl), PrimaryKey);

        static Order order1 = new Order()
        {
            Partition = "test",
            OrderId = 101,
            Address = new Address()
            {
                City = "Keller",
                State = "Texas"
            },
            OrderLineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OrderLineItemId = 200,
                        Price = 55
                    },
                    new OrderLineItem()
                    {
                        OrderLineItemId = 201,
                        Price = 299
                    },
                }
        };
        static Order order2 = new Order()
        {
            Partition = "test",
            OrderId = 102,
            Address = new Address()
            {
                City = "Seattle",
                State = "Washington"
            },
            OrderLineItems = new List<OrderLineItem>()
                {
                    new OrderLineItem()
                    {
                        OrderLineItemId = 202,
                        Price = 42
                    },
                    new OrderLineItem()
                    {
                        OrderLineItemId = 203,
                        Price = 183
                    },
                }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Saving Documents...");
            SaveDocuments();

            Console.WriteLine("Getting the saved Orders by SQL Query + JOIN...");
            foreach (var item in GetAllOrdersWithSQLQuery())
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }

            Console.WriteLine("Getting the saved OrderLineItems by SQL Query + JOIN...");
            foreach (var item in GetOrderLineItemsWithSQLQuery())
            {
                Console.WriteLine(JsonConvert.SerializeObject(item));
            }

            Console.WriteLine("Press any key to exit the learning activity application.");
            Console.ReadLine();
        }

        static async void SaveDocuments()
        {
            await client.CreateDocumentAsync(collectionUri, order1);
            await client.CreateDocumentAsync(collectionUri, order2);
        }

        static List<Order> GetAllOrdersWithSQLQuery()
        {
            var query = @"SELECT * FROM Orders o ORDER BY o.Address.City ASC";

            var orders = client.CreateDocumentQuery<Order>(
                collectionUri, 
                query, 
                feedOptions: new FeedOptions() { EnableCrossPartitionQuery = true })
            .ToList();
            return orders;
        }

        static List<OrderLineItem> GetOrderLineItemsWithSQLQuery()
        {
            var query = @"SELECT li.OrderLineItemId, li.Price
                FROM Orders o
                JOIN li IN o.OrderLineItems
                WHERE li.Price > 60
                ORDER BY o.Address.City ASC";

            var orders = client.CreateDocumentQuery<OrderLineItem>(
                collectionUri, 
                query, 
                feedOptions: new FeedOptions() { EnableCrossPartitionQuery = true })
            .ToList();
            return orders;
        }
    }
}
