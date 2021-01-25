using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DapperGeneric_v2
{
    class DapperGeneric_v2
    {
        static void Main(string[] args)
        {
            var c = new CarManager();

            Console.WriteLine("GetCarByID: 1");
            var s = "Name:" + c.GetCarByID(1).Name.ToString() + " " + "Price:" + c.GetCarByID(1).Price.ToString();
            Console.WriteLine(s);

            Console.WriteLine("\n");

            Console.WriteLine("GetAllCar");
            foreach (Car car in c.GetAllCar()) {
                var a = "Name:" + car.Name.ToString() + " " + "Price:" + car.Price.ToString();
                Console.WriteLine(a);
            }

            Console.WriteLine("\npress any key to exit the process...");
            Console.ReadKey();
        }
    }

    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
    }


    public class CarManager : DapperBaseRepository
    {

        public List<Car> GetAllCar()
        {
            var query = "SELECT * FROM cars";
            return QueryList<Car>(query);
        }

        public Car GetCarByID(int id)
        {
            var query = "SELECT * FROM cars WHERE ID = @carID";
            var p = new DynamicParameters();
            p.Add("carID", id, dbType: DbType.Int32);

            return QuerySingle<Car>(query, p);
        }
    }

    public class DapperBaseRepository
    {
        private readonly string connStr = @"Server=localhost\SQLEXPRESS;Database=testdb;Trusted_Connection=True;";

        public IEnumerable<T> Query<T>(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    return conn.Query<T>(query, parameters, commandType: comType);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default;
            }
        }

        public List<T> QueryList<T>(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    return conn.Query<T>(query, parameters, commandType: comType).ToList();
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return new List<T>();
            }
        }

        public T QueryFirst<T>(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    return conn.QueryFirst<T>(query, parameters, commandType: comType);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }

        public T QueryFirstOrDefault<T>(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    return conn.QueryFirstOrDefault<T>(query, parameters, commandType: comType);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }

        public T QuerySingle<T>(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    return conn.QuerySingle<T>(query, parameters, commandType: comType);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }

        public T QuerySingleOrDefault<T>(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    return conn.QuerySingleOrDefault<T>(query, parameters, commandType: comType);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
                return default; //Or however you want to handle the return
            }
        }

        public void Execute(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Execute(query, parameters, commandType: comType);
                }
            }
            catch (Exception ex)
            {
                //Handle the exception
            }
        }
    }
}
