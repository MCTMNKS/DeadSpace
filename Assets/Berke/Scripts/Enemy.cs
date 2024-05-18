using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float heatlh;
    public float attackCooldown;
    private float coolDown;
    public bool isAttacking;
    private GameObject _player;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public SpriteRenderer[] spriteRenderers;
    // Start is called before the first frame update
    void Start()
    {
        heatlh = 10;
        coolDown = 0;
        _player = GameObject.FindGameObjectWithTag("Player");
        isAttacking = false;
    }

    public void MoveTowardsPlayer()
    {
        var distanceToPlayer = Vector2.Distance(transform.position, _player.transform.position);
        var distanceX = transform.position.x - _player.transform.position.x;
        var distanceY = transform.position.y - _player.transform.position.y;

        if (distanceToPlayer <= 10)
        {
            if (distanceY > 0) 
            {
                animator.SetBool("isRunningDown", true);
                animator.SetBool("isRunningTop", false);
                animator.SetBool("isRunningSide", false);
                animator.SetBool("isAttacking", false);
            } else if (distanceY < 0)
            {
                animator.SetBool("isRunningTop", true);
                animator.SetBool("isRunningSide", false);
                animator.SetBool("isAttacking", false);
                animator.SetBool("isRunningDown", false);
            }


            if (Mathf.Abs(distanceY) > 0)
            {
                transform.position = Vector2.MoveTowards(
                   transform.position,
                   new Vector2(transform.position.x, _player.transform.position.y),
                   speed * Time.deltaTime);
                isAttacking = false;

            }
            else if (Mathf.Abs(distanceX) > 1)
            {
                animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningTop", false);
                animator.SetBool("isRunningSide", true);
                animator.SetBool("isAttacking", false);
                spriteRenderer.flipX = distanceX < 0;
                foreach (var item in spriteRenderers)
                {
                    item.flipX = distanceX < 0;
                }
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    new Vector2(_player.transform.position.x, transform.position.y),
                    speed * Time.deltaTime);
                isAttacking = false;
            }
            else 
            {
                 animator.SetBool("isRunningDown", false);
                animator.SetBool("isRunningTop", false);
                animator.SetBool("isRunningSide", false);
                animator.SetBool("isAttacking", true);
                isAttacking = true;
            }
        }
        else 
        {
            animator.SetTrigger("isIdle");
            animator.SetBool("isAttacking", false);
        }
    }

    public void Attack()
    {
        if (isAttacking)
        {
            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
            {
                Debug.Log("Attacking");
                //_player.GetComponent<Player>().TakeDamage(1);
                coolDown = attackCooldown;
            }
        }
    }

    public void TakeDamage(int damage) 
    {
        heatlh -= damage;
        if (heatlh <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowardsPlayer();
        Attack();
    }
}
