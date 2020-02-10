using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController :Singleton<EnemyController>
{
    public float currentEnemyVelocity = 1f;
    public float currentEnemySpawnTime = 1f;

    public float timeToSpawnNextEnemy = 3f;

    private float currentTime;

    public RectTransform spawnPosition;

    public ObjectPool enemyPool;
    protected override void Awake()
    {
        this.IsPersistentBetweenScenes = false;
        base.Awake();
    }

    private void Update() {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToSpawnNextEnemy && !GameController.Instance.gameOver && !GameController.Instance.gamePaused) {
            GameObject newEnemy = enemyPool.GetPooledObject();
            RectTransform enemyPosition = newEnemy.GetComponent<RectTransform>();
            enemyPosition.anchoredPosition =  new Vector2(spawnPosition.position.x, spawnPosition.position.y);
            newEnemy.SetActive(true);
            currentTime = 0f;
        }
    }
}
