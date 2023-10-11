using MasterMind.Data;
using MasterMind.Data.Enum;
using Microsoft.AspNetCore.Components;
using System.Drawing;
using System.Xml;

namespace MasterMind.Pages
{
    public partial class Game
    {
        [Inject] NavigationManager NavigationManager { get; set; }
        public List<string> Colors { get; set; } = new List<string>() { "red", "blue", "orange", "green", "yellow", "brown", "pink", "purple" };
        public List<Piece> Answers { get; set; } = new();
        public List<Tips> Indications { get; set; } = new();
        public List<string> ColorsLine { get; set; } = new();
        public Player Player { get; set; } = new();
        public int Turn { get; set; } = 1;
        public bool IsWin { get; set; } = false;
        public bool IsShowCorrectLine { get; set; } = false;
        public bool IsShowPlayerModal { get; set; } = false;
        public bool IsShowEndGame { get; set; } = false;

        private static System.Timers.Timer aTimer;
        private int CounterSecond = 0;
        public string Chrono { get; set; } = "00:00";

        protected override async Task OnInitializedAsync()
        {
            IsShowPlayerModal = true;
        }

        #region Start & restart
        public void NewGame()
        {
            IsWin = false;
            IsShowCorrectLine = false;

            StateHasChanged();

            Turn = 1;
            CounterSecond = 0;

            Answers = new();
            Indications = new();
            ColorsLine = new();

            Chrono = "00:00";

            GenerateArray();
            GenerateColorLine();

            Console.WriteLine($"{ColorsLine[0]} {ColorsLine[1]} {ColorsLine[2]} {ColorsLine[3]}");

            StartTimer();

        }

        public void RestartGame()
        {
            IsWin = false;
            IsShowCorrectLine = false;

            StateHasChanged();

            Turn = 1;
            CounterSecond = 0;

            Answers = new();
            Indications = new();
            ColorsLine = new();

            Chrono = "00:00";

            GenerateArray();
            GenerateColorLine();

            aTimer.Enabled = true;
        }
        #endregion

        #region Back To Menu
        public void BackToMenu()
        {
            NavigationManager.NavigateTo("/");
        }
        #endregion

        #region SetPlayer
        public void SetPlayer(Player player)
        {
            Player = player;

            IsShowPlayerModal = false;
            StateHasChanged();
            NewGame();
        }
        #endregion

        #region Finished Game

        public void FinishedGame()
        {
            StopTimer();
            IsShowEndGame = true;
        }

        public void FinishGameReturn()
        {
            IsShowEndGame = false;
            StateHasChanged();
        }
        #endregion

        #region Timer
        public void StartTimer()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += CountDownTimer;
            aTimer.Enabled = true;
        }
        public void CountDownTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            CounterSecond++;
            TimeSpan t = TimeSpan.FromSeconds(Convert.ToDouble(CounterSecond));
            Chrono = t.ToString(@"mm\:ss");
            StateHasChanged();
        }

        public void StopTimer()
        {
            aTimer.Enabled = false;
        }
        #endregion

        #region Insert & remove Color
        public void InsertColor(ColorEnum color)
        {
            int id = Answers.Where(x => x.Turn == Turn && x.Color == ColorEnum.grey).Select(x => x.Id).FirstOrDefault();
            if (id == 0 && Turn > 1)
                id = -1;
            List<Piece> pieces = Answers.Where(x => x.Turn == Turn).ToList();
            bool colorAlreadyUsed = pieces.Select(x => x.Color).Contains(color);
            if (id >= 0 && !colorAlreadyUsed)
            {
                Answers[id].Color = color;
                StateHasChanged();
            }
            else
            {
                Console.WriteLine("Ligne complète ou couleur déjà utilisée");
            }
        }

        public void RemoveColor(int id)
        {
            if (!IsShowCorrectLine)
            {
                Answers[id].Color = ColorEnum.grey;
                StateHasChanged();
            }
        }
        #endregion

        #region CheckLine & ReponseIndication
        public void CheckedLine()
        {
            List<Piece> userline = Answers.Where(x => x.Turn == Turn).ToList();
            int Greys = userline.Where(x => x.Color.Equals(ColorEnum.grey)).Count();

            if (Greys == 0)
            {
                bool isFinish = ResponseIndication(userline);
                if (isFinish)
                {
                    IsWin = true;
                    IsShowCorrectLine = true;
                    FinishedGame();
                }
                else
                {
                    if (Turn < 10)
                        Turn++;
                    else
                    {
                        IsWin = false;
                        IsShowCorrectLine = true;
                        FinishedGame();
                    }
                }

                StateHasChanged();
            }
            else
            {
                Console.WriteLine("Ligne incomplète");
            }
        }

        public bool ResponseIndication(List<Piece> userline)
        {
            int i = 0;
            foreach (var piece in userline)
            {
                if (piece.Color.ToString() == ColorsLine[i])
                {
                    Indications[piece.Id].Color = ColorEnum.white;
                    Indications[piece.Id].Priority = 1;
                }

                if (Indications[piece.Id].Color == ColorEnum.grey && ColorsLine.Contains(userline[i].Color.ToString()))
                {
                    Indications[piece.Id].Color = ColorEnum.black;
                    Indications[piece.Id].Priority = 2;
                }

                i++;
            }

            //Sort color : 1st white, 2sd black, 3th
            Indications = Indications.OrderBy(x => x.Turn).ThenBy(x => x.Priority).ToList();
            int whites = Indications.Where(x => x.Turn == userline[0].Turn && x.Color == ColorEnum.white).Count();
            if (whites == 4)
                return true;
            else
                return false;
        }
        #endregion

        #region Generate ColorLine & Array

        public void GenerateColorLine()
        {
            List<string> ColorCopy = new List<string>() { "red", "blue", "orange", "green", "yellow", "brown", "pink", "purple" };
            Random rnd = new Random();
            for (int i = 0; i < 4; i++)
            {
                int range = rnd.Next(ColorCopy.Count);
                ColorsLine.Add(ColorCopy[range]);
                ColorCopy.RemoveAt(range);
            }
        }

        public void GenerateArray()
        {
            int j = 0;
            int turn = 1;
            for (int i = 0; i <= 39; i++)
            {

                Piece piece = new()
                {
                    Id = i,
                    Color = ColorEnum.grey,
                    IsActive = false,
                    Turn = turn
                };
                Answers.Add(piece);

                Tips tips = new()
                {
                    Id = i,
                    Color = ColorEnum.grey,
                    IsActive = false,
                    Turn = turn
                };
                Indications.Add(tips);

                j++;

                if (j == 4)
                {
                    j = 0;
                    turn++;
                }
            }
        }
        #endregion

    }
}
