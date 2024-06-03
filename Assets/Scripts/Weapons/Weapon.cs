using Assets.Scripts.StringConstant;
using UnityEngine;
using Assets.Scripts.Creatures;
using Assets.Scripts.InputActions;
using System;

namespace Assets.Scripts.Weapons
{
    internal class Weapon : MonoBehaviour
    {
        private TouchingDirections _direction;
        public TouchingDirections Direction { get { return _direction; } private set { _direction = value; } }

        private Animator _animator;
        public Animator Animator { get { return _animator; } private set { _animator = value; } }

        private SpriteRenderer _spriteRenderer;
        public SpriteRenderer SpriteRenderer { get { return _spriteRenderer; } private set { _spriteRenderer = value; } }

        private InputListener _inputListener;
        public InputListener InputListener { get { return _inputListener; } private set { _inputListener = value; } }

        [SerializeField]
        private WeaponData _weaponData;
        public WeaponData WeaponData { get { return _weaponData; } private set { _weaponData = value; } }

        private GameObject _weaponUser;
        public GameObject WeaponUser { get { return _weaponUser; } private set { _weaponUser = value; } }


        private float _lastTimeMainAction = 0f;
        public float LastTimeMainAction { get { return _lastTimeMainAction; }  set { _lastTimeMainAction = value; } }

        public void Start()
        {
            WeaponUser = gameObject.transform.parent.gameObject;
            Direction = WeaponUser.GetComponent<TouchingDirections>();
            Animator = WeaponUser.GetComponent<Animator>();
            SpriteRenderer = WeaponUser.GetComponent<SpriteRenderer>();

            InputListener = GameObject.Find(GameObjectsName.INPUTMANAGER)?.GetComponent<InputListener>();
        }

        
    }
}
