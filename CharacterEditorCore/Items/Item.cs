using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore.Items
{
    public class Item
    {
        public string Name { get; set; }


        public string TypeOfItem { get; protected set; }

        public int Level { get; set; }


        public int RequiredStrength { get; protected set; }
        public int RequiredDexterity { get; protected set; }
        public int RequiredConstitution { get; protected set; }
        public int RequiredIntelligence { get; protected set; }

        public double ManaPoints { get; protected set; }
        public double HealthPoints { get; protected set; }
        public double Attack { get; protected set; }
        public double PhysicalDefense { get; protected set; }
        public double MagicAttack { get; protected set; }

        public Item(string name, string typeOfItem, int level, int strength,
            int dexterity, int constitution, int intelligence, double manaPoints,
            double hp, double attack, double physicalDefence, double magicAttack)
        {
            Name = name;
            TypeOfItem = typeOfItem;   
            Level = level;

            RequiredStrength = strength;
            RequiredDexterity = dexterity;
            RequiredConstitution = constitution;
            RequiredIntelligence = intelligence;

            ManaPoints = manaPoints * level;
            HealthPoints = hp * level;
            Attack = attack * level;
            PhysicalDefense = physicalDefence * level;
            MagicAttack = magicAttack * level;
        }

        public string GetTypeOfItem()
        {
            return TypeOfItem;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
