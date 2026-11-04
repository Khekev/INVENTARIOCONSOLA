using UnityEngine;

public class ListaLigada : MonoBehaviour
{
    class Nodo
    {
        public Transform dato;
        public Nodo siguiente;

        public Nodo(Transform valor)
        {
            dato = valor;
            siguiente = null;
        }
    }

    Nodo cabeza;

    public Transform[] waypoints;

    void Start()
    {
        Debug.Log("Lista ligada");

       
        for (int i = 0; i < waypoints.Length; i++)
        {
            AddNodeToLinkedList(waypoints[i], i);
        }

        ShowLinkedList();
    }

    public void ShowLinkedList()
    {
        Debug.Log("Mostrar lista");

        Nodo actual = cabeza;

        if (actual == null)
        {
            Debug.Log("La lista esta vacia");
            return;
        }

        while (actual != null)
        {
            Debug.Log(actual.dato.name);
            actual = actual.siguiente;
        }
    }

    public void ClearLinkList()
    {
        cabeza = null;
        Debug.Log("Lista limpiada");
    }

    public void AddNodeToLinkedList(Transform valor, int posicion)
    {
        if (valor == null)
            return;

        Nodo nuevo = new Nodo(valor);

        if (posicion <= 0 || cabeza == null)
        {
            nuevo.siguiente = cabeza;
            cabeza = nuevo;
        }
        else
        {
            Nodo actual = cabeza;
            int contador = 0;

            while (actual.siguiente != null &&
                   contador < posicion - 1)
            {
                actual = actual.siguiente;
                contador++;
            }

            nuevo.siguiente = actual.siguiente;
            actual.siguiente = nuevo;
        }

        Debug.Log("Nodo agregado: " + valor.name);
    }

    public void RemoveNodeFromLinkedList(int posicion)
    {
        if (cabeza == null || posicion < 0)
        {
            Debug.Log("No se puede eliminar el nodo");
            return;
        }

        if (posicion == 0)
        {
            cabeza = cabeza.siguiente;
            Debug.Log("Primer nodo eliminado");
            return;
        }

        Nodo actual = cabeza;
        int contador = 0;

        while (actual.siguiente != null &&
               contador < posicion - 1)
        {
            actual = actual.siguiente;
            contador++;
        }

        if (actual.siguiente != null)
        {
            actual.siguiente = actual.siguiente.siguiente;
            Debug.Log("Nodo eliminado: " + posicion);
        }
        else
        {
            Debug.Log("Posicion no encontrada");
        }
    }

    public void InvertLinkList()
    {
        Nodo anterior = null;
        Nodo actual = cabeza;

        while (actual != null)
        {
            Nodo siguiente = actual.siguiente;
            actual.siguiente = anterior;
            anterior = actual;
            actual = siguiente;
        }

        cabeza = anterior;

        Debug.Log("Lista invertida");
        ShowLinkedList();
    }

    public Transform GetWaypoint(int posicion)
    {
        Nodo actual = cabeza;
        int contador = 0;

        while (actual != null)
        {
            if (contador == posicion)
                return actual.dato;

            actual = actual.siguiente;
            contador++;
        }

        return null;
    }

    public int GetCount()
    {
        Nodo actual = cabeza;
        int contador = 0;

        while (actual != null)
        {
            contador++;
            actual = actual.siguiente;
        }

        return contador;
    }
}

