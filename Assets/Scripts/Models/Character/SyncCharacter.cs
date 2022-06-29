using System;
using UnityEngine;

namespace Models.Character
{
    public class SyncCharacter
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Transform Transform { get; set; }
        public bool Synced { get; set; }
    }
}