using UnityEngine;
using System.Collections;

public class Boss : Enemy
{
    public GameObject[] laserGuns;
    public GameObject laserPrefab;

    protected override void Attack()
    {
        base.Attack();

        for (int i = 0; i < laserGuns.Length; i++)
        {
            Vector3 delta = (PlayerController.playerInstance.transform.position - laserGuns[i].transform.position);
            GameObject obj = Instantiate(laserPrefab);
            obj.transform.position = laserGuns[i].transform.position;
            obj.tag = "EnemyAttack";
            obj.GetComponent<Bullet>().velocityVector = delta.normalized * 10f;
        }
    }

    protected override void OnDeath()
    {
        Shard.current.gameObject.SetActive(true);
    }
}
