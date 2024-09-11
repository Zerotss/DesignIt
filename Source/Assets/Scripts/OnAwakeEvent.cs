using UnityEngine;
using UnityEngine.Events;
public class OnAwakeEvent : MonoBehaviour
{
    [SerializeField] public UnityEvent _OnAwake;
    private void Awake()
    {
        _OnAwake.Invoke();
    }
}
