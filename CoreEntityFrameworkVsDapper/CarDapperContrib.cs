using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreEntityFrameworkVsDapper.Dapper
{
    [Table("CARS")]
    public class CarDapperContrib
    {
        public int Id { get; set; }

        public String Model { get; set; }

        public String Registration { get; set; }

        public String Color { get; set; }
    }
}
