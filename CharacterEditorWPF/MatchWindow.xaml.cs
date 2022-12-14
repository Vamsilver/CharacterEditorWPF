using CharacterEditorCore;
using CharacterEditorCore.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
        Match match;
        List<Character> characters = new List<Character>();
        List<ObjectId> firstTeam = new List<ObjectId>();
        List<ObjectId> secondTeam = new List<ObjectId>();


        public MatchWindow()
        {
            InitializeComponent();
            FillCharactersFromDb();
            FillData();
        }

        public void FillData()
        {
            FillTeam(firstTeam, lb_firstTeam);
            FillTeam(secondTeam, lb_secondTeam);
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

            ChangeStartMatchButtonColor();
            DisplayAverageLevelOfTheTeam(firstTeam);
            DisplayAverageLevelOfTheTeam(secondTeam);
        }

        public void FillCharactersFromDb()
        {
            var collection = MongoDb.GetCharactersCollection();
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
                    AddCharacterToTeam(firstTeam);
        }

        private void lb_allCharacters_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (lb_allCharacters.SelectedItem is not null)
                if (e.LeftButton == Mouse.LeftButton)
                    AddCharacterToTeam(secondTeam);
        }

        public void AddCharacterToTeam(List<ObjectId> team)
        {
            if (team.Count < 6)
            {
                Character character = (Character)lb_allCharacters.SelectedItem;
                team.Add(character._id);
                lb_allCharacters.Items.Remove(character);

                FillData();
            }
            else
                MessageBox.Show("No");
        }

        public void DeleteCharacterFromTeam(List<ObjectId> team, ListBox box)
        {
            Character character = (Character)box.SelectedItem;
            team.Remove(character._id);
            lb_allCharacters.Items.Add(character);

            FillData();
        }

        private void lb_firstTeam_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (lb_firstTeam.SelectedItem is not null)
                if (e.Key == Key.Delete)
                    DeleteCharacterFromTeam(firstTeam, lb_firstTeam);
        }

        private void lb_secondTeam_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (lb_secondTeam.SelectedItem is not null)
                if (e.Key == Key.Delete)
                    DeleteCharacterFromTeam(secondTeam, lb_secondTeam);
        }

        private void ChangeStartMatchButtonColor()
        {
            if(CheckTeamBalance() && (!txt_FirstTeamAverLvl.Text.Equals("0") || !txt_SecondTeamAverLvl.Text.Equals("0")))
                btn_StartMatch.Background = Brushes.Green;
            else
                btn_StartMatch.Background = Brushes.Red;
        }

        private void DisplayAverageLevelOfTheTeam(List<ObjectId> ids)
        {
            if(ids == firstTeam)
                txt_FirstTeamAverLvl.Text = CalcAverageLevelOfTeam(ids).ToString();
            else
                txt_SecondTeamAverLvl.Text = CalcAverageLevelOfTeam(ids).ToString();
        }

        private bool CheckTeamBalance()
        {
            double firstTeamAverage = CalcAverageLevelOfTeam(firstTeam);
            double secondTeamAverage = CalcAverageLevelOfTeam(secondTeam);

            return Math.Abs(firstTeamAverage - secondTeamAverage) <= 1;
        }

        private double CalcAverageLevelOfTeam(List<ObjectId> ids)
        {
            double answer = 0;

            if (ids.Count != 0)
            {
                foreach (var id in ids)
                {
                    foreach (var character in characters.Where<Character>(x => x._id.Equals(id)).ToList())
                    {
                        answer += character.level.CurrentLevel;
                    }
                }

                return Math.Round(answer / 6, 2);
            }
            else
                return 0;
        }

        private void btn_AutoFill_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();

            if(firstTeam.Count != 0 || secondTeam.Count != 0)
                ClearCharFromTeams();

            while(true)
            {
                foreach(var character in characters)
                {
                    if (random.Next(1, 3) == 1 && firstTeam.Count < 6)
                        firstTeam.Add(character._id);
                    else if (random.Next(1, 3) == 2 && secondTeam.Count < 6)
                        secondTeam.Add(character._id);
                }

                if (CheckTeamBalance())
                    break;
                else
                {
                    ClearCharFromTeams();
                }
            }

            FillData();
        }

        private void ClearCharFromTeams()
        {
            firstTeam.Clear();
            secondTeam.Clear();
        }

        private void btn_StartMatch_Click(object sender, RoutedEventArgs e)
        {
            if(btn_StartMatch.Background == Brushes.Green)
            {
                match = new Match();
                match.firstTeam = firstTeam;
                match.secondTeam = secondTeam;
                match.date = DateTime.Now;
                MongoDb.AddMatchToDataBase(match);
                ClearCharFromTeams();

                lb_allCharacters.Items.Clear();
                lb_firstTeam.Items.Clear();
                lb_secondTeam.Items.Clear();

                foreach (var character in characters)
                {
                    lb_allCharacters.Items.Add(character);
                }
            }
            else
            {
                MessageBox.Show("Teams are not balanced");
            }
        }

        private void btn_ShowMatchesHistory_Click(object sender, RoutedEventArgs e)
        {
            MatchesHistoryWindow history = new MatchesHistoryWindow();
            history.Owner = this;
            history.Show();
        }
    }
}
