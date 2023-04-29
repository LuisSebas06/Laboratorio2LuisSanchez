using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Laboratorio3LuisSanchez
{
    class Program
    {
            //Creacion de clases con ayuda de convertidos online
            public class Customer
        {
            public Int64 dpi { get; set; }
            public Int64 budget { get; set; }
            public string date { get; set; }
        }


        public class InputLab1
        {
            public string property { get; set; }
            public List<Customer> customers { get; set; }
            public int rejection { get; set; }
        }

        public class InputLab2
        {
            public Int64 dpi { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string birthDate { get; set; }
            public string job { get; set; }
            public string placeJob { get; set; }
            public Int64 salary { get; set; }
        }
        static void Main(string[] args)
        {

    }
}
}
