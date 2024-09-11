using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FurnitureModel : MonoBehaviour
{
    [field: SerializeField]
    public String Name { get; set; }
    [field: SerializeField]
    public Sprite Image { get; set; }
    [field: SerializeField]
    public FurnitureType Type { get; set; }
    [field: SerializeField]
    public FurnitureSpecs Specs { get; set; }
    public Editable editable { get; set; }

    private void Awake()
    {

        editable = GetComponentInChildren<Editable>();
        editable.setup();
        Specs.colors = editable.getAllColorsInArray(editable.renderers);

    }
    private void Start()
    {
       
    }
    public void onRotationChanaged()
    {

        gameObject.transform.localEulerAngles= new Vector3(Specs.rotations[0], Specs.rotations[1], Specs.rotations[2]);
    }
    public void onPositionsChanged()
    {
        gameObject.transform.localPosition = new Vector3(Specs.positions[0], Specs.positions[1], Specs.positions[2]);

    }
    public void onScaleChanged()
    {

       gameObject.transform.localScale = new Vector3(Specs.scales[0], Specs.scales[1], Specs.scales[2]);

    }
    public void onColorsChanged()
    {
        Debug.Log(editable.renderers[0]);
        editable.changeColors(Specs.colors, editable.renderers) ;

    }
}//Each color is a float[4]
[Serializable]
public class FurnitureSpecs
{
    public float[] positions;
    public float[] rotations;
    public float[,] colors;                     
    public float[] scales;
    [field:SerializeField]
    public FurnitureRenderer renderer { get; set; }
    public override string ToString()
    {
        string positionsStr = ArrayToString(positions);
        string rotationsStr = ArrayToString(rotations);
        string colorsStr = Array2DToString(colors);
        string scalesStr = ArrayToString(scales);

        return $"Positions: {positionsStr}, Rotations: {rotationsStr}, Colors: {colorsStr}, Scales: {scalesStr}, Renderer: {renderer}";
    }

    private string ArrayToString(float[] array)
    {
        return array != null ? string.Join(", ", array) : "null";
    }

    private string Array2DToString(float[,] array)
    {
        if (array == null)
            return "null";

        string result = "[";
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result += array[i, j];
                if (j < cols - 1)
                    result += ", ";
            }
            if (i < rows - 1)
                result += "; ";
        }

        result += "]";
        return result;
    }
}
public enum FurnitureRenderer
{
    BATHTUB,
    BED,
    BOOKSHELF,
    CHAIR,
    MINICOUCH,
    BIGCOUCH,
    LAMP,
    DRAWER,
    FRIDGE,
    KITCHENBANK,
    OVEN,
    TABLE,
    WC,
    WCSINK
}
public enum FurnitureType
{
    LIVING_ROOM,
    KITCHEN,
    WC,
    BEDROOM
}
