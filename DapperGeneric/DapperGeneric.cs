using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DapperGeneric
{
    class DapperGeneric
    {
        static void Main(string[] args)
        {
            var c = new CarManager();

            Console.WriteLine("GetCarByID: 1");
            var s = "Name:" + c.GetCarByID(1).Name.ToString() + " " + "Price:" + c.GetCarByID(1).Price.ToString();
            Console.WriteLine(s);

            Console.WriteLine("\n");

            Console.WriteLine("GetAllCar");
            foreach (Car car in c.GetAllCar())
            {
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
        private static string connStr = @"Server=localhost\SQLEXPRESS;Database=testdb;Trusted_Connection=True;";

        public CarManager() : base(connStr) { }

        public List<Car> GetAllCar()
        {
            var sql = "SELECT * FROM cars";
            return QueryList<Car>(sql);
        }

        public Car GetCarByID(int id)
        {
            var sql = "SELECT * FROM cars WHERE ID = @carID";
            var p = new DynamicParameters();
            p.Add("carID", id, dbType: DbType.Int32);

            return QuerySingle<Car>(sql, p);
        }
    }

    public class DapperBaseRepository : IDisposable
    {

        private readonly IDbConnection conn;

        public DapperBaseRepository(string connectionString)
        {
            conn = new SqlConnection(connectionString);
            conn.Open();
        }
        
        public IEnumerable<T> Query<T>(string query, DynamicParameters parameters = null, CommandType comType = CommandType.Text)
        {
            try
            {

                return conn.Query<T>(query, parameters, commandType: comType).ToList();

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

                return conn.Query<T>(query, parameters, commandType: comType).ToList();

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

                return conn.QueryFirst<T>(query, parameters, commandType: comType);

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

                return conn.QueryFirstOrDefault<T>(query, parameters, commandType: comType);

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

                return conn.QuerySingle<T>(query, parameters, commandType: comType);

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

                return conn.QuerySingleOrDefault<T>(query, parameters, commandType: comType);

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

                conn.Execute(query, parameters, commandType: comType);

            }
            catch (Exception ex)
            {
                //Handle the exception
            }
        }

        public void Dispose()
        {
            conn.Dispose();
        }
    }

}
