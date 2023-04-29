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
        class Node
        {
            public int data;
            public int height;
            public Node left;
            public Node right;

            public Node(int data)
            {
                this.data = data;
                this.height = 1;
                this.left = null;
                this.right = null;
            }
        }
        //Creacion del Arbol para tener los datos ordenados

        static void Main(string[] args)
        {
            //Leer json
            string jsonText = File.ReadAllText(@"C:\Users\usuario\source\repos\Lab3LuisSanchez\Lab3LuisSanchez\input_auctions_example_lab_3.jsonl");
            string[] jsonObjects = jsonText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            //Definir el tamaño que va a leer
            for (int r = 0; r < jsonObjects.Length; r++)
            {
                //Deserealizar el json
                InputLab1 input = JsonConvert.DeserializeObject<InputLab1>(jsonObjects[r])!;
                //Definir valor en base a rechazados
                int ValorBuscar = (input.rejection) + 1;
                //Definir el presupuesto
                Int64[] budget = new Int64[input.customers.Count];
                //Definir ciclo for para crear un arreglo de los presupuestos
                for (int i = 0; i < input.customers.Count; i++)
                {
                    budget[i] = input.customers[i].budget;
                }
            }
        }
}
}
