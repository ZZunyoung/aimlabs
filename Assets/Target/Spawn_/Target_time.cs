using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target_time : MonoBehaviour
{
    private Target_time_Spawner spawner;
    public float lifeTime = 5f;  // Ÿ���� ���� �ð�
    private float timer;

    public void Initialize(Target_time_Spawner spawner)
    {
        this.spawner = spawner;
    }

    private void OnEnable()
    {
        timer = lifeTime;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            gameObject.SetActive(false);
            spawner.RespawnTarget(this);
        }
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
