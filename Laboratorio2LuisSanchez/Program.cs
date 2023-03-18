using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

public class Program
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
        for (int r = 0; r < 100; r++)
        {
            string jsonText = File.ReadAllText(@"C:\Users\usuario\source\repos\lab2_ED\lab2_ED\input_challenge_lab_2.jsonl");
            string[] jsonObjects = jsonText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            InputLab input = JsonSerializer.Deserialize<InputLab>(jsonObjects[r])!;
            double[] pricee = new double[1000];
            bool[] DeterminarVacio = { false, false, false };
            int ColorD = 0;
            int res = 0;
            string[] idr = new string[1000];


            if (input.input2.typeBuilder == "Apartments")
            {
                        foreach (var item in input.input1)
                        {
                            if (item.builds.Apartments != null)
                            {
                                bool[] PFs = item.builds.Apartments.Select(a => a.isPetFriendly).ToArray();
                                double[] Budgets = item.builds.Apartments.Select(a => a.price).ToArray();

                                for (int i = 0; i < item.builds.Apartments.Length; i++)
                                {
                                    if (PFs[i] == input.input2.wannaPetFriendly && Budgets[i] <= input.input2.budget)
                                    {
                                        idr[res] = item.builds.Apartments[i].id;
                                        pricee[res] = Budgets[i];
                                        res++;
                                    }
                                }
                            }

                        }
                    }
            if (input.input2.typeBuilder == "Houses")
            {

                for (int i = 0; i < input.input1.Length; i++)
                        {
                            if (input.input1[i].builds.Houses == null) { continue; }

                            DeterminarVacio[1] = true;
                            string[] id = new string[input.input1[i].builds.Houses.Length];
                            double[] precio = new double[input.input1[i].builds.Houses.Length];
                            int[] Color = new int[input.input1[i].builds.Houses.Length];

                            for (int j = 0; j < input.input1[i].builds.Houses.Length; j++)
                            {
                                var house = input.input1[i].builds.Houses[j];
                                id[j] = house.id;
                                precio[j] = house.price;

                                // Asignar el número dependiendo del color de zona de riesgo.
                                switch (house.zoneDangerous)
                                {
                                    case "Green":
                                Color[j] = 3;
                                        break;
                                    case "Yellow":
                                Color[j] = 2;
                                        break;
                                    case "Orange":
                                Color[j] = 1;
                                        break;
                                    case "Red":
                                Color[j] = 0;
                                        break;
                                }
                            }

                            for (int j = 0; j < input.input1[i].builds.Houses.Length; j++)
                            {
                                if (Color[j] <= ColorD && precio[j] <= input.input2.budget)
                                {
                                    idr[res] = id[j];
                                    pricee[res] = precio[j];
                                    res++;
                                }
                            }

                    DeterminarVacio[1] = false;
                        }
                    }
            if (input.input2.typeBuilder == "Premises")
            {
                        int totalPremises = 0;
                        foreach (var i in input.input1)
                        {
                            if (i.builds.Premises != null)
                            {
                                totalPremises += i.builds.Premises.Count();
                            }
                        }

                        idr = new string[totalPremises];
                        pricee = new double[totalPremises];
                        res = 0;
                        int index = 0;

                        foreach (var i in input.input1)
                        {
                            if (i.builds.Premises != null)
                            {
                                foreach (var p in i.builds.Premises)
                                {
                                    if (p.commercialActivities.Contains(input.input2.commercialActivity) && p.price <= input.input2.budget)
                                    {
                                        idr[index] = p.id;
                                        pricee[index] = p.price;
                                        index++;
                                        res++;
                                    }
                                }
                            }

                        }
                    }
                    

                    //Ordenar valores usando quicksort
                    void Quicksort(double[] arr, string[] ids, int left, int right)
                    {
                        if (left < right)
                        {
                            int pivotIndex = Partition(arr, ids, left, right);
                            Quicksort(arr, ids, left, pivotIndex - 1);
                            Quicksort(arr, ids, pivotIndex + 1, right);
                        }
                    }

                    int Partition(double[] arr, string[] ids, int left, int right)
                    {
                        double pivotValue = arr[right];
                        int pivotIndex = left - 1;
                        for (int i = left; i < right; i++)
                        {
                            if (arr[i] <= pivotValue)
                            {
                                pivotIndex++;
                                Swap(arr, ids, pivotIndex, i);
                            }
                        }
                        Swap(arr, ids, pivotIndex + 1, right);
                        return pivotIndex + 1;
                    }

                    void Swap(double[] arr, string[] ids, int i, int j)
                    {
                        double tempD = arr[i];
                        string tempS = ids[i];
                        arr[i] = arr[j];
                        ids[i] = ids[j];
                        arr[j] = tempD;
                        ids[j] = tempS;
                    }

                    // Llamar a Quicksort para ordenar los arreglos
                    Quicksort(pricee, idr, 0, res - 1);

                    var respuestaFinal = new StringBuilder("[");
                    for (int i = 0; i < res; i++)
                    {
                        respuestaFinal.Append($"\"{idr[i]}\"");
                        if (i < res - 1)
                        {
                            respuestaFinal.Append(",");
                        }
                    }
                    respuestaFinal.Append("]");

                    Console.WriteLine(respuestaFinal);
            }
        }
    }
