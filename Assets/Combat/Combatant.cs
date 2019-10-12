using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float recoveryRate = 50;
    [SerializeField] private float recoveryDelay = 5;
    
    private float _health;
    private float _damageThisFrame = 0;
    private float _lastDamageTime = 0;

    public float DamageThisFrame => _damageThisFrame;

    public float Health => _health;

    public void Start()
    {
        _health = maxHealth;
    }
    
    public void LateUpdate()
    {
        _damageThisFrame = 0;
    }

    public void Update()
    {
        if (Time.time - _lastDamageTime > recoveryDelay)
        {
            _health = Mathf.Clamp(_health + recoveryDelay * Time.deltaTime, 0, maxHealth);
        }
    }

    public void DoDamage(float damage)
    {
        _damageThisFrame += damage;
        lastDamageTime = Time.time;
    }
}