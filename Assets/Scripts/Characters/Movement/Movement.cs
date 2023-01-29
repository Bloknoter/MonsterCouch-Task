using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character.Movement
{
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody2D _ownerRb;

        [Min(0)]
        [SerializeField]
        private float _speed = 1f;

        private Transform _owner;

        private Vector2 _direction;

        private bool _updateMovement;

        private Vector2 _targetPosition;

        private bool _hasTarget;


        public bool HasTarget => _hasTarget;


        private void Awake()
        {
            _owner = _ownerRb.gameObject.transform;
        }

        private void FixedUpdate()
        {
            if(_updateMovement)
            {
                _updateMovement = false;
                Vector2 movement = _direction * _speed * Time.fixedDeltaTime;
                _ownerRb.MovePosition((Vector2)_owner.position + movement);
            }

            if(_hasTarget)
            {
                MoveTowardsTarget();
            }
        }

        public void Move(Vector2 direction)
        {
            _direction = direction.normalized;
            _hasTarget = false;
            _updateMovement = true;
        }

        public void SetTarget(Vector2 targetPosition)
        {
            _hasTarget = true;
            _targetPosition = targetPosition;
        }

        public void Stop()
        {
            _hasTarget = false;
            _updateMovement = false;

            _targetPosition = _owner.position;
            _direction = Vector2.zero;
        }

        private void MoveTowardsTarget()
        {
            _direction = _targetPosition - (Vector2)_owner.position;

            if(_direction.sqrMagnitude > Mathf.Pow(_speed * Time.fixedDeltaTime, 2))
            {
                _ownerRb.MovePosition((Vector2)_owner.position + _direction.normalized * _speed * Time.fixedDeltaTime);
            }
            else
            {
                _ownerRb.MovePosition(_targetPosition);
                _hasTarget = false;
            }
        }
    }
}
