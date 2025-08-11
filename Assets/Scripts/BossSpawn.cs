using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    private void Start()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss != null)
        {
            boss.transform.position = transform.position;
        }
        else
        {
            Debug.LogWarning("Boss introuvable ! Le tag 'Boss' est-il bien défini ?");
        }
    }
}
