using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EditorPanelManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TMP_InputField heigth;
    [SerializeField] private TMP_InputField width;
    [SerializeField] private TMP_InputField depth;
    [SerializeField] private Image mainColorButton;
    [SerializeField] private Image detailButton;
    [SerializeField] private Transform itemHolder;

    [SerializeField] private TextMeshProUGUI heigthRecommended;
    [SerializeField] private TextMeshProUGUI widthRecommended;
    [SerializeField] private TextMeshProUGUI depthRecommended;
    
    public float selectedHeight { get; set; }
    public float selectedWidht { get; set; }
    public float selectedDepth { get; set; }
    private FurnitureModel furnitureModel;

    private void OnEnable()
    {
        furnitureModel = itemHolder.GetChild(0).gameObject.GetComponent<FurnitureModel>();
        initializeAllFields();
        resetFields();
    }
    public void resetFields()
    {
        heigth.text = furnitureModel.editable.initialMeasures[0]* furnitureModel.Specs.scales[0] +"";
        width.text = furnitureModel.editable.initialMeasures[1] * furnitureModel.Specs.scales[1] + "";
        depth.text = furnitureModel.editable.initialMeasures[2] * furnitureModel.Specs.scales[2] + "";
    }
    public void initializeAllFields()
    {

        setMainColor();
        setDetailColor();
        setField(heigthRecommended, 0);
        setField(widthRecommended, 1);
        setField(depthRecommended, 2);
    }
    public void setField(TextMeshProUGUI uwu,int index)
    {
        uwu.text = "("+ furnitureModel.editable.minMeasures[index]+"-"+ furnitureModel.editable.maxMeasures[index] + ")";
    }
    public checkStatus checkAllFields()
    {
        Debug.Log("Resizes:"+heigth.text + "," + width.text + "," + depth.text);
        try
        {
            if(string.IsNullOrWhiteSpace(heigth.text)
                && string.IsNullOrWhiteSpace(width.text) 
                && string.IsNullOrWhiteSpace(depth.text))
            {
                return checkStatus.EMPTY;
            }

            if (!(float.Parse(heigth.text) >= furnitureModel.editable.minMeasures[0] 
                && float.Parse(heigth.text) <= furnitureModel.editable.maxMeasures[0]))
            {
                return checkStatus.ERROR;
            }
            if (!(float.Parse(width.text) >= furnitureModel.editable.minMeasures[1]
                && float.Parse(width.text) <= furnitureModel.editable.maxMeasures[1]))
            {
                return checkStatus.ERROR;
            }
            if (!(float.Parse(depth.text) >= furnitureModel.editable.minMeasures[2]
                && float.Parse(depth.text) <= furnitureModel.editable.maxMeasures[2]))
            {
                return checkStatus.ERROR;
            }
        }
        catch(Exception e)
        {
            return checkStatus.ERROR;
        }
        selectedHeight = float.Parse(heigth.text);
        selectedWidht = float.Parse(width.text);
        selectedDepth = float.Parse(depth.text);
        return checkStatus.OK;
    }
    
    public void setMainColor()
    {

        mainColorButton.color=new Color(furnitureModel.Specs.colors[0, 0], furnitureModel.Specs.colors[0, 1], furnitureModel.Specs.colors[0, 2], furnitureModel.Specs.colors[0, 3]);
    }
    public void setDetailColor()
    {
        if (furnitureModel.Specs.colors.Length>4)
        {
            detailButton.gameObject.SetActive(true);
           
            detailButton.color = new Color(furnitureModel.Specs.colors[1, 0], furnitureModel.Specs.colors[1, 1], furnitureModel.Specs.colors[1, 2], furnitureModel.Specs.colors[1, 3]);
        }
        else
        {
            detailButton.gameObject.SetActive(false);
        }
    }
    public enum checkStatus
    {
        OK,
        ERROR,
        EMPTY
    }
}
