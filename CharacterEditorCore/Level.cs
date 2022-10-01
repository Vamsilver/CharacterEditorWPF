using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharacterEditorCore
{
    public class Level
    {
        private int _currentLevel = 1;
        private int _currentExperience = 0;
        private int _growEdge = 1000;
        private int _currentLevelEdge = 1000;

        public int CurrentLevel
        {
            get { return _currentLevel; }
            set 
            {
                _currentLevel = value;
                LevelUpEvent?.Invoke();
            }
        }

        public int CurrentExperience
        {
            get { return _currentExperience; }
            set
            {
                if(value >= _currentLevelEdge)
                {
                    ++CurrentLevel;
                    _currentLevelEdge += _growEdge * CurrentLevel;
                }
                _currentExperience = value;
            }
        }

        public delegate void LevelDelegate(); 
        public event LevelDelegate? LevelUpEvent;
    }
}
