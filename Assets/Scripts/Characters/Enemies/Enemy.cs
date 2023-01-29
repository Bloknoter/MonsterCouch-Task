using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private Movement.Movement _movement;

        [Min(0)]
        [SerializeField]
        private float _reactionDistance;

        [Min(0)]
        [SerializeField]
        private float _walkingRadius;

        private Transform _owner;

        private Transform _player;

        private SpriteRenderer _enemyRenderer;

        private Rect _walkingField = new Rect(-2, -2, 4, 4);

        private bool _isDead;

        public Transform Player
        {
            get => _player;
            set
            {
                _player = value;
            }
        }

        private void Awake()
        {
            _owner = transform;
            _enemyRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            Vector2 playerOffset = _player.position - _owner.position;
            if (playerOffset.sqrMagnitude <= _reactionDistance * _reactionDistance)
            {
                _movement.Move(-playerOffset);
            }
            else if(!_movement.HasTarget)
            {
                _movement.SetTarget(GetRandomTarget());
            }
        }

        public void SetWalkingField(Rect walkingField)
        {
            _walkingField = walkingField;
        }

        private Vector2 GetRandomTarget()
        {
            return new Vector2(Random.Range(_walkingField.x, _walkingField.x + _walkingField.width),
                Random.Range(_walkingField.y, _walkingField.y + _walkingField.height));
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject == _player.gameObject)
            {
                if (!_isDead)
                    SetupDeath();
            }
        }

        private void SetupDeath()
        {
            _isDead = true;
            _enemyRenderer.color = Color.grey;
            _movement.Stop();
            _movement.enabled = false;
            enabled = false;
        }
    }
}
