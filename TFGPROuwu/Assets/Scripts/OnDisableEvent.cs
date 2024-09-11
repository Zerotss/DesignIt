using UnityEngine;
using UnityEngine.Events;

public class OnDisableEvent : MonoBehaviour
{
    [SerializeField] private UnityEvent _OnDisable;
    private void OnDisable()
    {
        _OnDisable.Invoke();
    }
}
