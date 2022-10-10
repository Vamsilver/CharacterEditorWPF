using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore.Items
{
    public class Chestplate : Item
    { 
        public Chestplate(string name, int level, int strength,
            int dexterity, int constitution, int intelligence, double manaPoints,
            double hp, double attack, double physicalDefence, double magicAttack) 
                : base(name, typeOfItem : "Chestplate", level, strength, dexterity, constitution,
                  intelligence, manaPoints, hp, attack, physicalDefence, magicAttack)
        {
            TypeOfItem = "Chestplate";
        }
    }
}
