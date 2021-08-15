using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable<int>, IKillable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private int _minHealth;
    [SerializeField] private int _currentHealth;
    private int _defaultHealth = 50;

    #region Setup

    private void Start()
    {
        _maxHealth = SetDefaultHealth(_maxHealth);
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Ensures Max Health is not set to 0. Uses _defaultHealth int
    /// </summary>
    private int SetDefaultHealth(int currentMaxVal)
    {
        if (currentMaxVal > 0)
        {
            return currentMaxVal;
        }
        else
        {
            return _defaultHealth;
        }
    }

    #endregion

    public void Damage(int damageAmount)
    {
        _currentHealth -= damageAmount;
        if (_currentHealth < _minHealth)
        {
            Kill();
        }
    }

    public void Kill()
    {
        if (this.gameObject.GetComponent<Player>() != null) // the player is getting killed
        {
            Camera.main.transform.parent = null;
        }
        Destroy(this.gameObject);
    }
}
