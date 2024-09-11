using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBehaviour : MonoBehaviour
{
    public void Activate()
    {
        gameObject.SetActive(true);
    }
    public void Desactivate()
    {
        gameObject.SetActive(false);
    }

    public void ActivateGameObject(GameObject reference)
    {
        reference.SetActive(true);
    }
    public void DesactivateGameObject(GameObject reference)
    {
        reference.SetActive(false);
    }

    public static void ActivateGameObjectStatic(GameObject reference)
    {
        reference.SetActive(true);
    }
    public static void DesactivateGameObjectStatic(GameObject reference)
    {
        reference.SetActive(false);
    }
}
