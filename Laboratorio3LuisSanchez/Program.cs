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
        class AVLTree
        {
            private Node root;

            public AVLTree()
            {
                root = null;
            }

            private int Height(Node node)
            {
                if (node == null)
                {
                    return 0;
                }
                return node.height;
            }

            private int BalanceFactor(Node node)
            {
                if (node == null)
                {
                    return 0;
                }
                return Height(node.left) - Height(node.right);
            }

            private Node RotateRight(Node y)
            {
                Node x = y.left;
                Node z = x.right;
                x.right = y;
                y.left = z;
                y.height = 1 + Math.Max(Height(y.left), Height(y.right));
                x.height = 1 + Math.Max(Height(x.left), Height(x.right));
                return x;
            }

            private Node RotateLeft(Node x)
            {
                Node y = x.right;
                Node z = y.left;
                y.left = x;
                x.right = z;
                x.height = 1 + Math.Max(Height(x.left), Height(x.right));
                y.height = 1 + Math.Max(Height(y.left), Height(y.right));
                return y;
            }

            private Node Insert(Node node, int data)
            {
                if (node == null)
                {
                    return new Node(data);
                }
                if (data > node.data)
                {
                    node.right = Insert(node.right, data);
                }
                else if (data < node.data)
                {
                    node.left = Insert(node.left, data);
                }
                else
                {
                    return node; // el valor ya existe en el árbol, no se realiza la inserción
                }
                node.height = 1 + Math.Max(Height(node.left), Height(node.right));
                int balance = BalanceFactor(node);
                if (balance > 1 && data < node.left.data)
                {
                    return RotateRight(node);
                }
                if (balance < -1 && data > node.right.data)
                {
                    return RotateLeft(node);
                }
                if (balance > 1 && data > node.left.data)
                {
                    node.left = RotateLeft(node.left);
                    return RotateRight(node);
                }
                if (balance < -1 && data < node.right.data)
                {
                    node.right = RotateRight(node.right);
                    return RotateLeft(node);
                }
                return node;
            }

            public void InsertArray(Int64[] arr)
            {
                Array.Sort(arr); // Se ordena el arreglo antes de insertar para mejorar el rendimiento.
                root = InsertArrayHelper(arr, 0, arr.Length - 1);
            }

            private Node InsertArrayHelper(Int64[] arr, int start, int end)
            {
                if (start > end)
                {
                    return null;
                }

                int mid = (start + end) / 2;
                Node node = new Node((int)arr[mid]);

                node.left = InsertArrayHelper(arr, start, mid - 1);
                node.right = InsertArrayHelper(arr, mid + 1, end);

                node.height = 1 + Math.Max(Height(node.left), Height(node.right));
                int balance = BalanceFactor(node);

                if (balance > 1 && arr[mid] < node.left.data)
                {
                    return RotateRight(node);
                }
                if (balance < -1 && arr[mid] > node.right.data)
                {
                    return RotateLeft(node);
                }
                if (balance > 1 && arr[mid] > node.left.data)
                {
                    node.left = RotateLeft(node.left);
                    return RotateRight(node);
                }
                if (balance < -1 && arr[mid] < node.right.data)
                {
                    node.right = RotateRight(node.right);
                    return RotateLeft(node);
                }
                return node;
            }
            private Node Delete(Node node, Int64 data)
            {
                if (node == null)
                {
                    return node;
                }
                if (data < node.data)
                {
                    node.left = Delete(node.left, data);
                }
                else if (data > node.data)
                {
                    node.right = Delete(node.right, data);
                }
                else
                {
                    if (node.left == null || node.right == null)
                    {
                        Node temp = null;
                        if (temp == node.left)
                        {
                            temp = node.right;
                        }
                        else
                        {
                            temp = node.left;
                        }
                        if (temp == null)
                        {
                            temp = node;
                            node = null;
                        }
                        else
                        {
                            node = temp;
                        }
                    }
                    else
                    {
                        Node temp = MinimumValue(node.right);
                        node.data = temp.data;
                        node.right = Delete(node.right, temp.data);
                    }
                }
                if (node == null)
                {
                    return node;
                }
                node.height = 1 + Math.Max(Height(node.left), Height(node.right));
                int balance = BalanceFactor(node);
                if (balance > 1 && BalanceFactor(node.left) >= 0)
                {
                    return RotateRight(node);
                }
                if (balance > 1 && BalanceFactor(node.left) < 0)
                {
                    node.left = RotateLeft(node.left);
                    return RotateRight(node);
                }
                if (balance < -1 && BalanceFactor(node.right) <= 0)
                {
                    return RotateLeft(node);
                }
                if (balance < -1 && BalanceFactor(node.right) > 0)
                {
                    node.right = RotateRight(node.right);
                    return RotateLeft(node);
                }
                return node;
            }

            private Node MinimumValue(Node node)
            {
                Node current = node;
                while (current.left != null)
                {
                    current = current.left;
                }
                return current;
            }

            public void DeleteValue(Int64 data)
            {
                root = Delete(root, data);
            }

            private void Traverse(Node node)
            {
                if (node == null)
                {
                    return;
                }
                Traverse(node.right); // primero el subárbol derecho
                Console.Write(node.data + " ");
                Traverse(node.left); // luego el subárbol izquierdo
            }

            public void PrintInOrder()
            {
                Traverse(root);
            }

            //Busqueda de arbol
            private int positionCounter = 0;

            private Node FindDataByPosition(Node node, int position)
            {
                if (node == null)
                {
                    return null;
                }

                Node right = FindDataByPosition(node.right, position);
                if (right != null)
                {
                    return right;
                }

                positionCounter++;
                if (positionCounter == position)
                {
                    return node;
                }

                Node left = FindDataByPosition(node.left, position);
                if (left != null)
                {
                    return left;
                }

                return null;
            }

            public int FindDataByPosition(int position)
            {
                positionCounter = 0;
                Node result = FindDataByPosition(root, position);
                if (result != null)
                {
                    return result.data;
                }
                throw new ArgumentException("La posición especificada está fuera de rango");
            }
        }
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
                //Se relaciona el arbol con el array del presupuesto
                AVLTree tree = new AVLTree();
                tree.InsertArray(budget);
                //Se declara variable para el valor a buscar en base a los rechazados en el arbol
                Int64 valorEncontrado = tree.FindDataByPosition(ValorBuscar);
                //Se declaran variables de los inputs los cuales nos serviran para armar la informacion
                Int64 DPIEncontrado = 0;
                string DateEncontrado = "";
                string firstName = "";
                string lastName = "";
                string job = "";
                string placeJob = "";
                Int64 salary = 0;
                string Date = "";
                string property = "";
                string signature = "";

                //Arreglo para los DPI
                Int64[] DPI = new Int64[input.customers.Count];
                //Ciclo for para recorrer todos los clientes
                for (int i = 0; i < input.customers.Count; i++)
                {
                    //Numero especifico del dpi del cliente
                    DPI[i] = input.customers[i].dpi;
                    //Comparativa para determinar si el presupuesto ganador es igual al del json para sacar el dpi
                    if (valorEncontrado == input.customers[i].budget)
                    {
                        //Dpi encontrado se guarda
                        DPIEncontrado = DPI[i];
                    }
                }
                // Leer json2
                string jsonText2 = File.ReadAllText(@"C:\Users\usuario\source\repos\Lab3LuisSanchez\Lab3LuisSanchez\input_customer_example_lab_3.jsonl");
                string[] jsonObjects2 = jsonText2.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                // Recorrer el json
                for (int p = 0; p < jsonObjects2.Length; p++)
                {
                    //Deserealizar json2
                    InputLab2 input2 = JsonConvert.DeserializeObject<InputLab2>(jsonObjects2[p])!;
                    //Crear un arreglo de Dpi del json2
                    Int64[] DPI2 = new Int64[jsonObjects2.Length];
                    //Recorrer el input
                    for (int i = 0; i < jsonObjects2.Length; i++)
                    {
                        //Obtener dpi
                        DPI2[i] = input2.dpi;
                        //Se compara el dpi que se encontro en el anterior json con el json2
                        if (DPIEncontrado == input2.dpi)
                        {
                            //Se nombran variables y se le agregan los datos dependiendo los datos obtenidos
                            DateEncontrado = input2.birthDate;
                            firstName = input2.firstName;
                            lastName = input2.lastName;
                            job = input2.job;
                            placeJob = input2.placeJob;
                            salary = input2.salary;
                            property = input.property;
                            Date = input.customers[0].date;
                            //Aplicando Hash para encriptar el DPI
                            string DPIHash = DPI2[i].ToString();
                            MD5 md5 = MD5.Create();
                            byte[] hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(DPIHash));

                            StringBuilder sb = new StringBuilder();
                            for (int j = 0; j < hashBytes.Length; j++)
                            {
                                sb.Append(hashBytes[j].ToString("x2"));
                            }
                            signature = sb.ToString();

                        }
                    }
                }

                // Imprimir la línea con todos los valores requeridos del cliente
                Console.WriteLine("{\"dpi\":" + DPIEncontrado + ",\"budget\":" + valorEncontrado + ",\"date\":\"" + Date + "\",\"firstName\":\"" + firstName + "\",\"lastName\":\"" + lastName + "\",\"birthDate\":\"" + DateEncontrado + "\",\"job\":\"" + job + "\",\"placeJob\":\"" + placeJob + "\",\"salary\":" + salary + ",\"property\":\"" + property + "\",\"signature\":\"" + signature + "\"}");
            }
        }
    }
}