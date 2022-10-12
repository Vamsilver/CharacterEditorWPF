using CharacterEditorCore;
using CharacterEditorCore.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
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

namespace CharacterEditorWPF
{
    /// <summary>
    /// Interaction logic for MatchWindow.xaml
    /// </summary>
    public partial class MatchWindow : Window
    {
        Match match = new Match();
        List<Character> characters = new List<Character>();


        public MatchWindow()
        {
            InitializeComponent();
            FillCharactersFromDb();
            FillData();
        }

        public void FillData()
        {
            FillTeam(match.firstTeam, lb_firstTeam);
            FillTeam(match.secondTeam, lb_secondTeam);
        }

        public void FillTeam(List<ObjectId> team, ListBox list)
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

        public void FillCharactersFromDb()
        {
            var collection = MongoDb.GetCollection();
            try
            {
                var filter = new BsonDocument();
                using var cursor = collection.FindSync(filter);
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

                if (lb_allCharacters.Items.Count != 0)
                {
                    lb_allCharacters.Items.Clear();
                }

                foreach (var character in characters)
                {
                    lb_allCharacters.Items.Add(character);
                }
            }
            catch { }
        }

        private void lb_allCharacters_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lb_allCharacters.SelectedItem is not null)
                if (e.LeftButton == Mouse.LeftButton)
                    AddCharacterToTeam(match.firstTeam);
        }

        private void lb_allCharacters_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lb_allCharacters.SelectedItem is not null)
                if (e.LeftButton == Mouse.LeftButton)
                    AddCharacterToTeam(match.secondTeam);
        }

        public void AddCharacterToTeam(List<ObjectId> team)
        {
            Character character = (Character)lb_allCharacters.SelectedItem;
            team.Add(character._id);
            lb_allCharacters.Items.Remove(character);

            FillData();
        }
    }
}
