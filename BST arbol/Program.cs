using System;
using System.Diagnostics;

class Nodo
{
    public string valor;
    public Nodo izquierdo;
    public Nodo derecho;
    public Nodo(string valor)
    {
        this.valor = valor;
        this.izquierdo = null;
        this.derecho = null;
    }
}

class ArbolBinario
{
    public Nodo raiz;
    public ArbolBinario()
    {
        raiz = null;
    }

    public void Insertar(string valor)
    {
        raiz = InsertarRecursivo(raiz, valor);
    }

    private Nodo InsertarRecursivo(Nodo nodo, string valor)
    {
        if (nodo == null)
            return new Nodo(valor);
        
        Console.WriteLine($"\n¿Insertar {valor} a la izquierda o derecha de {nodo.valor}? (I/D)");
        char direccion = Console.ReadKey().KeyChar;
        Console.WriteLine();

        if (char.ToUpper(direccion) == 'I')
            nodo.izquierdo = InsertarRecursivo(nodo.izquierdo, valor);
        else
            nodo.derecho = InsertarRecursivo(nodo.derecho, valor);
        
        return nodo;
    }

    public void MedirTiempoRecorrido(Action<Nodo> metodo)
    {
        Stopwatch sw = Stopwatch.StartNew();
        long inicio = Stopwatch.GetTimestamp();
        metodo(raiz);
        long fin = Stopwatch.GetTimestamp();
        sw.Stop();
        long ticks = fin - inicio;
        Console.WriteLine($"Tiempo transcurrido: {ticks} ticks");
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

    public int CalcularAltura()
    {
        return Altura(raiz);
    }

    private int Altura(Nodo nodo)
    {
        if (nodo == null)
            return 0;
        return 1 + Math.Max(Altura(nodo.izquierdo), Altura(nodo.derecho));
    }
}

class Program
{
    static void Main()
    {
        ArbolBinario arbol = new ArbolBinario();
        bool salir = false;

        while (!salir)
        {
            Console.WriteLine("\nMenú:");
            Console.WriteLine("1. Insertar nodo");
            Console.WriteLine("2. Recorridos del árbol con tiempo");
            Console.WriteLine("3. Calcular altura");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

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
                    arbol.MedirTiempoRecorrido(arbol.PreOrden);
                    Console.WriteLine("\nInOrden:");
                    arbol.MedirTiempoRecorrido(arbol.InOrden);
                    Console.WriteLine("\nPostOrden:");
                    arbol.MedirTiempoRecorrido(arbol.PostOrden);
                    break;
                case "3":
                    Console.WriteLine($"\nAltura del árbol: {arbol.CalcularAltura()}");
                    break;
                case "4":
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }
        }
    }
}
