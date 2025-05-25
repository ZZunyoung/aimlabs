using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_time_Spawner : MonoBehaviour
{
    public GameObject targetPrefab;        // Ÿ�� ������
    public int poolSize = 5;               // ������ Ÿ�� ��
    public float checkRadius = 0.5f;       // ��ġ ��ȿ�� �˻� ������
    public LayerMask obstacleLayer;        // ��ֹ� ���̾�
    public Vector3 spawnMin = new Vector3(0f, 0.5f, -28f); // ���� ���� ������
    public Vector3 spawnMax = new Vector3(50f, 0.5f, -10f); // ���� ���� ����

    private Queue<Target_time> targetPool = new Queue<Target_time>();

    void Start()
    {
        // ������Ʈ Ǯ �ʱ�ȭ
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(targetPrefab);
            obj.SetActive(false);
            Target_time target = obj.GetComponent<Target_time>();
            target.Initialize(this);
            targetPool.Enqueue(target);
        }

        // �ʱ� ��ġ
        for (int i = 0; i < poolSize; i++)
        {
            SpawnFromPool();
        }
    }

    // Target_time���� ȣ���
    public void RespawnTarget(Target_time target)
    {
        StartCoroutine(RespawnAfterDelay(target, 1f)); // 1�� �� ��Ȱ��ȭ
    }

    private IEnumerator RespawnAfterDelay(Target_time target, float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnFromPool();
    }

    private void SpawnFromPool()
    {
        if (targetPool.Count == 0) return;

        Target_time target = targetPool.Dequeue();
        Vector3 spawnPos = FindValidPosition();

        if (spawnPos != Vector3.zero)
        {
            target.transform.position = spawnPos;
            target.gameObject.SetActive(true);
            targetPool.Enqueue(target); // �ٽ� Ǯ�� �ֱ�
        }
    }

    // ��ȿ�� ��ġ ã�� (��ֹ� ���� ����)
    private Vector3 FindValidPosition()
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 pos = new Vector3(
                Random.Range(spawnMin.x, spawnMax.x),
                spawnMin.y,
                Random.Range(spawnMin.z, spawnMax.z)
            );

            if (!Physics.CheckSphere(pos, checkRadius, obstacleLayer))
            {
                return pos;
            }
        }

        Debug.LogWarning("��ȿ�� ���� ��ġ�� ã�� ����.");
        return Vector3.zero;
    }
}
