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
            GameManager.Instance.AddScore(10);
            gameObject.SetActive(false);         // ��Ȱ��ȭ
            spawner.RespawnTarget(this);         // �����ʿ� ����� ��û
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            // �ƹ� ���۵� ���� ����
        }
    }
  
    public void Respawn()
    {
        if (spawner != null)
        {
            spawner.RespawnTarget(this);
        }
    }
}

