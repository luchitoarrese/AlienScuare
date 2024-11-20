using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform playerTransform; // Referencia al jugador
    public Vector3 offset = new Vector3(0, 0, -10); // Desfase de la cámara respecto al jugador

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Actualizar la posición de la cámara para seguir al jugador
            transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, offset.z) + offset;

            // Bloquear la rotación de la cámara
            transform.rotation = Quaternion.identity;
        }
    }
}
