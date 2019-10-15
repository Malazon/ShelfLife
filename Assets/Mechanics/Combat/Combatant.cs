using System;
using UnityEngine;

public class Combatant : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float recoveryRate = 50;
    [SerializeField] private float recoveryDelay = 5;
    [SerializeField] private bool immortal = false;
    
    private float _health;
    private float _damageThisFrame = 0;
    private float _damageLastFrame = 0;
    private float _lastDamageTime = 0;
    private bool _hasDied = false;

    public bool HasDied => _hasDied;

    // Delayed by 1 frame
    public float DamageTaken => _damageLastFrame;

    // Delayed by 1 frame
    public float Health => _health;

    public void Start()
    {
        _health = maxHealth;
    }
    
    public void LateUpdate()
    {

        if (!immortal)
        {
            _health = Mathf.Clamp(_health - _damageThisFrame, 0, maxHealth);
            
            if (_health == 0)
            {
                _hasDied = true;
            }
        }
        
        _damageLastFrame = _damageThisFrame;
        _damageThisFrame = 0;
    }

    public void Kill()
    {
        _health = 0;
        _hasDied = true;
    }

    public void Update()
    {
        if (!immortal && !_hasDied && Time.time - _lastDamageTime > recoveryDelay)
        {
            _health = Mathf.Clamp(_health + recoveryDelay * Time.deltaTime, 0, maxHealth);
        }
    }

    public void DoDamage(float damage)
    {
        
        if (PauseMenuSingleton.Paused) return;
        _damageThisFrame += damage;
        _lastDamageTime = Time.time;
    }
}