using UnityEngine;

public class Target : MonoBehaviour
{
    private TargetSpawner spawner;

    // �����ʿ� ����
    public void Initialize(TargetSpawner targetSpawner)
    {
        spawner = targetSpawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);         // ��Ȱ��ȭ
            spawner.RespawnTarget(this);         // �����ʿ� ����� ��û
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // �ƹ� ���۵� ���� ����
        }
    }

}

