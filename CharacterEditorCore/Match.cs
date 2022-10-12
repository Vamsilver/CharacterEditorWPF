using CharacterEditorCore.Items;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore
{
    public class Match
    {
        public ObjectId _id;

        public readonly int teamSize = 6;

        [BsonIgnoreIfDefault]
        public List<ObjectId> firstTeam;


        [BsonIgnoreIfDefault]
        public List<ObjectId> secondTeam;

        public Match()
        {
            firstTeam = new List<ObjectId>(teamSize);
            secondTeam = new List<ObjectId>(teamSize);
        }
    }
}
