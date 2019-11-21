using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{

    public float maxSpeed;
    public float minHeight, maxHeight;
    public float damageTimer = 0.5f;
    public int maxHealth;
    public float attackRate = 1f;
    public string enemyName;
    public Sprite enemyImage;

    protected int currentHealth;
    private float currentSpeed;
    protected Rigidbody rig;
    protected Animator anim;
    private Transform groundCheck;
    private bool onGround;
    protected bool facingRight = false;
    private Transform target;
    protected bool isDead = false;
    private float zForce;
    private float walkTimer;
    protected bool damaged = false;
    private float damagedTimer;
    private float nextAttack;

    // Start is called before the first frame update
    void Awake()
    {

        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundCheck = transform.Find("GroundCheck");
        target = FindObjectOfType<Player>().transform;
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {

        onGround = Physics.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        anim.SetBool("Grounded", onGround);
        anim.SetBool("Dead", isDead);

        if (!isDead)
        {

            facingRight = (target.position.x < transform.position.x) ? false : true;
            if (facingRight)
            {

                transform.eulerAngles = new Vector3(0, 180, 0);

            }
            else
            {

                transform.eulerAngles = new Vector3(0, 0, 0);

            }

        }

        if (damaged && isDead)
        {

            damageTimer += Time.deltaTime;
            if (damagedTimer >= damageTimer)
            {

                damaged = false;
                damageTimer = 0;

            }

        }

        walkTimer += Time.deltaTime;

    }

    private void FixedUpdate()
    {

        if (!isDead)
        {

            Vector3 targetDitance = target.position - transform.position;
            float hForce = targetDitance.x / Mathf.Abs(targetDitance.x);

            if (walkTimer >= Random.Range(1f, 2f))
            {

                zForce = Random.Range(-1, 2);
                walkTimer = 0;

            }

            if (Mathf.Abs(targetDitance.x) < 1.5f)
            {

                hForce = 0;

            }

            if(!damaged)
            rig.velocity = new Vector3(hForce * currentSpeed, 0, zForce * currentSpeed);

            anim.SetFloat("Speed", Mathf.Abs(currentSpeed));

            if(Mathf.Abs(targetDitance.x) < 1.5f && Mathf.Abs(targetDitance.z) < 1.5f && Time.time > nextAttack)
            {

                anim.SetTrigger("Attack");
                currentSpeed = 0;
                nextAttack = Time.time + attackRate;                

            }

        }

        rig.position = new Vector3(rig.position.x,
        rig.position.y,
        Mathf.Clamp(rig.position.z, minHeight, maxHeight));

    }

    public virtual void TookDamage(int damage)
    {

        if (!isDead)
        {
            damaged = true;
            currentHealth -= damage;
            anim.SetTrigger("HitDamage");
            FindObjectOfType<UIManager>().UpdateEnemyUI(maxHealth, currentHealth, enemyName, enemyImage);
            if (currentHealth <= 0)
            {
                isDead = true;
                rig.AddRelativeForce(new Vector3(3, 5, 0), ForceMode.Impulse);
                Destroy(gameObject);
            }
        }

    }

    public void DisableEnemy()
    {

        gameObject.SetActive(false);
        Destroy(gameObject);

    }

    private void ResetSpeed()
    {

        currentSpeed = maxSpeed;

    }

}
