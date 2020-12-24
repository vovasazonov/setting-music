using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sample.Scripts
{
    public class ButtonView : MonoBehaviour, IPointerClickHandler, IButtonView
    {
        public event Action Click;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            CallClick();
        }

        private void CallClick()
        {
            Click?.Invoke();
        }
    }
}