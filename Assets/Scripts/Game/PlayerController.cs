using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController playerInstance;

    public float thrust = 1f;
    public float dashSpeed = 10f;
    public GameObject powerAttack;
    public GameObject slashAttack;
    public float attackDistance = 100f;

    public Image dashCD;
    public Image HPBar;
    public Image powerBar;

    public Animator animator;

    GameObject endScreen;

    int hp = 0;
    int maxHP = 20;
    float power = 0f;
    float powerPerAttack = 0.126f;
    float powerAttackTime = 0.0f;

    Rigidbody2D rb;

    float dashMaxCooldown = 1f;
    float dashCooldown = 0f;

    float slashMaxCooldown = 1f;
    float slashCooldown = 0f;

    private void Awake()
    {
        playerInstance = this;

        rb = GetComponent<Rigidbody2D>();
        slashAttack.SetActive(false);
        powerAttack.SetActive(false);

        HPBar.fillAmount = 1f;
        powerBar.fillAmount = 0f;

        endScreen = GameObject.Find("Canvas/DeathScreen");
        endScreen.SetActive(false);

        hp = maxHP;
    }

    private void Update()
    {
        dashCooldown -= Time.deltaTime;
        slashCooldown -= Time.deltaTime;
        powerAttackTime -= Time.deltaTime;

        dashCD.fillAmount = Mathf.Clamp01(dashCooldown / dashMaxCooldown);

        Vector2 mouseDir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;

        if (slashCooldown < slashMaxCooldown - 0.1f)
        {
            slashAttack.SetActive(false);
        }
        else
        {
            UpdateSlashPosition(mouseDir);
        }

        if (powerAttackTime > 0f)
        {
            float scale = 5f - (powerAttackTime * 10f);
            powerAttack.transform.localScale = new Vector3(scale, scale, scale);
        }
        else
        {
            powerAttack.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && slashCooldown <= 0f)
        {
            slashCooldown = slashMaxCooldown;
            slashAttack.SetActive(true);
            UpdateSlashPosition(mouseDir);

            animator.Play("PlayerAttack", 0);
        }

        if (Input.GetMouseButtonDown(1) && dashCooldown <= 0f)
        {
            dashCooldown = dashMaxCooldown;
            float magnitude = rb.velocity.magnitude;
            rb.velocity = mouseDir * (dashSpeed + magnitude);
        }

        if (Input.GetKey(KeyCode.E))
        {
            if (power >= 1f)
            {
                power = 0f;
                powerBar.fillAmount = power;
                PowerAttack();
            }
        }

        if (power >= 1f)
        {
            float s = 1f + Mathf.Sin(Time.time * 5f) * 0.1f;
            powerBar.transform.parent.localScale = new Vector3(s, s, s);
        }
        else
        {
            powerBar.transform.parent.localScale = Vector3.one;
        }

        GetComponent<SpriteRenderer>().flipX = mouseDir.x < 0f;
    }

    public void AttackHit()
    {
        power += powerPerAttack;
        powerBar.fillAmount = power;
    }

    void PowerAttack()
    {
        powerAttack.SetActive(true);
        powerAttackTime = 0.5f;
    }

    void UpdateSlashPosition(Vector2 mouseDir)
    {
        float angle = Mathf.Atan2(mouseDir.y, mouseDir.x);
        float dist = attackDistance * (slashMaxCooldown - slashCooldown) * 10f;
        slashAttack.transform.localPosition = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * dist;
        slashAttack.transform.localEulerAngles = new Vector3(0f, 0f, angle * Mathf.Rad2Deg);

        float scale = (slashMaxCooldown - slashCooldown) * 15f + 0.5f;

        slashAttack.transform.localScale = new Vector3(scale, scale, scale);
    }

    public void Die()
    {
        endScreen.SetActive(true);
        ExplosionFactory.Instance.CreateExplosion(transform.position, 2f);
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
            rb.AddForce(Vector2.right * thrust);
        if (Input.GetKey(KeyCode.W))
            rb.AddForce(Vector2.up * thrust);
        if (Input.GetKey(KeyCode.A))
            rb.AddForce(Vector2.left * thrust);
        if (Input.GetKey(KeyCode.S))
            rb.AddForce(Vector2.down * thrust);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyAttack")
        {
            hp--;
            HPBar.fillAmount = ((float)hp / maxHP);
            ExplosionFactory.Instance.CreateExplosion(transform.position, 0.2f);
            if (hp == 0)
            {
                Die();
            }
        }

        else if (collision.tag == "Shard")
        {
            collision.gameObject.GetComponent<Shard>().Collect();
        }
    }
}
