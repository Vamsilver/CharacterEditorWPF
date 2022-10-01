using System.ComponentModel;
using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using CharacterEditorCore.Item;

namespace CharacterEditorCore
{
    public class Character
    {
        public string Name { get; set; }

        public ObjectId _id;

        public string typeOfCharacter;

        [BsonIgnoreIfDefault]
        public List<IItem> inventory = new List<IItem>(3);

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

        public Character()
        {
            level.LevelUpEvent += LevelUp;
            availablePoint = 10;
        }
    }
}