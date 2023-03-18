using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            int res = 0;
            string[] idr = new string[100];
            double[] pricer = new double[100]; 
            for (int i=0; i<100; i++) {
                string jsonText = File.ReadAllText(@"C:\Users\usuario\source\repos\Laboratorio2LuisSanchez\Laboratorio2LuisSanchez\input_challenge_lab_2.jsonl");
                string[] jsonObjects = jsonText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                InputLab input = JsonSerializer.Deserialize<InputLab>(jsonObjects[i])!;
                bool[] Variable = { false };
                //Apartamentos

                if (input.input2.typeBuilder == "Apartments")
                {
                    for (int j = 0; j < input.input1.Length; j++)
                    {
                        if (input.input1[j].builds.Apartments != null)
                        {
                            //Variables necesarios para los apartamentos
                            var id = input.input1[j].builds.Apartments.Select(a => a.id).ToArray();
                            var isPetFriendly = input.input1[j].builds.Apartments.Select(a => a.isPetFriendly).ToArray();
                            var price = input.input1[j].builds.Apartments.Select(a => a.price).ToArray();

                            if (Variable[0] == true)
                            {
                                for (int z = 0; z < input.input1[j].builds.Apartments.Length; z++)
                                {
                                    id[z] = input.input1[j].builds.Apartments[z].id;
                                    isPetFriendly[z] = input.input1[j].builds.Apartments[z].isPetFriendly;
                                    price[z] = input.input1[j].builds.Apartments[z].price;
                                }
                                //Determinar el precio y si tiene mascota
                                for (int k = 0; k < input.input1[j].builds.Apartments.Length; k++)
                                {
                                    if (isPetFriendly[k] == input.input2.wannaPetFriendly && price[k] <= input.input2.budget)
                                    {
                                        idr[res] = id[k];
                                        pricer[res] = price[k];
                                        res++;
                                    }
                                }
                                Variable[0] = false;
                            }
                        }
                    }

                }
                if (input.input2.typeBuilder == "Houses")
                {

                }
                if (input.input2.typeBuilder == "Premises")
                {

                }
            }
        }
    }
}
