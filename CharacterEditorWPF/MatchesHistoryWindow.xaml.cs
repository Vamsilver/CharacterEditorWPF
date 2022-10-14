using CharacterEditorCore.MongoDb;
using CharacterEditorCore;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using Match = CharacterEditorCore.Match;

namespace CharacterEditorWPF
{
    /// <summary>
    /// Interaction logic for MatchesHistoryWindow.xaml
    /// </summary>
    public partial class MatchesHistoryWindow : Window
    {
        List<Character> characters = new List<Character>();
        public MatchesHistoryWindow()
        {
            InitializeComponent();
            FillMatchesFromDB();
            FillCharactersFromDb();
        }

        private void FillMatchesFromDB()
        {
            var collection = MongoDb.GetMatchesCollection();
            try
            {
                var filter = new BsonDocument();
                using var cursor = collection.FindSync<Match>(filter);
                {
                    while (cursor.MoveNext())
                    {
                        var docs = cursor.Current;
                        foreach (var doc in docs)
                        {
                            lb_allMatches.Items.Add(doc);
                        }
                    }
                }
            }
            catch { }
        }

        public void FillCharactersFromDb()
        {
            var collection = MongoDb.GetCharactersCollection();
            try
            {
                var filter = new BsonDocument();
                using var cursor = collection.FindSync<Character>(filter);
                {
                    while (cursor.MoveNext())
                    {
                        var docs = cursor.Current;
                        foreach (var doc in docs)
                        {
                            characters.Add(doc);
                        }
                    }
                }
            }
            catch { }
        }

        private void lb_allMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Match match = (Match)lb_allMatches.SelectedItem;
            FillCharacertsToTeam(match.firstTeam, lb_firstTeam);
            FillCharacertsToTeam(match.secondTeam, lb_secondTeam);
        }

        private void FillCharacertsToTeam(List<ObjectId> team, ListBox list)
        {
            if (list.Items.Count != 0)
            {
                list.Items.Clear();
            }

            foreach (var id in team)
            {
                foreach (var character in characters.Where<Character>(x => x._id.Equals(id)).ToList())
                {
                    list.Items.Add(character);
                }
            }
        }

        private void lb_firstTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCharacterData((Character)lb_firstTeam.SelectedItem);
        }

        private void FillCharacterData(Character character)
        {
            txt_Name.Text = character.Name;
            txt_Class.Text = character.GetType().Name;
            txt_Level.Text = character.level.CurrentLevel.ToString();

            txt_Strength.Text = character.Strength.ToString();
            txt_Dexterity.Text = character.Dexterity.ToString();
            txt_Constitution.Text = character.Constitution.ToString();
            txt_Intelligence.Text = character.Intelligence.ToString();

            txt_HP.Text = character.healthPoints.ToString();
            txt_MP.Text = character.manaPoints.ToString();
            txt_Attack.Text = character.attack.ToString();
            txt_MagicAttack.Text = character.magicAttack.ToString();
            txt_PhysicalDefence.Text = character.physicalDefense.ToString();

            lb_inventory.Items.Clear();
            lb_equipment.Items.Clear();
            lb_abillity.Items.Clear();  

            foreach(var item in character.inventory)
            {
                lb_inventory.Items.Add(item);
            }

            foreach(var item in character.equipment)
            {
                lb_equipment.Items.Add(item);
            }

            foreach(var ability in character.abilities)
            {
                lb_abillity.Items.Add(ability);
            }
        }

        private void lb_secondTeam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCharacterData((Character)lb_secondTeam.SelectedItem);
        }
    }
}
