using UnityEngine;

public class Inventario : MonoBehaviour
{
    string[] hotbar = new string[5];

    void Start()
    {
        Debug.Log("inventario ");

        Objetos(0, "pegelagarto");
        Objetos(1, "agua");
        Objetos(2, "pijama");
        Objetos(3, "comida");
        Objetos(4, "dinero");

       DescribirInventario();

        DescribirObjeto(4);

        DesechadoObjeto(1);

       DescribirInventario();
    }

    void Objetos(int indice, string objeto)
    {
        hotbar[indice] = objeto;
        Debug.Log("Objeto +mas: " + objeto);
    }

    void DescribirObjeto(int indice)
    {
        Debug.Log("espacio " + indice + ": " + hotbar[indice]);
    }

    void DesechadoObjeto(int indice)
    {
        hotbar[indice] = null;
        Debug.Log("espacio " + indice + " vaciado");
    }

    void DescribirInventario()
    {
        Debug.Log("inventario ");

        for (int i = 0; i < hotbar.Length; i++)
        {
            if (hotbar[i] != null)
                Debug.Log("espacio " + i + ": " + hotbar[i]);
            else
                Debug.Log("espacio " + i + ": vaciodo");
        }
    }
}
