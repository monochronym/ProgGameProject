using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 move;
    public Rigidbody2D body;
    public Transform attackPoint;
    public float speedX;
    public float speedY;
    public Animator animator;
    public float attackRange;
    public MapEdges mapEdges;
    public LayerMask enemiesLayer;
    public float currentHealth { get; private set; }
    public int maxHealth = 100;
    public int attackPower;
    public float attackSpeed;
    public float attackTime;
    public Slider slider;
    public float regenerateHealth;
    public float timeOfDeath;
    public void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    public void Moving()
    {
        //(body.position.x > mapEdges.RightEdge ? 0 : speedX)
        move.x = Input.GetKey(KeyCode.A) ? (body.position.x < mapEdges.LeftEdge ? 0 : -speedX) : 0;
        move.x = Input.GetKey(KeyCode.D) ? speedX : move.x;
        move.y = Input.GetKey(KeyCode.S) ? (body.position.y < mapEdges.DownEdge ? 0 : -speedY) : 0;
        move.y = Input.GetKey(KeyCode.W) ? (body.position.y > mapEdges.UpEdge ? 0 : speedY) : move.y;
        if (move.x != 0 || move.y != 0) animator.SetBool("IsRunning", true);
        else animator.SetBool("IsRunning", false);
        if (move.x < 0) transform.rotation = new Quaternion(0, 180, 0, 0);
        else if (move.x > 0) transform.rotation = new Quaternion(0, 0, 0, 0);
        body.position += move;
       
    }
    public void Attack(int x)
    {
        animator.SetTrigger("Attacking"+x);
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemiesLayer);
        foreach (Collider2D enemy in enemies)
        {
            Debug.Log("Detected");
            enemy.GetComponent<Enemy>().TakeDamage(attackPower + x * 10);
            Debug.Log(enemy.GetComponent<Enemy>().currentHealth);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!this.enabled) return;
        currentHealth -= damage;
        slider.value = currentHealth;
        animator.SetTrigger("TakeHit");
        Debug.Log(currentHealth);
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Dead");
            animator.SetBool("Lying", true);
            timeOfDeath = Time.time;
            this.enabled = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= attackTime)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                Attack(0);
                attackTime = Time.time + 1f / attackSpeed;
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                Attack(1);
                attackTime = Time.time + 2f / attackSpeed;
            }
        }
        Moving();
        if (currentHealth < maxHealth)
        {
            slider.value += regenerateHealth;
            currentHealth += regenerateHealth;
        }
    }
}
