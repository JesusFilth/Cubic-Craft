﻿using System;
using UnityEngine;

namespace Source.Scripts.Core.Spawners
{
    [Serializable]
    public class SpawnObjectModel
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] [Range(0, 100)] private float _weight = 50f;

        public GameObject Prefab => _prefab;
        public float Weight => _weight;
    }
}