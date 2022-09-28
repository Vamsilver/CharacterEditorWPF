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
        protected int availablePoints;

        public double manaPoints;
        public double healthPoints;
        public double attack;
        public double physicalDefense;
        public double magicAttack;

        public virtual int Strength { get; set; }
        public virtual int Dexterity { get; set; }
        public virtual int Constitution { get; set; }
        public virtual int Intelligence { get; set; }

        public override string ToString()
        {
            return $"{Name} | {_id}";
        }
    }
}