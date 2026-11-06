
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
        if (Keyboard.current == null)
            return;

       
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            IniciarMovimiento();
        }

        
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            CambiarRuta();
        }

       
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            lista.AgregarNodo();
        }

        
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            lista.EliminarNodo();
        }
    }

    void FixedUpdate()
    {
        if (!movimiento)
            return;

        if (lista == null || lista.GetCount() == 0)
        {
            movimiento = false;
            return;
        }

        Transform objetivo = lista.GetWaypoint(puntoActual);

        if (objetivo == null)
            return;

        Vector2 posicion = transform.position;
        Vector2 destino = objetivo.position;

        float distancia = Vector2.Distance(posicion, destino);

        if (distancia <= distanciaLlegada)
        {
            transform.position = destino;

            Debug.Log("Punto alcanzado: " + objetivo.name);

            if (puntoActual >= lista.GetCount() - 1)
            {
                movimiento = false;
                Debug.Log("¡ULTIMO NODO ALCANZADO!");
            }
            else
            {
                puntoActual++;
            }

            return;
        }

        Vector2 direccion = (destino - posicion).normalized;

        
        float velocidadActual = velocidad;

        if (distancia < distanciaFrenado)
        {
            velocidadActual = Mathf.Max(
                velocidad * 0.3f,
                velocidad * (distancia / distanciaFrenado)
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
            Debug.Log("¡OBSTACULO DETECTADO!");

            Vector2 derecha = new Vector2(
                -direccion.y,
                direccion.x
            );

            Vector2 izquierda = -derecha;

            bool derechaLibre = !Physics2D.CircleCast(
                posicion,
                radioDeteccion,
                derecha,
                distanciaDeteccion,
                capaObstaculos
            );

            bool izquierdaLibre = !Physics2D.CircleCast(
                posicion,
                radioDeteccion,
                izquierda,
                distanciaDeteccion,
                capaObstaculos
            );

            if (derechaLibre)
                direccionMovimiento = derecha;
            else if (izquierdaLibre)
                direccionMovimiento = izquierda;
            else
            {
                Debug.Log("No hay espacio para rodear el obstaculo");
                return;
            }
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
