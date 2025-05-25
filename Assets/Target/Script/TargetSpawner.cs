using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public int poolSize = 1;

    private Queue<Target> targetPool = new Queue<Target>();

    //  10개의 타겟 위치 예시 (직접 지정)
    private Vector3[] spawnPositions = new Vector3[]
    {
        new Vector3(60f, -0.2f, 47f),
        new Vector3(61f, -0.2f, 45f),
        new Vector3(104f, -0.2f, 38f),
        new Vector3(81f, -0.2f, 30f),
        new Vector3(76f, 3.8f, 26f),
        new Vector3(76f, 3.8f ,30.5f),
        new Vector3(105f, 3.8f, 47f),
        new Vector3(105f, 3.8f, 47f),
        new Vector3(105f, 3.8f, 41f),
        new Vector3(56f, -0.2f, 27f),
    };

    void Start()
    {
        // 오브젝트 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(targetPrefab);
            obj.SetActive(false);
            Target target = obj.GetComponent<Target>();
            target.Initialize(this);
            targetPool.Enqueue(target);
        }

        // 초기 타겟 배치
        for (int i = 0; i < poolSize; i++)
        {
            SpawnFromPool();
        }
    }

    public void RespawnTarget(Target target)
    {
        StartCoroutine(RespawnAfterDelay(target, 1f));
    }

    private IEnumerator RespawnAfterDelay(Target target, float delay)
    {
        yield return new WaitForSeconds(delay);

        Vector3 pos = GetRandomSpawnPosition();
        target.transform.position = pos;
        target.gameObject.SetActive(true);
    }

    private void SpawnFromPool()
    {
        if (targetPool.Count == 0) return;

        Target target = targetPool.Dequeue();
        Vector3 pos = GetRandomSpawnPosition();
        target.transform.position = pos;
        target.gameObject.SetActive(true);
        targetPool.Enqueue(target);
    }

    // 지정된 위치 중 랜덤 선택
    private Vector3 GetRandomSpawnPosition()
    {
        int index = Random.Range(0, spawnPositions.Length);
        return spawnPositions[index];
    }
}
