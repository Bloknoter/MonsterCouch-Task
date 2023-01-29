using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inputs
{
    public class MovementInput : MonoBehaviour
    {
        [SerializeField]
        private Character.Movement.Movement _movement;

        [SerializeField]
        private Camera _targetCamera;

        [SerializeField]
        private bool _enableMouseControl = true;

        private Vector2 _lastMousePosition;

        private bool _isMouseInControl;

        private void Update()
        {
            ReadKeysInput();
            if (_enableMouseControl)
            {
                ReadMouseInput();
                if (_isMouseInControl)
                    _movement.SetTarget(_lastMousePosition);
            }
        }


        private void ReadKeysInput()
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if(direction != Vector2.zero)
                _isMouseInControl = false;

            _movement.Move(direction);
        }

        private void ReadMouseInput()
        {
            if(Input.GetMouseButton(0))
            {
                _lastMousePosition = _targetCamera.ScreenToWorldPoint(Input.mousePosition);
                _isMouseInControl = true;
            }
        }
    }
}
