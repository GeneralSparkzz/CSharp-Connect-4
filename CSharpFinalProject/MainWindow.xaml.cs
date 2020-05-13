using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CSharpFinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static int tileCount = 42, Rows = 6, Cols = 7, player_one_score_total = 0, player_two_score_total = 0;
        private static bool AIGame, processingMove = false;
        private byte player = 1;

        Button[] ColButtons = new Button[7];
        Canvas[,] GridTiles = new Canvas[Rows, Cols];
        byte[,] GridMove = new byte[Rows, Cols];
        public MainWindow(bool isAI)
        {
            InitializeComponent();
            win_box.Visibility = Visibility.Hidden;
            AIGame = isAI;

            if (AIGame)
            {
                player_two_title.Content = "Computer Player";
                player_two_score_title.Content = "Computer Player";
            }

            player_one_background.Background = Brushes.Yellow;
            player_two_background.Background = Brushes.Gray;

            ColButtons[0] = btn_col_0;
            ColButtons[1] = btn_col_1;
            ColButtons[2] = btn_col_2;
            ColButtons[3] = btn_col_3;
            ColButtons[4] = btn_col_4;
            ColButtons[5] = btn_col_5;
            ColButtons[6] = btn_col_6;
            int count = 1;

            for(int y = 0; y < Rows; y++)
            {
                for(int x = 0; x < Cols; x++)
                {
                    //Console.WriteLine("Loading attempt for " + count + " at: " + x + "," + y);
                    GridTiles[y, x] = (Canvas)this.FindName("_" + count);
                    GridMove[y,x] = 0;
                    count++;
                }
            }
        }

        public void ProcessMove(int col)
        {
            SubProcessMove(col);
            if (AIGame && player == 2)
            {
                FindAIMove();
            }
        }
        private void SubProcessMove(int col)
        {
            if (processingMove == false)
            {
                processingMove = true;
                //Console.WriteLine("Processing move: ");
                SetColButtonsState(false);
                for (int y = Rows - 1; y >= 0; y--)
                {
                    //Console.WriteLine("Checking: " + col + "," + y);
                    //Thread.Sleep(100);
                    if (GridMove[y, col] != 0)
                    {
                        if (y != Rows - 1)
                        {
                            GridMove[y + 1, col] = player;
                            if (player == 1)
                            {
                                GridTiles[y + 1, col].Background = Brushes.Yellow;
                            }
                            else
                            {
                                GridTiles[y + 1, col].Background = Brushes.Red;
                            }
                            y = -1;
                        }
                        else
                        {
                            //Console.WriteLine("Can't make that play");
                            y = -1;
                        }
                    }
                    else if (GridMove[y, col] == 0 && y == 0)
                    {
                        GridMove[y, col] = player;
                        if (player == 1)
                        {
                            GridTiles[y, col].Background = Brushes.Yellow;
                        }
                        else
                        {
                            GridTiles[y, col].Background = Brushes.Red;
                        }
                        y = -1;
                    }
                    else
                    {

                    }
                }
                if (WinCheck(player))
                {
                    if (player == 1)
                    {
                        player_one_score_total++;
                        win_title.Content = "Player One Wins!";
                        win_box.Background = Brushes.Yellow;

                    }
                    else if(player == 2)
                    {
                        player_two_score_total++;
                        if (AIGame)
                        {
                            win_title.Content = "Computer Wins!";
                        }
                        else
                        {
                            win_title.Content = "Player Two Wins!";
                        }
                        win_box.Background = Brushes.Red;
                        player = 1;
                    }

                    player_one_score.Content = player_one_score_total.ToString();
                    player_two_score.Content = player_two_score_total.ToString();
                    win_box.Visibility = Visibility.Visible;
                }
                else
                {
                    if (AIGame == false)
                    {
                        if (player == 1)
                        {
                            player_one_background.Background = Brushes.Gray;
                            player_two_background.Background = Brushes.Red;
                            player = 2;
                        }
                        else
                        {
                            player_one_background.Background = Brushes.Yellow;
                            player_two_background.Background = Brushes.Gray;
                            player = 1;
                        }
                        SetColButtonsState(true);
                    }
                    else
                    {
                        if (player == 1)
                        {
                            player_one_background.Background = Brushes.Gray;
                            player_two_background.Background = Brushes.Red;
                            player = 2;
                        }
                        else
                        {

                            player_one_background.Background = Brushes.Yellow;
                            player_two_background.Background = Brushes.Gray;
                            player = 1;
                        }
                        SetColButtonsState(true);
                    }
                }
                processingMove = false;
            }
        }
        private bool WinCheck(int PlayerMove)
        {
            bool Victory = false;
            int Match = 0;

            // Vertical Check
            for(int y = 0; y < Rows; y++)
            {
                for(int x = 0; x < Cols; x++)
                {
                    //Console.WriteLine("Win check on: " + y + "," + x + " - GridMove: " + GridMove[y,x] + " Match: " + Match);
                    if (Match == 4)
                    {
                        Victory = true;
                        y = Rows + 1;
                        x = Cols + 1;
                    }
                    else if (GridMove[y,x] == PlayerMove)
                    {
                        Match++;
                    }
                    else
                    {
                        Match = 0;
                    }
                }
            }
            if (Victory == false)
            {
                // Horizontal Check
                for (int x = 0; x < Cols; x++)
                {
                    for (int y = 0; y < Rows; y++)
                    {
                        //Console.WriteLine("Win check on: " + y + "," + x + " - GridMove: " + GridMove[y, x] + " Match: " + Match);
                        if (Match == 4)
                        {
                            Victory = true;
                            y = Rows + 1;
                            x = Cols + 1;
                        }
                        else if (GridMove[y, x] == PlayerMove)
                        {
                            Match++;
                        }
                        else
                        {
                            Match = 0;
                        }
                    }
                }
            }

            // Left '/' Check
            if(Victory == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int y = i, x = 0; y < Rows && x < Cols; y++, x++)
                    {
                        if (Match == 4)
                        {
                            Victory = true;
                            y = Rows + 1;
                            x = Cols + 1;
                        }
                        else if (GridMove[y, x] == PlayerMove)
                        {
                            Match++;
                        }
                        else
                        {
                            Match = 0;
                        }
                        //GridTiles[y, x].Background = Brushes.Violet;
                    }
                }
            }

            // Bottom '/' Check
            if (Victory == false)
            {
                for(int i = 1; i < 4; i++)
                {
                    for(int y = 0, x = i; y < Rows && x < Cols; x++, y++)
                    {
                        if (Match == 4)
                        {
                            Victory = true;
                            y = Rows + 1;
                            x = Cols + 1;
                        }
                        else if (GridMove[y, x] == PlayerMove)
                        {
                            Match++;
                        }
                        else
                        {
                            Match = 0;
                        }
                        //GridTiles[y, x].Background = Brushes.Blue;
                    }
                }
            }

            // Right '\' Check
            if (Victory == false)
            {
                for(int i = 0; i < 3; i++)
                {
                    for(int y = i, x = Cols-1; y < Rows && x >= 0; y++, x--)
                    {
                        if (Match == 4)
                        {
                            Victory = true;
                            y = Rows + 1;
                            x = Cols + 1;
                        }
                        else if (GridMove[y, x] == PlayerMove)
                        {
                            Match++;
                        }
                        else
                        {
                            Match = 0;
                        }
                        //GridTiles[y, x].Background = Brushes.BlueViolet;
                    }
                }
            }

            // Bottom '\' Check
            if (Victory == false)
            {

                for (int i = 0; i < 3; i++)
                {
                    for (int y = 0, x = Cols - (i + 2); y < Rows && x >= 0; y++, x--)
                    {
                        if (Match == 4)
                        {
                            Victory = true;
                            y = Rows + 1;
                            x = Cols + 1;
                        }
                        else if (GridMove[y, x] == PlayerMove)
                        {
                            Match++;
                        }
                        else
                        {
                            Match = 0;
                        }
                        //GridTiles[y, x].Background = Brushes.Brown;
                    }
                }
            }

            return Victory;
        }

        private void SetColButtonsState(bool active)
        {
            for(int i = 0; i < ColButtons.Length; i++)
            {
                ColButtons[i].IsEnabled = active;
            }
        }
        private void Reset_Game_Click(object sender, RoutedEventArgs e)
        {
            for (int y = 0; y < Rows; y++)
            {
                for (int x = 0; x < Cols; x++)
                {
                    GridTiles[y, x].Background = Brushes.Gray;
                    GridMove[y, x] = 0;
                }
            }
            SetColButtonsState(true);
            win_box.Visibility = Visibility.Hidden;
            player_one_background.Background = Brushes.Yellow;
            player_two_background.Background = Brushes.Gray;
            processingMove = false;
            player = 1;
        }
        private void FindAIMove()
        {
            bool run = true;
            int Match = 0, MatchCheck = 3;
            Random rand = new Random();
            ProcessMove(rand.Next(0,6));
        }
        private void Btn_col_0_Click(object sender, RoutedEventArgs e)
        {
            ProcessMove(0);
        }

        private void Btn_col_1_Click(object sender, RoutedEventArgs e)
        {
            ProcessMove(1);
        }

        private void Btn_col_2_Click(object sender, RoutedEventArgs e)
        {
            ProcessMove(2);
        }

        private void Btn_col_3_Click(object sender, RoutedEventArgs e)
        {
            ProcessMove(3);
        }

        private void Btn_col_4_Click(object sender, RoutedEventArgs e)
        {
            ProcessMove(4);
        }

        private void Btn_col_5_Click(object sender, RoutedEventArgs e)
        {
            ProcessMove(5);
        }

        private void Btn_col_6_Click(object sender, RoutedEventArgs e)
        {
            ProcessMove(6);
        }
        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window1 MM = new Window1();
            MM.Show();
            e.Cancel = false;
        }
    }
}
