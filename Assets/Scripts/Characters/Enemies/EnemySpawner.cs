using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Character.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _enemyPrefab;

        [SerializeField]
        private Transform _target;

        [Min(0)]
        [SerializeField]
        private int _amount = 100;

        [SerializeField]
        private Rect _spawnField;

        private void Awake()
        {
            for(int i = 0; i < _amount; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            GameObject enemy = Instantiate(_enemyPrefab);
            Vector2 position = GetRandomPositionFromField();
            enemy.transform.position = position;
            Enemy enemyComponent = enemy.GetComponent<Enemy>();
            enemyComponent.Player = _target;
            enemyComponent.SetWalkingField(_spawnField);
        }

        private Vector2 GetRandomPositionFromField()
        {
            return new Vector2(Random.Range(_spawnField.x, _spawnField.x + _spawnField.width), 
                Random.Range(_spawnField.y, _spawnField.y + _spawnField.height));
        }
    }
}
