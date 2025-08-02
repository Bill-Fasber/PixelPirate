using System;
using PixelPirateCodes.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PixelPirateCodes.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/Repository/Throwable", fileName = "Throwable")]
    public class ThrowableRepository : DefRepository<ThrowableDef>
    {
        
    }

    [Serializable]
    public struct ThrowableDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private GameObject _projectile;

        public string Id => _id;
        public GameObject Projectile => _projectile;
    }
}