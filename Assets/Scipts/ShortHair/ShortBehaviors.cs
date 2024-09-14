using System;
using UnityEngine;

public class ShortBehaviors : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    public static int health = 100;

    public static bool shortAttacking;
    private Animator shortHairAnim;

    void Start()
    {
        shortHairAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Move();
        Attack();
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
        shortHairAnim.SetBool("IsPunching", false);
        shortHairAnim.SetBool("IsKicking", false);
        shortHairAnim.SetBool("IsDamaged", false);
        shortAttacking = false;
        if (Input.GetKeyDown(KeyCode.J))
        {
            shortHairAnim.SetBool("IsPunching", true);
            shortAttacking = true;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            shortHairAnim.SetBool("IsKicking", true);
            shortAttacking = true;
        }
    }

    private void OnTriggerEnter2D (Collider2D col)
    {
        Debug.Log("Touched");
        if (col.gameObject.tag.Equals("LongHair") && LongBehaviors.longAttacking)
        {
            Debug.Log("Attacked");
            health -= 10;
            shortHairAnim.SetBool("IsDamaged", true);
        }
    }
}
