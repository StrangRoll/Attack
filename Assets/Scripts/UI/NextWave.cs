using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NextWave : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Button _nextWaveButton;

    public void OnAllEnemiesKilled()
    {
        _nextWaveButton.gameObject.SetActive(true);
    }

    public void OnNextWaveButtonClick()
    {
        _spawner.NextWave();
        _nextWaveButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _spawner.AllEnemiesKilled += OnAllEnemiesKilled;
        _nextWaveButton.onClick.AddListener(OnNextWaveButtonClick);
    }

    private void OnDisable()
    {
        _spawner.AllEnemiesKilled -= OnAllEnemiesKilled;
        _nextWaveButton.onClick.RemoveListener(OnNextWaveButtonClick);

    }
}
