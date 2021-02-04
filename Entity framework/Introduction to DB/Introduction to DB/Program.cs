using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace Introduction_to_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=.; Database=Minions; Integrated Security=true;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            using (sqlConnection)
            {
                string sqlComand = @"SELECT REPLACE (v.Name,CHAR(9),'') AS Name,COUNT(MinionId) AS Count FROM MinionsVillains AS m
                                        JOIN Villains AS v ON
                                         m.VillainId=v.Id
                                        GROUP BY (Name)
                                    
                    ";
                SqlCommand command = new SqlCommand(sqlComand, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                DataTable dataTable = reader.GetSchemaTable();
                Console.WriteLine(string.Join(' ', dataTable.Rows));
                while (reader.Read())
                {
                    string villainName = (string)reader["Name"];
                    int count = (int)reader["Count"];
                    Console.Write(villainName + "  " + count);


                }
            }





            //sqlConnection.Open();
            //using (sqlConnection)
            //{
            //    int id = int.Parse(Console.ReadLine());
            //    string sqlComand = @"SELECT m.Name  FROM MinionsVillains AS mv
            //                            JOIN Minions AS m ON
            //                             mv.MinionId=m.Id
            //                             WHERE mv.VillainId=@Id
                                       
                                    
            //        ";
               
            //    SqlCommand command = new SqlCommand(sqlComand, sqlConnection);
            //    command.Parameters.AddWithValue(@"Id", id);
            //    SqlDataReader reader = command.ExecuteReader();
            //    DataTable dataTable = reader.GetSchemaTable();

            //    while (reader.Read())
            //    {
            //        string villainName = (string)reader["Name"];
            //        int count = (int)reader["Count"];
            //        Console.Write(villainName + "  " + count);


            //    }


            //}
        }
    }
}
