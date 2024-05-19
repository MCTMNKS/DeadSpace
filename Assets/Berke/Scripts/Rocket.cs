using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Vector2 direction;
    public float speed;

    private void Start() 
    {
        Destroy(gameObject, 10);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            col.GetComponent<Enemy>().TakeDamage(5);
        }
    }
}
