using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Creatures
{
    internal class TouchingDirections: MonoBehaviour
    {

        private ContactFilter2D castFilter;
        private float groundDistance = 0.05f;
        private BoxCollider2D touchingCol;
        private RaycastHit2D[] groundHits = new RaycastHit2D[5];

        [SerializeField]
        private bool _isGrounded;
        public bool IsGrounded { get { return _isGrounded; } private set { _isGrounded = value; } }

        [SerializeField]
        private bool _isRightWalled;
        public bool IsRightWalled { get { return _isRightWalled; } private set { _isRightWalled = value; } }
        

        [SerializeField]
        private bool _isLeftWalled;
        public bool IsLeftWalled { get { return _isLeftWalled; } private set { _isLeftWalled = value; } }
        

        [SerializeField]
        private bool _isCeiling;
        public bool IsCeiling { get { return _isCeiling; } private set { _isCeiling = value; } }
        

        private void Awake()
        {
            touchingCol = GetComponent<BoxCollider2D>();
        }

        private void FixedUpdate()
        {
            IsGrounded = touchingCol.Cast(Vector2.down, castFilter, groundHits, groundDistance) > 0;
            IsRightWalled = touchingCol.Cast(Vector2.right, castFilter, groundHits, groundDistance) > 0;
            IsLeftWalled = touchingCol.Cast(Vector2.left, castFilter, groundHits, groundDistance) > 0;
            IsCeiling = touchingCol.Cast(Vector2.up, castFilter, groundHits, groundDistance) > 0;
        }

    }
}
