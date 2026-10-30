using System.Collections.Generic;
using UnityEngine;

public class Enemigosowo : MonoBehaviour
{
    List<string> enemigos = new List<string>();

    void Start()
    {
        Debug.Log("enemigosuwu");

        SpawneaEnemigo("chikawa");
        SpawneaEnemigo("usagi");
        SpawneaEnemigo("hachiware");
        SpawneaEnemigo("rakko");

        MostrarEnemigos();

        PetateadoEnemigo("shisa");

        MostrarEnemigos();

        SpawneaEnemigo("momonga");

        MostrarEnemigos();

        Debug.Log("total de lo s enemigos: " + enemigos.Count);
    }

    void SpawneaEnemigo(string enemigo)
    {
        enemigos.Add(enemigo);
        Debug.Log("Enemigo agregado: " + enemigo);
    }

    void PetateadoEnemigo(string enemigo)
    {
        if (enemigos.Remove(enemigo))
        {
            Debug.Log("enemigo petateado: " + enemigo);
        }
        else
        {
            Debug.Log("no se encontro el enemigo");
        }
    }

    void MostrarEnemigos()
    {
        Debug.Log("enemigosowo");

        for (int i = 0; i < enemigos.Count; i++)
        {
            Debug.Log(i + ": " + enemigos[i]);
        }
    }
}
