using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreEntityFrameworkVsDapper.Dapper
{
    [BenchmarkDotNet.Attributes.Jobs.CoreJob]
    public class CarDapper
    {
        private NpgsqlConnection conn = new NpgsqlConnection("User ID=postgres;Password=yandrakar666;Host=odin;Port=5432;Database=Vehicles;Pooling=true;MinPoolSize=5;MaxPoolSize=20;");

        public CarDapper()
        {
            conn.Open();
        }

        [Benchmark]
        public Car Select()
        {
            return conn.Query<Car>("SELECT * FROM CARS WHERE ID = @Id", new { Id = 1 }).FirstOrDefault();
        }

        [Benchmark]
        public IEnumerable<Car> SelectAll()
        {
            return conn.Query<Car>("SELECT * FROM CARS");
        }

        [Benchmark]
        public void Insert()
        {
            Car car = new Car { Model = "Dapper", Registration = "10000000", Color = "RED" };

            conn.Execute("INSERT INTO CARS (\"Model\", \"Registration\", \"Color\") VALUES (:Model, :Registration, :Color)", car);
        }

        [Benchmark]
        public void InsertAllForEachWay()
        {
            List<Car> cars = Enumerable.Range(20000000, 20001000).Select(i => new Car { Model = "Dapper", Registration = i.ToString(), Color = "RED" }).ToList();

            foreach (Car car in cars)
            {
                conn.Execute("INSERT INTO CARS (\"Model\", \"Registration\", \"Color\") VALUES (:Model, :Registration, :Color)", car);
            }
        }

        [Benchmark]
        public void InsertAllBulkWay()
        {
            List<Car> cars = Enumerable.Range(20000000, 20001000).Select(i => new Car { Model = "Dapper", Registration = i.ToString(), Color = "RED" }).ToList();

            var entries = new { entries = cars.Select(c => String.Format("({0}, {1}, {2})", c.Model, c.Registration, c.Color)) };

            conn.Execute("SELECT * FROM \"INSERT_INTO_CARS\"(:entries::\"CAR_TYPE\")", entries);
        }
    }

    public class Car
    {
        public int Id { get; set; }

        public String Model { get; set; }

        public String Registration { get; set; }

        public String Color { get; set; }
    }
}
