using UnityEngine;
using System.Collections;

public class MeleeMonster : Enemy
{
    public GameObject claws;

    protected override void Update()
    {
        base.Update();

        if (attackCD < maxAttackCD - 0.1f)
        {
            claws.SetActive(false);
        }
    }

    protected override void Attack()
    {
        base.Attack();

        claws.SetActive(true);
    }
}
