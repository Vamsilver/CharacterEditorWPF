using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore.Abilities
{
    [BsonKnownTypes(typeof(Blizzard), typeof(Cleave), typeof(ColdBlood),
        typeof(ConeOfCold), typeof(Rage), typeof(GhostlyStrike))]
    public class Ability
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public delegate void OnAbilityDelegate();
        public event OnAbilityDelegate? OnAbilityEvent;
    }
}
