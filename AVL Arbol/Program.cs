using System;
using System.Diagnostics;

//Definimos la clase nodo para el Arbol AVL
class Nodo
{
    public string valor;
    public Nodo izquierdo;
    public Nodo derecho;
    public int altura;

//Constructor de la clase nodo
    public Nodo(string valor)
    {
        this.valor = valor;
        this.izquierdo = null;
        this.derecho = null;
        this.altura = 1; // Inicia altura en 1
    }
}
//Clase para el arbol AVL
class ArbolAVL
{
    public Nodo raiz;

    public ArbolAVL()
    {
        raiz = null;
    }
//Metodo public para insertar valores en el arbol.
    public void Insertar(string valor)
    {
        Stopwatch sw = Stopwatch.StartNew();// INICIO DEL CRONOMETRO
        raiz = InsertarRecursivo(raiz, valor);
        sw.Stop();// para el cronomentro
        Console.WriteLine($"Tiempo de inserción: {sw.ElapsedTicks} ticks");
    }
//Metodo privado para insertar un nodo
    private Nodo InsertarRecursivo(Nodo nodo, string valor)
    {
        if (nodo == null)
            return new Nodo(valor);

        if (string.Compare(valor, nodo.valor) < 0)
            nodo.izquierdo = InsertarRecursivo(nodo.izquierdo, valor);
        else
            nodo.derecho = InsertarRecursivo(nodo.derecho, valor);

        nodo.altura = 1 + Math.Max(ObtenerAltura(nodo.izquierdo), ObtenerAltura(nodo.derecho));

        return Balancear(nodo);
    }
//Se actualiza la altura del nodo
    private int ObtenerAltura(Nodo nodo)
    {
        //se balance el arbol
        return nodo?.altura ?? 0;
    }
//Metodo para obtener el factor de un nodo
    private int ObtenerFactorBalance(Nodo nodo)
    {
        return nodo == null ? 0 : ObtenerAltura(nodo.izquierdo) - ObtenerAltura(nodo.derecho);
    }
// Rotacion a la derecha
    private Nodo RotacionDerecha(Nodo y)
    {
        Nodo x = y.izquierdo;
        Nodo temp = x.derecho;

        x.derecho = y;
        y.izquierdo = temp;
        // Actualzia alturas
        y.altura = 1 + Math.Max(ObtenerAltura(y.izquierdo), ObtenerAltura(y.derecho));
        x.altura = 1 + Math.Max(ObtenerAltura(x.izquierdo), ObtenerAltura(x.derecho));

        return x;
    }
//Rotacion a la izquierda
    private Nodo RotacionIzquierda(Nodo x)
    {
        Nodo y = x.derecho;
        Nodo temp = y.izquierdo;

        y.izquierdo = x;
        x.derecho = temp;

        x.altura = 1 + Math.Max(ObtenerAltura(x.izquierdo), ObtenerAltura(x.derecho));
        y.altura = 1 + Math.Max(ObtenerAltura(y.izquierdo), ObtenerAltura(y.derecho));

        return y;
    }
// Metodo para balancer el nodo despues de la incersion 
    private Nodo Balancear(Nodo nodo)
    {
        int factorBalance = ObtenerFactorBalance(nodo);
        //Caso de desbalance a la izquierda
        if (factorBalance > 1 && ObtenerFactorBalance(nodo.izquierdo) >= 0)
            return RotacionDerecha(nodo);

        //Caso de desbalance a la derecha

        if (factorBalance < -1 && ObtenerFactorBalance(nodo.derecho) <= 0)
            return RotacionIzquierda(nodo);
        //Caso de desbalance izquierda y derecha 
        if (factorBalance > 1 && ObtenerFactorBalance(nodo.izquierdo) < 0)
        {
            nodo.izquierdo = RotacionIzquierda(nodo.izquierdo);
            return RotacionDerecha(nodo);
        }
            //Caso de desbalance derecha izquierda
        if (factorBalance < -1 && ObtenerFactorBalance(nodo.derecho) > 0)
        {
            nodo.derecho = RotacionDerecha(nodo.derecho);
            return RotacionIzquierda(nodo);
        }

        return nodo;
    }
//Metodos del recorrido del  ALV
    public void MedirRecorrido(Action<Nodo> recorrido, string tipo)
    {
        Stopwatch sw = Stopwatch.StartNew();
        recorrido(raiz);
        sw.Stop();
        Console.WriteLine($"\nTiempo de {tipo}: {sw.ElapsedTicks} ticks");
    }

    public void PreOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            Console.Write(nodo.valor + " ");
            PreOrden(nodo.izquierdo);
            PreOrden(nodo.derecho);
        }
    }

    public void InOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            InOrden(nodo.izquierdo);
            Console.Write(nodo.valor + " ");
            InOrden(nodo.derecho);
        }
    }

    public void PostOrden(Nodo nodo)
    {
        if (nodo != null)
        {
            PostOrden(nodo.izquierdo);
            PostOrden(nodo.derecho);
            Console.Write(nodo.valor + " ");
        }
    }

    public void CalcularAltura()
    {
        Stopwatch sw = Stopwatch.StartNew();
        int altura = ObtenerAltura(raiz);
        sw.Stop();
        Console.WriteLine($"\nAltura del árbol: {altura}");
        Console.WriteLine($"Tiempo de cálculo de altura: {sw.ElapsedTicks} ticks");
    }
// metodo para imprimir el Arbol
    public void ImprimirArbol()
    {
        Stopwatch sw = Stopwatch.StartNew();
        ImprimirRecursivo(raiz, 0);
        sw.Stop();
        Console.WriteLine($"\nTiempo de impresión: {sw.ElapsedTicks} ticks");
    }

    private void ImprimirRecursivo(Nodo nodo, int nivel)
    {
        if (nodo != null)
        {
            ImprimirRecursivo(nodo.derecho, nivel + 1);
            Console.WriteLine(new string(' ', nivel * 4) + nodo.valor);
            ImprimirRecursivo(nodo.izquierdo, nivel + 1);
        }
    }
}
//Programa con el menu de opcione
class Program
{
    static void Main()
    {
        ArbolAVL arbol = new ArbolAVL();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Insertar el nodo");
            Console.WriteLine("2. Recorridos del árbol");
            Console.WriteLine("3. Calcular altura");
            Console.WriteLine("4. Imprimir árbol gráficamente");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una de las opción: ");

            string opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el valor del nodo: ");
                    string valor = Console.ReadLine();
                    arbol.Insertar(valor);
                    break;
                case "2":
                    Console.WriteLine("\nPreOrden:");
                    arbol.MedirRecorrido(arbol.PreOrden, "PreOrden");
                    Console.WriteLine("\nInOrden:");
                    arbol.MedirRecorrido(arbol.InOrden, "InOrden");
                    Console.WriteLine("\nPostOrden:");
                    arbol.MedirRecorrido(arbol.PostOrden, "PostOrden");
                    break;
                case "3":
                    arbol.CalcularAltura();
                    break;
                case "4":
                    Console.WriteLine("\nÁrbol impreso:");
                    arbol.ImprimirArbol();
                    break;
                case "5":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}

