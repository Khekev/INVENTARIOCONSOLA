using UnityEngine;

public class ListaLigada : MonoBehaviour
{
    class Nodo
    {
        public string dato;
        public Nodo siguiente;

        public Nodo(string valor)
        {
            dato = valor;
            siguiente = null;
        }
    }

    Nodo cabeza;

    void Start()
    {
        Debug.Log(" lista ligada ");

        AddNodeToLinkedList("Inicio", 0);
        AddNodeToLinkedList("bosque", 1);
        AddNodeToLinkedList("ciudad", 2);
        AddNodeToLinkedList("desierto", 3);
        AddNodeToLinkedList("Destino", 4);

        ShowLinkedList();

        RemoveNodeFromLinkedList(2);

        ShowLinkedList();

        InvertLinkList();

        ShowLinkedList();

        ClearLinkList();

        ShowLinkedList();
    }

    void ShowLinkedList()
    {
        Debug.Log(" mostrar lista");

        Nodo actual = cabeza;

        if (actual == null)
        {
            Debug.Log("la lista esta vacia");
            return;
        }

        while (actual != null)
        {
            Debug.Log(actual.dato);
            actual = actual.siguiente;
        }
    }

    void ClearLinkList()
    {
        cabeza = null;
        Debug.Log("Lista limpiada");
    }

    void AddNodeToLinkedList(string valor, int posicion)
    {
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

        Debug.Log("Nodo agregado: " + valor);
    }

    void RemoveNodeFromLinkedList(int posicion)
    {
        if (cabeza == null || posicion < 0)
        {
            Debug.Log("no se puede eliminar el nodo");
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
            Debug.Log("Nodo eliminado en posicion: " + posicion);
        }
        else
        {
            Debug.Log("posicion no encontrada");
        }
    }

    void InvertLinkList()
    {
        Nodo anterior = null;
        Nodo actual = cabeza;
        Nodo siguiente = null;

        while (actual != null)
        {
            siguiente = actual.siguiente;
            actual.siguiente = anterior;
            anterior = actual;
            actual = siguiente;
        }

        cabeza = anterior;

        Debug.Log("lista invertida");
    }
}
