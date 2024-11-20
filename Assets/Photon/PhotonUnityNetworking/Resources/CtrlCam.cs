using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCam : MonoBehaviour
{
    void Start()
    {
        // Buscar la c�mara hija autom�ticamente
        Transform cameraTransform = GetComponentInChildren<Camera>()?.transform;

        if (cameraTransform != null)
        {
            // Desacoplar la c�mara del objeto instanciado
            cameraTransform.SetParent(null);
        }
        else
        {
            Debug.LogWarning("No se encontr� una c�mara como hijo del objeto.");
        }
    }
}
