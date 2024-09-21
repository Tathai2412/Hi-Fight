using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBehaviors : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int health = 100;

    public bool longAttacking;
    private Animator longHairAnim;

    public event Action OnDead;

    void Start()
    {
        longHairAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Attack();
        CheckDead();
    }

    public int Health
    {
        get => health;
        set => health = value;
    }

    public Animator LongHairAnim
    {
        get => longHairAnim;
        set => longHairAnim = value;
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
        
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            longHairAnim.SetBool("IsPunching", true);
            CheckAttacking();
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            longHairAnim.SetBool("IsKicking", true);
            CheckAttacking();
        }
    }
    
    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.3f); 
        longAttacking = false;
        longHairAnim.SetBool("IsPunching", false);
        longHairAnim.SetBool("IsKicking", false);
    }
    
    void CheckAttacking()
    {
        if (longHairAnim.GetBool("IsPunching") || longHairAnim.GetBool("IsKicking"))
        {
            longAttacking = true;
        }

        StartCoroutine(Attacking());
    }
    
    private IEnumerator Damaged(ShortBehaviors shortBehaviors)
    {
        yield return new WaitForSeconds(0.2f);
        shortBehaviors.ShortHairAnim.SetBool("IsDamaged", false);
        // 
    }

    void CheckDead()
    {
        if (health <= 0)
        {
            OnDead?.Invoke();
        }
    }

    private void OnTriggerStay2D (Collider2D col)
    {
        ShortBehaviors shortBehaviors = col.gameObject.GetComponent<ShortBehaviors>();
        
        if (shortBehaviors != null && longAttacking)
        {
            shortBehaviors.ShortHairAnim.SetBool("IsDamaged", true);
            StartCoroutine(Damaged(shortBehaviors));
        }
    }
}
