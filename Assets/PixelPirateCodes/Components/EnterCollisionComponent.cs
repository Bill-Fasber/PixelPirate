﻿using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelPirateCodes.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {
        [SerializeField] private string _tag;
        [SerializeField] private EnterEvent _action;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (string.IsNullOrEmpty(_tag) || other.gameObject.CompareTag(_tag))
            {
                _action?.Invoke(other.gameObject);
            }
        }
    }

    [Serializable]
    public class EnterEvent : UnityEvent<GameObject>
    {
    }
}
