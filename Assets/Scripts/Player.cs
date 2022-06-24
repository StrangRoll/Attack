using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]

public class Player : MonoBehaviour
{
    [SerializeField] private List<Weapon> _weapons;
    [SerializeField] private int _maxHealh;
    [SerializeField] private Transform _shootPoint;

    private Weapon _currentWeapon;
    private int _currentWeaponIndex = 0;
    private int _currentHealh;
    private Animator _animator;

    public int Money { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction<int> MoneyChaged;

    public void ApplyDamage(int damage)
    {
        _currentHealh -= damage;

        if (_currentHealh <= 0)
        {
            _currentHealh = 0;
            Destroy(gameObject);
        }

        HealthChanged?.Invoke(_currentHealh, _maxHealh);
    }

    public void AddMoney(int reward)
    {
        Money += reward;
        MoneyChaged?.Invoke(Money);
    }

    public void BuyWeapon(Weapon weapon)
    {
        Money -= weapon.Price;
        MoneyChaged?.Invoke(Money);
        _weapons.Add(weapon);
    }

    public void NextWeapon()
    {
        if (++_currentWeaponIndex >= _weapons.Count)
        {
            _currentWeaponIndex = 0;
        }

        ChangeWeapon(_currentWeaponIndex);
    }

    public void PreviousWeapon()
    {
        if (--_currentWeaponIndex < 0)
        {
            _currentWeaponIndex = _weapons.Count - 1;
        }

        ChangeWeapon(_currentWeaponIndex);
    }

    private void ChangeWeapon(int index)
    {
        _currentWeapon = _weapons[index];
    }

    private void Start()
    {
        ChangeWeapon(_currentWeaponIndex);
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
