using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class CameraMovementBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject FurnitureHolder;
    [SerializeField] private AnimationCurve curvinha;
    [SerializeField] private Transform itemHolder;
    [SerializeField] private GameObject EditablePanel;
    private Coroutine coroutine;
    public UnityEvent moveLeft;
    public UnityEvent moveRight;

    public void MoverCamaraIzquierda()
    {
        if (MovePlane(-90))
        {
            moveLeft.Invoke();
        }
    }

    public void MoverCamaraDerecha()
    {

        if (MovePlane(90)){
            moveRight.Invoke();
        }

    }


    IEnumerator RotatePro(Transform objectToRotate, Vector3 newRotation, float duration)
    {
        float timer = 0;
        Vector3 initRotation = objectToRotate.eulerAngles;
        newRotation = newRotation - initRotation;
        while (true)
        {
            timer += Time.deltaTime;
            objectToRotate.eulerAngles = initRotation + newRotation * curvinha.Evaluate(timer);
            if (timer >= duration)
                break;
            yield return null;
        }
        coroutine = null;
        yield return null;
    }

    public bool MovePlane(float rotationAngle)
    {
        if (coroutine==null)
        {
            coroutine = StartCoroutine(RotatePro(FurnitureHolder.transform, FurnitureHolder.transform.eulerAngles + new Vector3(0, rotationAngle, 0), 0.4f));
            StartCoroutine(RotatePro(plane.transform, plane.transform.eulerAngles + new Vector3(0, rotationAngle, 0), 0.4f));
            if (itemHolder.childCount==0) return true;
            FurnitureModel furnitureModel = itemHolder.GetChild(0).GetComponent<FurnitureModel>();
            if (EditablePanel.activeInHierarchy)
            {
                furnitureModel.Specs.rotations[1] += rotationAngle;
                furnitureModel.onRotationChanaged();
            }
            
            return true;
        }
        return false;


    }
}