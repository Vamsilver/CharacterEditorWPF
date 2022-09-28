using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore.Item
{
    public class Axe : IItem
    {
        public Axe()
        {
            Name = "Axe";
        }

        public string Name { get; set; }
    }
}
