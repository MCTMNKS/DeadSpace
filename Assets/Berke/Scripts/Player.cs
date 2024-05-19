using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float health;
    public Animator animator;
    public GameObject rocketPrefab;
    public SpriteRenderer spriteRenderer;
    public SpriteRenderer[] spriteRenderers;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

     public void TakeDamage(int damage) 
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunningDown", false);
            animator.SetBool("isRunningTop", true);
            animator.SetBool("isRunningSide", false);
            animator.SetBool("isAttacking", false);
            transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunningDown", false);
            animator.SetBool("isRunningTop", false);
            animator.SetBool("isRunningSide", true);
            animator.SetBool("isAttacking", false);
            spriteRenderer.flipX = true;
            foreach (var item in spriteRenderers)
            {
                item.flipX = true;
            }

            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = false;
            foreach (var item in spriteRenderers)
            {
                item.flipX = false;
            }
            animator.SetBool("isRunningDown", false);
            animator.SetBool("isRunningTop", false);
            animator.SetBool("isRunningSide", true);
            animator.SetBool("isAttacking", false);
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isRunningDown", true);
            animator.SetBool("isRunningTop", false);
            animator.SetBool("isRunningSide", false);
            animator.SetBool("isAttacking", false);
            transform.Translate(Vector2.down * speed * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var spawnPos = Vector2.zero;
            var dir = Vector2.zero;
            if (spriteRenderer.flipX)
            {
                spawnPos = new Vector2(transform.position.x + 1, transform.position.y- 0.5f) ;
                dir = Vector2.right;
            } else 
            {
                spawnPos = new Vector2(transform.position.x - 1, transform.position.y- 0.5f);
                dir = Vector2.left;
            }
            var rocket = Instantiate(rocketPrefab, spawnPos, Quaternion.identity);
            rocket.GetComponent<Rocket>().SetDirection(dir);
            animator.SetBool("isRunningDown", false);
            animator.SetBool("isRunningTop", false);
            animator.SetBool("isRunningSide", false);
            animator.SetBool("isAttacking", true);
        }
        else 
        {
            animator.SetTrigger("isIdle");
            animator.SetBool("isRunningDown", false);
            animator.SetBool("isRunningTop", false);
            animator.SetBool("isRunningSide", false);
            animator.SetBool("isAttacking", false);
            animator.SetBool("isAttacking", false);
        }
    }
}
