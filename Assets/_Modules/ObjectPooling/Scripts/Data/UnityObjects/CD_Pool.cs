using _Modules.ObjectPooling.Scripts.Data.ValueObjects;
using _Modules.ObjectPooling.Scripts.Enums;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Modules.ObjectPooling.Scripts.Data.UnityObjects
{
    [CreateAssetMenu(fileName = "CD_Pool", menuName = "MaxedOutEntertainmentModules/CD_Pool", order = 0)]
    public class CD_Pool : ScriptableObject
    {
        public SerializedDictionary<PoolType, PoolData> PoolList = new SerializedDictionary<PoolType, PoolData>();
    }
}