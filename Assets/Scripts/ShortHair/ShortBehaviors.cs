using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class ShortBehaviors : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int health = 100;
    [SerializeField] private DamageSender damageSender;
    [SerializeField] private DamageReceiver damageReceiver;
    [SerializeField] private float attack_Timer = 0.3f;

    public bool shortAttacking;
    private Animator shortHairAnim;
    private float current_attack_Timer;
    private bool canAttack;

    void Start()
    {
        shortHairAnim = GetComponent<Animator>();
        current_attack_Timer = attack_Timer;
    }

    void Update()
    {
        Move();
        Attack();
    }

    public Animator ShortHairAnim
    {
        get => shortHairAnim;
        set => shortHairAnim = value;
    }

    void Move()
    {
        shortHairAnim.SetBool("IsMoving", false);
        if (Input.GetKey(KeyCode.D))
        {
            if (!shortHairAnim.GetBool("IsDamaged"))
            {
                shortHairAnim.SetBool("IsMoving", true);
                Quaternion direct = transform.rotation;
                direct.y = 0;
                transform.rotation = direct;

                Vector3 temp = transform.position;
                temp.x += moveSpeed * Time.deltaTime;
                transform.position = temp;
            }
            
        } 
        if (Input.GetKey(KeyCode.A))
        {
            if (!shortHairAnim.GetBool("IsDamaged"))
            {
                shortHairAnim.SetBool("IsMoving", true);
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
        
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                shortHairAnim.SetBool("IsPunching", true);
                CheckAttacking();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (canAttack)
            {
                canAttack = false;
                attack_Timer = 0f;
                shortHairAnim.SetBool("IsKicking", true);
                CheckAttacking();
            }
        }
    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.3f);
        shortAttacking = false;
        damageSender.SetColliderEnable(false);
        shortHairAnim.SetBool("IsPunching", false);
        shortHairAnim.SetBool("IsKicking", false);
    }

    void CheckAttacking()
    {
        if (shortHairAnim.GetBool("IsPunching") || shortHairAnim.GetBool("IsKicking"))
        {
            damageSender.SetColliderEnable(true);
            shortAttacking = true;
        }

        StartCoroutine(Attacking());
    }
}
