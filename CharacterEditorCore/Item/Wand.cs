using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore.Item
{
    public class Wand : IItem
    {
        public Wand()
        {
            Name = "Wand";
        }

        public string Name { get; set; }
    }
}
