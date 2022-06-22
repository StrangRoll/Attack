using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Player _player;

    private Wave _currentWave;
    private int _currentWaveIndex = 0;
    private float _timeAfterLastSpawn;
    private int _spawned;
    private int _killed;

    public event UnityAction AllEnemiesKilled;

    public void NextWave()
    {
        if (_currentWaveIndex + 1 < _waves.Count)
        {
            SetWave(_currentWaveIndex + 1);
        }
        else
        {
            _currentWave = null;
        }
    }

    private void Start()
    {
        SetWave(_currentWaveIndex);
    }

    private void Update()
    {
        if (_currentWave == null)
            return;

        _timeAfterLastSpawn += Time.deltaTime;

        if (_timeAfterLastSpawn >= _currentWave.Delay && _spawned < _currentWave.Count)
        {
            InstantiateEnemy();
            _spawned++;
            _timeAfterLastSpawn = 0;
        }
    }

    private void SetWave(int index)
    {
        _currentWave = _waves[index];
        _currentWaveIndex = index;
    }

    private void InstantiateEnemy()
    {
        Enemy enemy = Instantiate(_currentWave.Template, _spawnPoint.position, _spawnPoint.rotation, _spawnPoint).GetComponent<Enemy>();
        enemy.Init(_player);
        enemy.Dying += OnEnemyDying;
    }

    private void OnEnemyDying(Enemy enemy)
    {
        enemy.Dying -= OnEnemyDying;
        _player.AddMoney(enemy.Reward);
        _killed++;

        if (_killed >= _currentWave.Count)
            AllEnemiesKilled?.Invoke();
    }
}


[System.Serializable]
public class Wave 
{
    public GameObject Template;
    public float Delay;
    public int Count;
}
