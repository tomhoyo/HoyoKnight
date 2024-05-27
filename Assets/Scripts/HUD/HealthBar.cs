using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Assets.Scripts.StringConstant;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private float _health = 0;
    public float Health { get { return _health; } set { _health = value; } }

    [SerializeField]
    private float _maxHealth = 0;
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }

    public GameObject HealthPointPrefab;
    [SerializeField]
    List<GameObject> _healthPoints = new List<GameObject>{};

    public void UpdateHealthBar(float health)
    {
        if (Health != health)
        {
            Health = health;
            ModifyHealth();
        }
    }

    private void ModifyHealth()
    {
        foreach (GameObject healthPoint in _healthPoints)
        {
            healthPoint.GetComponent<Animator>().SetBool(AnimationString.HASHEALTHPOINT, true);
        }

        int missingHealthPoint = (int)(_healthPoints.Count - Health);
        for (int i = 0; i < missingHealthPoint; i++)
        {
            _healthPoints[_healthPoints.Count-1 - i].GetComponent<Animator>().SetBool(AnimationString.HASHEALTHPOINT, false);
        }
    }

    public void UpdateMaxHealthBar(float maxHealth)
    {
        if (MaxHealth != maxHealth)
        {
            MaxHealth = maxHealth;
            ModifyMaxHealth();
        }
    }

    private void ModifyMaxHealth()
    {
        if (MaxHealth > _healthPoints.Count )
        {
            int healthPointToAdd = (int) (MaxHealth - _healthPoints.Count);
            for (int i = 0; i < healthPointToAdd; i++)
            {
                _healthPoints.Add(Instantiate(HealthPointPrefab, gameObject.transform));
            }
        }
        else if (MaxHealth < _healthPoints.Count )
        {
            int healthPointToRemove = (int)(_healthPoints.Count - MaxHealth);
            for (int i = 0; i < healthPointToRemove; i++)
            {
                _healthPoints.RemoveAt(_healthPoints.Count - 1);
            }
        }
    }

    
}
