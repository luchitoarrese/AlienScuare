using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiManager : MonoBehaviour
{
    private void Awake()
    {
        // Contar todos los AudioListeners en la escena
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

        // Si hay más de uno, desactivar todos excepto el primero
        if (listeners.Length > 1)
        {
            for (int i = 1; i < listeners.Length; i++)
            {
                listeners[i].gameObject.SetActive(false);
            }
        }
    }
}
