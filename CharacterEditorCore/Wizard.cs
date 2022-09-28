using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore
{
    public class Wizard : Character
    {
        public Wizard()
        {
            Strength = (int)Enums.WizardStats.minStrength;
            Dexterity = (int)Enums.WizardStats.minDexterity;
            Constitution = (int)Enums.WizardStats.minConstitution;
            Intelligence = (int)Enums.WizardStats.minIntelligence;
            typeOfCharacter = "Wizard";
        }

        public Wizard(int strength, int dexterity, int constitution, int intelligence, string name)
        {
            Strength = strength;
            Dexterity = dexterity;
            Constitution = constitution;
            Intelligence = intelligence;
            Name = name;
            typeOfCharacter = "Wizard";
        }

        public override int Strength
        {
            get { return strength; }
            set
            {
                if (value >= (int)Enums.WizardStats.minStrength &&
                    value <= (int)Enums.WizardStats.maxStrength)
                {
                    attack += (value - strength) * 3;
                    healthPoints += (value - strength);

                    strength = value;
                }
            }
        }

        public override int Dexterity
        {
            get { return dexterity; }
            set
            {
                if (value >= (int)Enums.WizardStats.minDexterity &&
                    value <= (int)Enums.WizardStats.maxDexterity)
                {
                    physicalDefense += (value - dexterity) * 0.5;

                    dexterity = value;
                }
            }
        }

        public override int Constitution
        {
            get { return constitution; }
            set
            {
                if (value >= (int)Enums.WizardStats.minConstitution &&
                    value <= (int)Enums.WizardStats.maxConstitution)
                {
                    healthPoints += (value - constitution) * 3;
                    physicalDefense += (value - constitution);

                    constitution = value;
                }
            }
        }

        public override int Intelligence
        {
            get { return intelligence; }
            set
            {
                if (value >= (int)Enums.WizardStats.minIntelligence &&
                    value <= (int)Enums.WizardStats.maxIntelligence)
                {
                    manaPoints += (value - intelligence) * 2;
                    magicAttack += (value - intelligence) * 5;

                    intelligence = value;
                }
            }
        }
    }
}
