using SpecialNeeds.Cloudata.Entities;
using UnityEngine;

namespace SpecialNeeds.Cloudata.Untities
{
    [CreateAssetMenu(fileName = "Music", menuName = "Special Needs/Untities/Music")]
    public class MusicUntity : ScriptableObject
    {
        public byte id;
        public string name;
        public Sprite icon;
        public AudioClip audioClip;
        public string description;
    }
}