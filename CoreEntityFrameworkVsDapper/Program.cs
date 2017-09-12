using BenchmarkDotNet.Running;
using Npgsql;
using System;

namespace CoreEntityFrameworkVsDapper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var sum = BenchmarkRunner.Run<Dapper.CarDapper>();
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
