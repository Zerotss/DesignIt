using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adjust : MonoBehaviour
{
    public GameObject plano;
    public Camera camara;
    private void Start()
    {
        CenterPlane();
    }
    public void CenterPlane()
    {
        Debug.Log("Soy viewer de ampeter");
        float diagonal = Mathf.Sqrt(plano.transform.lossyScale.x * plano.transform.lossyScale.x + plano.transform.lossyScale.z * plano.transform.lossyScale.z);
        float orthoSize = diagonal*10f *Screen.height/ Screen.width*0.5f;
        camara.orthographicSize = orthoSize+ orthoSize*0.10f;
        
    }
}
