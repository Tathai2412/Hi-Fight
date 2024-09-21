using System;
using System.Collections;
using UnityEngine;

public class ShortBehaviors : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private int health = 100;

    public bool shortAttacking;
    private Animator shortHairAnim;

    public event Action OnDead;

    void Start()
    {
        shortHairAnim = GetComponent<Animator>();
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
            shortHairAnim.SetBool("IsMoving", true);
            Quaternion direct = transform.rotation;
            direct.y = 0;
            transform.rotation = direct;

            Vector3 temp = transform.position;
            temp.x += moveSpeed * Time.deltaTime;
            transform.position = temp;
        } 
        if (Input.GetKey(KeyCode.A))
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

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            shortHairAnim.SetBool("IsPunching", true);
            CheckAttacking();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            shortHairAnim.SetBool("IsKicking", true);
            CheckAttacking();
        }
    }

    private IEnumerator Attacking()
    {
        yield return new WaitForSeconds(0.3f);
        shortAttacking = false;
        shortHairAnim.SetBool("IsPunching", false);
        shortHairAnim.SetBool("IsKicking", false);
    }

    void CheckAttacking()
    {
        if (shortHairAnim.GetBool("IsPunching") || shortHairAnim.GetBool("IsKicking"))
        {
            shortAttacking = true;
        }

        StartCoroutine(Attacking());
    }

    private IEnumerator Damaged(LongBehaviors longBehaviors)
    {
        yield return new WaitForSeconds(0.2f);
        longBehaviors.LongHairAnim.SetBool("IsDamaged", false);
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
        LongBehaviors longBehaviors = col.gameObject.GetComponent<LongBehaviors>();

        if (longBehaviors != null && shortAttacking)
        {
            //Debug.Log("Attacked");
            longBehaviors.LongHairAnim.SetBool("IsDamaged", true);
            StartCoroutine(Damaged(longBehaviors));
        }
    }
}
