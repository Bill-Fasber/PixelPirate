using System;
using UnityEngine;

namespace PixelPirateCodes.Model
{
    [Serializable]
    public class PlayerData
    {
        public int Coins;
        public int Hp;
        public bool IsArmed;
        public int Sword;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    }
}