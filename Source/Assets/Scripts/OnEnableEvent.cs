using UnityEngine;
using UnityEngine.Events;

public class OnEnableEvent : MonoBehaviour
{
    [SerializeField]private UnityEvent _OnEnable;
    private void OnEnable()
    {
        _OnEnable.Invoke();
    }
}
