using System;
using _Modules.ObjectPooling.Scripts.Enums;
using UnityEngine;

namespace _Modules.ObjectPooling.Scripts.Data.ValueObjects
{
    [Serializable]
    public class PoolData
    {
        public int Amount;
        public GameObject Prefab;

        // public Attribute Data; 

        [HideInInspector] public PoolType Type;
    }
}