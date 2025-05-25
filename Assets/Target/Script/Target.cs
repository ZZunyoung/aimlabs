using UnityEngine;

public class Target : MonoBehaviour
{
    private TargetSpawner spawner;

    public void Initialize(TargetSpawner targetSpawner)
    {
        spawner = targetSpawner;
    }

    public void Respawn()
    {
        if (spawner != null)
        {
            spawner.RespawnTarget(this);
        }
    }
}

