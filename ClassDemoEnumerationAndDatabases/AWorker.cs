using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ClassDemoEnumerationAndDatabases
{
    public class AWorker
    {
        private const string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DemoDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        public AWorker()
        {
        }

        public void Start()
        {

            Item item1 = new Item(1, "dims1", StatusType.ToDo, ColourType.Blue);
            Item item2 = new Item(2, "dims2", StatusType.Doing, ColourType.Yellow);

            Console.WriteLine("Print two items");
            Console.WriteLine(item1);
            Console.WriteLine(item2);

            Console.WriteLine($"Item1 direct: {item1.Status} string: {item1.Status.ToString()} int: {(int)item1.Status}");
            Console.WriteLine($"Item2 direct: {item2.Status} string: {item2.Status.ToString()} int: {(int)item2.Status}");


            SaveItem(item1);
            SaveItem(item2);

            PrintGetAll();
            
        }

        private const String SQL_INSERT =
            "insert into Item (ItemName, ItemStatus, ItemColour) values (@Name, @Status, @Colour)";

        private void SaveItem(Item item)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_INSERT, conn);
                cmd.Parameters.AddWithValue("@Name", item.Name);

                // save enum as string
                cmd.Parameters.AddWithValue("@Status", item.Status.ToString());
                // save enum as int
                cmd.Parameters.AddWithValue("@Colour", (int)item.Colour);

                int rows = cmd.ExecuteNonQuery();
                Console.WriteLine("Succeed " + (rows==1));

            }
        }


        private const String SQL_GET_ALL = "select * from Item";
        private void PrintGetAll()
        {
            List<Item> items = new List<Item>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_GET_ALL, conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    Item i = ReadItem(reader);
                    items.Add(i);
                }
            }

            foreach (Item i in items)
            {
                Console.WriteLine(i);
            }

        }

        private Item ReadItem(SqlDataReader reader)
        {
            Item item = new Item();

            item.Id = reader.GetInt32(0);
            item.Name = reader.GetString(1);
            // enum saved as text
            String StatusTxt = reader.GetString(2);
            item.Status = Enum.Parse<StatusType>(StatusTxt);

            // enum saved as int
            int ColourIx = reader.GetInt32(3);
            item.Colour = (ColourType)ColourIx;

            return item;
        }
    }
}