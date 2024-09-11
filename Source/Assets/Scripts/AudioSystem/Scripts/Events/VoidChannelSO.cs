using System;
using UnityEngine;
namespace Xeiv.AudioSystem
{
    [CreateAssetMenu(menuName = "Systems/AudioSystem/Events/VoidChannel")]
    public class VoidChannelSO : ScriptableObject
    {
        public Action OnEventCall;

        public void CallEvent()
        {
            OnEventCall?.Invoke();
        }
    }
}
