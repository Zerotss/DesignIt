using UnityEngine;

public class FCP_ExampleScript : MonoBehaviour {

    public bool getStartingColorFromMaterial;
    public FlexibleColorPicker fcp;
    public Material material;

    private void Start() {
        if(getStartingColorFromMaterial)
            fcp.color = material.color;

    }

    private void OnChangeColor(Color co) {
        material.color = co;
    }
}
