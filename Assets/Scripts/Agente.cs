using UnityEngine;
using UnityEngine.InputSystem;

public class Agente : MonoBehaviour
{
    [Header("Lista de waypoints")]
    public ListaLigada lista;

    [Header("Movimiento")]
    public float velocidad = 3f;
    public float distanciaLlegada = 0.15f;
    public float distanciaFrenado = 1.2f;

    [Header("Evitar obstaculos")]
    public LayerMask capaObstaculos;
    public float radioDeteccion = 0.25f;
    public float distanciaDeteccion = 0.8f;

    int puntoActual = 0;
    bool movimiento = false;

    void Update()
    {
        
        if (Keyboard.current != null &&
            Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            IniciarMovimiento();
        }

        
        if (Keyboard.current != null &&
            Keyboard.current.rKey.wasPressedThisFrame)
        {
            CambiarRuta();
        }
    }

    void FixedUpdate()
    {
        if (!movimiento)
            return;

        if (lista == null || lista.GetCount() == 0)
        {
            movimiento = false;
            Debug.Log("No hay waypoints");
            return;
        }

        Transform objetivo = lista.GetWaypoint(puntoActual);

        if (objetivo == null)
            return;

        Vector2 posicion = transform.position;
        Vector2 diferencia = (Vector2)objetivo.position - posicion;
        float distancia = diferencia.magnitude;

        if (distancia <= distanciaLlegada)
        {
            Debug.Log("Punto alcanzado: " + objetivo.name);

            if (puntoActual >= lista.GetCount() - 1)
            {
                movimiento = false;
                Debug.Log("Agente detenido: ultimo nodo alcanzado");
            }
            else
            {
                puntoActual++;
            }

            return;
        }

        Vector2 direccion = diferencia.normalized;

     
        float velocidadActual = velocidad;

        if (distancia < distanciaFrenado)
        {
            velocidadActual *= Mathf.Clamp01(
                distancia / distanciaFrenado
            );
        }


        RaycastHit2D obstaculo = Physics2D.CircleCast(
            posicion,
            radioDeteccion,
            direccion,
            distanciaDeteccion,
            capaObstaculos
        );

        Vector2 direccionMovimiento = direccion;

 
        if (obstaculo.collider != null)
        {
            Vector2 lateral = new Vector2(
                -obstaculo.normal.y,
                obstaculo.normal.x
            );

            if (Vector2.Dot(lateral, direccion) < 0)
            {
                lateral = -lateral;
            }

            direccionMovimiento = lateral;
        }

   
        transform.position = posicion +
            direccionMovimiento * velocidadActual * Time.fixedDeltaTime;
    }

    public void IniciarMovimiento()
    {
        if (lista == null || lista.GetCount() == 0)
        {
            Debug.Log("No hay ruta para seguir");
            return;
        }

        puntoActual = 0;
        movimiento = true;

        Debug.Log("Agente en movimiento");
    }

    public void CambiarRuta()
    {
        if (lista == null)
            return;

        lista.InvertLinkList();

        puntoActual = 0;
        movimiento = true;

        Debug.Log("Cambio de ruta");
    }
}