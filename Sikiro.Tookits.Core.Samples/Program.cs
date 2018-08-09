using System;
using System.Collections.Generic;
using System.Linq;
using Sikiro.Tookits.Core.Extension;

namespace Sikiro.Tookits.Core.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            var list = new List<User>
            {
                new User {CreateDateTime = DateTime.Now, Id = 1, Name = "chengong"},
                new User {CreateDateTime = DateTime.Now, Id = 2, Name = "chengong"},
                new User {CreateDateTime = DateTime.Now, Id = 3, Name = "chengong"}
            };

            list = list.DistinctBy(a => a.Name).ToList();
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
