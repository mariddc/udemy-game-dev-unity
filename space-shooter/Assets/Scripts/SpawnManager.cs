using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab, _enemyContainer;
    [SerializeField] private GameObject[] powerups;
    [SerializeField] private float _spawnEnemyDelay = 5.0f;
    private bool _stopSpawning = false;

    // Start is called before the first frame update
    public void StartSpawning()
    {
        IEnumerator _spawnEnemyRoutine = SpawnEnemyRoutine(_spawnEnemyDelay);
        StartCoroutine(_spawnEnemyRoutine);
        StartCoroutine(SpawnPowerupRoutine());
    }


    IEnumerator SpawnEnemyRoutine(float delay)
    {
        yield return new WaitForSeconds(3.0f);

        while(!_stopSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-9f, 9f), 8f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.5f);

        while (!_stopSpawning)
        {
            Vector3 powerupPosition = new Vector3(Random.Range(-9f, 9f), 8, 0);
            int randomPowerupID = Random.Range(0, powerups.Length);
            Instantiate(powerups[randomPowerupID], powerupPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
