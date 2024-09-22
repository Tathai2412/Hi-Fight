using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiver : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private Animator anim;
    
    public event Action OnDead;

    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    void OnEnable()
    {
        Reborn();
    }

    public BoxCollider2D Col
    {
        get => col;
        set => col = value;
    }

    public virtual void Reborn()
    {
        this.health = this.maxHealth;
    }
    
    public virtual void Deduct(int damage)
    {
        this.health -= damage;
        if (health <= 0)
        {
            OnDead?.Invoke();
        }
    }

    public virtual void HurtAnimation(bool status)
    {
        anim.SetBool("IsDamaged", status);
    }
}
