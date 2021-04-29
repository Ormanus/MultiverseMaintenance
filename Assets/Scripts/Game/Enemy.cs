using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    int hp = 5;
    public int maxHP = 5;
    public float maxAttackCD = 1f;
    public float attackRange = 1f;
    public Image HPBar;

    protected float attackCD;

    Vector2 targetOffset;
    float offsetLength = 0.75f;

    private void Awake()
    {
        HPBar.fillAmount = 1f;
        hp = maxHP;

        targetOffset = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * offsetLength;
    }

    protected virtual void Update()
    {
        if (PlayerController.playerInstance)
        {
            attackCD -= Time.deltaTime;

            Vector3 delta = (PlayerController.playerInstance.transform.position + (Vector3)targetOffset - transform.position);
            float d = delta.magnitude;

            if (d < attackRange && attackCD <= 0f)
            {
                Attack();

                if (d < attackRange - 2f)
                {
                    transform.position += delta.normalized * -speed * Time.deltaTime;
                }
            }
            else
            {
                transform.position += delta.normalized * speed * Time.deltaTime;
            }

            float s = delta.x < 0f ? 0.3f : -0.3f;
            transform.localScale = new Vector3(s, 0.3f, 1f);
        }
    }

    protected virtual void Attack()
    {
        attackCD = maxAttackCD;
    }

    protected virtual void OnDeath()
    {

    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerAttack")
        {
            hp--;
            HPBar.fillAmount = (hp / (float)maxHP);
            ExplosionFactory.Instance.CreateExplosion(collision.transform.position, 0.1f);
            PlayerController.playerInstance?.AttackHit();

            if (hp == 0)
            {
                OnDeath();
                Destroy(gameObject);
                ExplosionFactory.Instance.CreateExplosion(transform.position, 0.5f);
            }
        }
    }
}
