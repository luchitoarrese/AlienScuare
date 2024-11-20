using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCam : MonoBehaviour
{
    void Start()
    {
        // Buscar la cámara hija automáticamente
        Transform cameraTransform = GetComponentInChildren<Camera>()?.transform;

        if (cameraTransform != null)
        {
            // Desacoplar la cámara del objeto instanciado
            cameraTransform.SetParent(null);
        }
        else
        {
            Debug.LogWarning("No se encontró una cámara como hijo del objeto.");
        }
    }
}
