using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int currentHealth { get; private set; }
    public int maxHealth = 100;
    public Animator animator;

    public Transform attackPoint;
    public float attackRange;
    public int attackPower;
    public float attackSpeed;
    public float attackTime;

    public Transform player;
    public LayerMask playerLayer;
    public AIDestinationSetter destinationSetter;
    public Collider2D collider;
    float eps = 11f;
    List<Point> pastTransformList;
    class Point
    {
        public float x;
        public float y;
        public Point(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public Point(Transform transform)
        {
            x = transform.position.x;
            y = transform.position.y;
        }
    }

    void Start()
    {
        currentHealth = maxHealth;
        pastTransformList = new List<Point>();
        pastTransformList.Add(new Point(transform));
    }

    void Update()
    {
        if (pastTransformList[0].x != transform.position.x)
        {
            animator.SetBool("IsRunning", true);
            if (pastTransformList[0].x > transform.position.x)
                transform.rotation = new Quaternion(0, 180, 0, 0);
            else transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        else animator.SetBool("IsRunning", false);
        pastTransformList.Add(new Point(transform));
        if (pastTransformList.Count > 6) pastTransformList.RemoveAt(0);
        if ((Math.Abs(player.transform.position.x - transform.position.x) < eps) && (Math.Abs(player.transform.position.y - transform.position.y) < eps))
        {
            if (Time.time >= attackTime)
            {
                Attack();
                attackTime = Time.time + 1f / attackSpeed;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("TakeHit");
        if (currentHealth <= 0)
        {
            animator.SetTrigger("Dead");
            this.enabled = false;
            destinationSetter.enabled = false;
            collider.enabled = false;
            animator.SetTrigger("Lying");
        }
    }
    public void Attack()
    {
        animator.SetTrigger("Attacking");
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Player>().TakeDamage(attackPower);
            Debug.Log(enemy.GetComponent<Player>().currentHealth);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
