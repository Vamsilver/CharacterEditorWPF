using System.ComponentModel;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CharacterEditorCore.Items;
using CharacterEditorCore.Abilities;
using System.Collections.ObjectModel;

namespace CharacterEditorCore
{
    public class Character
    {
        public string Name { get; set; }

        public ObjectId _id;

        public string typeOfCharacter;

        [BsonIgnoreIfDefault]
        public List<Item> inventory;

        private readonly int _inventoryCapacity = 5;

        [BsonIgnoreIfDefault]
        public List<Item> equipment;

        protected int strength;
        protected int dexterity;
        protected int constitution;
        protected int intelligence;

        public double manaPoints;
        public double healthPoints;
        public double attack;
        public double physicalDefense;
        public double magicAttack;

        public virtual int Strength { get; set; }
        public virtual int Dexterity { get; set; }
        public virtual int Constitution { get; set; }
        public virtual int Intelligence { get; set; }

        public Level level = new Level();
        protected int availablePoint;

        public List<Ability> abilities = new List<Ability>();
        public List<Ability> potentialAbilities;
        public int abilitiesPoints;

        public int AvailablePoint
        {
            get { return availablePoint; }
            set 
            {
                if(value < 0)
                {
                    return;
                }
                availablePoint = value; 
            }
        }


        public override string ToString()
        {
            return $"{Name} | {_id}";
        }

        private void LevelUp()
        {
            availablePoint += 5;
        }

        private void AbilityPointUp()
        {
            if(this.level.CurrentLevel % 3 == 0)
            {
                abilitiesPoints++;
            }
        }

        //public void ChangePerformance(Item item)
        //{
        //    manaPoints += item.ManaPoints;
        //    healthPoints += item.HealthPoints;
        //    attack += item.Attack;
        //    physicalDefense += item.PhysicalDefense;
        //    magicAttack += item.MagicAttack;
        //}

        //public void SubscribeForEvent()
        //{
        //    level.LevelUpEvent += LevelUp;
        //    level.LevelUpEvent += AbilityPointUp;
        //}

        public Character()
        {
            level.LevelUpEvent += LevelUp;
            level.LevelUpEvent += AbilityPointUp;
            availablePoint = 10;
            abilitiesPoints = 0;

            inventory = new List<Item>(_inventoryCapacity);
            equipment = new List<Item>();
        }

        public int GetInventoryMaxCapacity()
        {
            return _inventoryCapacity; 
        }
    }
}