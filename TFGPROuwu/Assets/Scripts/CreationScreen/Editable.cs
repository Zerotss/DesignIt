using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Editable : MonoBehaviour
{
    // All editable values
    public Renderer[] renderers { get; set; }
    public List<Color> colors= new List<Color>();
    public List<Color> auxColors = new List<Color>();
    public bool isOnEdit { get; set; }
    public bool isRed { get; set; }
    [field: SerializeField]
    public float[] initialMeasures {  get; set; }
    [field: SerializeField]
    public float[] minMeasures { get; set; }
    [field: SerializeField]
    public float[] maxMeasures { get; set; }


    private int triggerCount = 0;

    public void setup()
    {
        renderers = GetComponentsInChildren<Renderer>();
        setPrefferedSizes(minMeasures, 0.5f, 0.7f);
        setPrefferedSizes(maxMeasures, 2f, 1.3f);
        foreach (var r in renderers)
        {
            colors.Add(r.material.color);
        }
    }
    void setPrefferedSizes(float[] measures,float porcentage, float percentageHeight)
    {
        for (int i = 0; i < initialMeasures.Length; i++)
        {
            if (i == 1)
            {
                measures[i] = initialMeasures[i] * percentageHeight;
            }
            else
            {
                measures[i] = initialMeasures[i] * porcentage;
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        triggerCount++;
        auxColors = new List<Color>(colors);
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (isRed) return;
        
        colors.Clear();
        foreach (var r in renderers)
        {
            colors.Add(r.material.color);
            r.material.color = Color.red;
            isRed = true;
        }
    } 
    private void OnTriggerExit(Collider other)
    {
        triggerCount--;
        if (triggerCount != 0) return;
        isRed = false;
        changeColors(colors,renderers);
        
    }
    public void changeColors(List<Color> colors, Renderer[] renderers) {
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = colors[i];
        }
    }
    public void changeColors(float[,] colors, Renderer[] renderers)
    {
        Debug.Log("length"+renderers.Length);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = new Color(colors[i,0], colors[i,1], colors[i, 2], colors[i, 3]);
            if (i == 1) return;
        }
    }
    public float[,] getAllColorsInArray(Renderer[] renderers)
    {
        float[,] arrayColors = new float[renderers.Length,4];
        for (int i = 0; i < renderers.Length; i++)
        {
            arrayColors[i,0] = renderers[i].material.color.r;
            arrayColors[i,1] = renderers[i].material.color.g;
            arrayColors[i,2] = renderers[i].material.color.b;
            arrayColors[i,3] = renderers[i].material.color.a;
        }
        return arrayColors;
    }


}
