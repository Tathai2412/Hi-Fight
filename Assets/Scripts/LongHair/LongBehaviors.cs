using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBehaviors : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int health = 100;
    [SerializeField] private DamageSender damageSender;
    [SerializeField] private DamageReceiver damageReceiver;
    [SerializeField] private float attack_Timer = 0.3f;

    public bool longAttacking;
    private Animator longHairAnim;
    private float current_attack_Timer;
    private bool canAttack;
    
    void Start()
    {
        longHairAnim = GetComponent<Animator>();
        current_attack_Timer = attack_Timer;
    }

    void Update()
    {
        Move();
        Attack();
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
            if (!longHairAnim.GetBool("IsDamaged"))
            {
                longHairAnim.SetBool("IsMoving", true);
                Quaternion direct = transform.rotation;
                direct.y = 0;
                transform.rotation = direct;

                Vector3 temp = transform.position;
                temp.x += moveSpeed * Time.deltaTime;
                transform.position = temp;
            }
        } 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!longHairAnim.GetBool("IsDamaged"))
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
    }

    void Attack()
    {
        attack_Timer += Time.deltaTime;
        if (attack_Timer > current_attack_Timer)
        {
            canAttack = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                longHairAnim.SetBool("IsPunching", true);
                CheckAttacking();
            }
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                longHairAnim.SetBool("IsKicking", true);
                CheckAttacking();
            }
        }
    }
    
    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.3f); 
        damageSender.SetColliderEnable(false);
        longAttacking = false;
        longHairAnim.SetBool("IsPunching", false);
        longHairAnim.SetBool("IsKicking", false);
    }

    void CheckAttacking()
    {
        if (longHairAnim.GetBool("IsPunching") || longHairAnim.GetBool("IsKicking"))
        {
            damageSender.SetColliderEnable(true);
            longAttacking = true;
        }

        StartCoroutine(Attacking());
    }
}
