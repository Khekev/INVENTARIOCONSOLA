using UnityEngine;

public class ComportamienoA : MonoBehaviour
{
    Vector2 posicionAgente = new Vector2(0, 0);
    Vector2 destino = new Vector2(5, 0);

    void Start()
    {
        Debug.Log("path");
        PathFollowing();

        Debug.Log(" arrival ");
        Arrival();

        Debug.Log(" collision avoidance ");
        CollisionAvoidance();
    }

    void PathFollowing()
    {
        Vector2 direccion = (destino - posicionAgente).normalized;

        Debug.Log("el agente sigue el camino");
        Debug.Log("Direccion: " + direccion);
    }

    void Arrival()
    {
        float distancia = Vector2.Distance(
            posicionAgente, destino);

        if (distancia > 0.5f)
        {
            Debug.Log("el agente sigue avanzando");
        }
        else
        {
            Debug.Log("el agente llego al destino");
        }

        Debug.Log("Distancia: " + distancia);
    }

    void CollisionAvoidance()
    {
        Vector2 obstaculo = new Vector2(2, 0);

        float distancia = Vector2.Distance(
            posicionAgente, obstaculo);

        if (distancia < 3)
        {
            Debug.Log("una roca en tu camino: cambiar direccion");
        }
        else
        {
            Debug.Log("camino despejado");
        }
    }
}
