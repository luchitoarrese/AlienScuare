using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform playerTransform; // Referencia al jugador
    public Vector3 offset = new Vector3(0, 0, -10); // Desfase de la c�mara respecto al jugador

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Actualizar la posici�n de la c�mara para seguir al jugador
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, offset.z) + offset;

            // Bloquear la rotaci�n de la c�mara
            transform.rotation = Quaternion.identity;
        }
    }
}
