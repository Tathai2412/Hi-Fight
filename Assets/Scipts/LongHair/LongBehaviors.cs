using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBehaviors : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public static int health = 100;

    public static bool longAttacking;
    private Animator longHairAnim;

    void Start()
    {
        longHairAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Attack();
        Debug.Log(longAttacking);
    }

    void Move()
    {
        longHairAnim.SetBool("IsMoving", false);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            longHairAnim.SetBool("IsMoving", true);
            Quaternion direct = transform.rotation;
            direct.y = 0;
            transform.rotation = direct;

            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;
            transform.position = temp;
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            longHairAnim.SetBool("IsMoving", true);
            Quaternion direct = transform.rotation;
            direct.y = 180;
            transform.rotation = direct;

            Vector3 temp = transform.position;
            temp.x -= moveSpeed * Time.deltaTime;
            transform.position = temp;
        }
    }

    void Attack()
    {
        longHairAnim.SetBool("IsPunching", false);
        longHairAnim.SetBool("IsKicking", false);
        
        longAttacking = false;
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            longHairAnim.SetBool("IsPunching", true);
            longAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            longHairAnim.SetBool("IsKicking", true);
            longAttacking = true;
        }
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        if (col.gameObject.tag.Equals("ShortHair") && ShortBehaviors.shortAttacking)
        {
            health -= 10;
            longHairAnim.SetBool("IsDamaged", true);
        }
        longHairAnim.SetBool("IsDamaged", false);
    }
}
