using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private int _maxHealh;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentHealh;
    private Animator _animator;

    public int Money { get; private set; }

    public void ApplyDamage(int damage)
    {
        _currentHealh -= damage;

        if (_currentHealh <= 0)
            Destroy(gameObject);
    }

    public void AddMoney(int reward)
    {
        Money += reward;
    }

    private void Start()
    {
        _currentWeapon = _weapons[0];
        _currentHealh = _maxHealh;
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentWeapon.Shoot(_shootPoint);
        }
    }
}
