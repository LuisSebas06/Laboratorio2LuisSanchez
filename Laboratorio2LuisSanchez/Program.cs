using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;

namespace Laboratorio2LuisSanchez
{
    class Program
    {
        public class House
        {
            public string zoneDangerous { get; set; }
            public string address { get; set; }
            public double price { get; set; }
            public string contactPhone { get; set; }
            public string id { get; set; }
        }
        public class Apartment
        {
            public bool isPetFriendly { get; set; }
            public string address { get; set; }
            public double price { get; set; }
            public string contactPhone { get; set; }
            public string id { get; set; }
        }
        public class Premise
        {
            public string[] commercialActivities { get; set; }
            public string address { get; set; }
            public double price { get; set; }
            public string contactPhone { get; set; }
            public string id { get; set; }
        }
        public class Builds
        {
            public Premise[]? Premises { get; set; }
            public Apartment[]? Apartments { get; set; }
            public House[]? Houses { get; set; }
        }
        public class Input1
        {
            public Dictionary<string, bool> services { get; set; }
            public Builds builds { get; set; }

        }
        public class Input2
        {
            public double budget { get; set; }
            public string typeBuilder { get; set; }
            public string[] requiredServices { get; set; }
            public string? commercialActivity { get; set; }
            public bool? wannaPetFriendly { get; set; }
            public string? minDanger { get; set; }
        }
        public class InputLab
        {
            public Input1[] input1 { get; set; }
            public Input2 input2 { get; set; }
        }
        static void Main(string[] args)
        {
            for () {
                string jsonText = File.ReadAllText(@"C:\Users\usuario\source\repos\Laboratorio2LuisSanchez\Laboratorio2LuisSanchez\input_lab_2_example");

            }
        }
    }
}
