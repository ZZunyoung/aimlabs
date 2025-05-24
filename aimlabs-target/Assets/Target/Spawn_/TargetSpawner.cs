using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public int poolSize = 3;
    public float spawnRange = 10f;
    public float checkRadius = 0.5f;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;

    private Queue<Target> targetPool = new Queue<Target>();

    void Start()
    {
        // ������Ʈ Ǯ �ʱ�ȭ
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(targetPrefab);
            obj.SetActive(false);
            Target target = obj.GetComponent<Target>();
            target.Initialize(this);
            targetPool.Enqueue(target);
        }

        // �ʱ� Ÿ�� ��ġ
        for (int i = 0; i < poolSize; i++)
        {
            SpawnFromPool();
        }
    }

    // Ÿ�� ���ġ ��û
    public void RespawnTarget(Target target)
    {
        StartCoroutine(RespawnAfterDelay(target, 1f));
    }

    private IEnumerator RespawnAfterDelay(Target target, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnFromPool();
    }

    // Ÿ���� ���� ��ġ�� ���ġ
    private void SpawnFromPool()
    {
        if (targetPool.Count == 0) return;

        Target target = targetPool.Dequeue();
        Vector3 spawnPos = FindValidPosition();

        if (spawnPos != Vector3.zero)
        {
            target.transform.position = spawnPos;
            target.gameObject.SetActive(true);
            targetPool.Enqueue(target); // ���� ��⿭�� �ٽ� �ֱ�
        }
    }

    // ��ȿ�� ��ġ ã�� (��ֹ� ���� + �ٴ� Raycast)
    private Vector3 FindValidPosition()
    {
        for (int i = 0; i < 30; i++)
        {
            // ������ ��ġ �������� ���� �ݰ� ��ġ ����
            Vector3 offset = new Vector3(
                Random.Range(-spawnRange, spawnRange),
                0f,
                Random.Range(-spawnRange, spawnRange)
            );

            Vector3 pos = transform.position + offset;
            pos.y = -0.2f; // ���� ���� (�ٴڿ� ��ġ�ϵ���)

            // ��ֹ� �浹 �˻�
            if (!Physics.CheckSphere(pos, checkRadius, obstacleLayer))
            {
                return pos;
            }
        }

        Debug.LogWarning("��ȿ�� ��ġ�� ã�� ���߽��ϴ�. (��ֹ��� ����)");
        return Vector3.zero;
    }

}
