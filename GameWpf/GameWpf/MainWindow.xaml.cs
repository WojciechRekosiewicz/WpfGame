﻿ using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GameWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        int ballX = 5;
        int ballY = 5;
        int score = 0;
        bool pause = false;

        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc) + new KeyEventHandler(BarMovment) + new KeyEventHandler(PauseKey);
            BallAnimation();
            Timer();
        }

        private void Timer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += GameLogic;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();
        }

        private void GameLogic(object sender, EventArgs e)
        {
            BallLogic();
            Score();
            PauseHandler();  
        }

        private void Score()
        {
            PlayerScore.Text = String.Format("SCORE: {0}", score);
            //  PlayerScore.Text = String.Format("{0} :X  {1} :Y ::: {2} : Height {3} : Width, SCORE: {4}", Canvas.GetLeft(Ball), Canvas.GetTop(Ball), canvas.ActualWidth, canvas.ActualHeight, score);
            //  Pos.Text = String.Format("Bar X :{0} Bar  Y: {1} {2}", Canvas.GetLeft(Bar), Canvas.GetTop(Bar), pause);
        }

        public void BallLogic()
        {
            Canvas.SetTop(Ball, Canvas.GetTop(Ball) - ballY);
            Canvas.SetLeft(Ball, Canvas.GetLeft(Ball) - ballX);
            if (CheckCollisionBallWithBar(Ball, Bar))
            {
                ballY = -ballY;
                score += 1;
            }
            if (Canvas.GetLeft(Ball) <= 1)
            {
                ballX = -ballX;
            }

            if (Canvas.GetLeft(Ball) >= 770)
            {
                ballX = -ballX;
            }

            if (Canvas.GetTop(Ball) < 0 || Canvas.GetTop(Ball) + Ball.Height > 400)
            {
                ballY = -ballY;
            }

            if (Canvas.GetTop(Ball) >= 380)
            {
                GameOver();
            }
        }

        public static bool CheckCollisionBallWithBar(FrameworkElement a, FrameworkElement b)
        {
            Rect rect1 = new Rect((double)a.GetValue(Canvas.LeftProperty), (double)a.GetValue(Canvas.TopProperty), a.Width, a.Height);
            Rect rect2 = new Rect((double)b.GetValue(Canvas.LeftProperty), (double)b.GetValue(Canvas.TopProperty), b.Width, b.Height);

            if (rect1.IntersectsWith(rect2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GameOver()
        {
            MessageBoxResult result = MessageBox.Show($"SCORE : {score} You want to start again?", "Game Over!",
                                          MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                Application.Current.Shutdown();
            }
            else if(result == MessageBoxResult.Yes)
            {
                score = 0;
                Canvas.SetTop(Ball, 150);
                Canvas.SetLeft(Ball, 150);
            }
        }

        public void PauseHandler()
        {
            if (pause)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("You want to continue?", "Game Paused",
                    MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.No)
                {
                    Close();
                }
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    pause = false;
                }
            }
        }

        private void PauseKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (!pause)
                {
                    pause = true;
                }
                else
                {
                    pause = false;
                }
            }
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("You want to quit the game?", "Quit",
                    System.Windows.MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Close();
                }

            }
        }

        public void BarMovment(object sender, KeyEventArgs e)
        {
            int MoveUnit = 10;
            int LeftSide = 0;
            int RightSide = 540;

            if(e.Key == Key.Left)
            {
                if(Canvas.GetLeft(Bar) > LeftSide)
                {
                    Canvas.SetLeft(Bar, Canvas.GetLeft(Bar) - MoveUnit);
                }
            }
            if (e.Key == Key.Right)
            {
                if (Canvas.GetLeft(Bar) < RightSide)
                {
                    Canvas.SetLeft(Bar, Canvas.GetLeft(Bar) + MoveUnit);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) { }

        //TO DELETE
        private void BallAnimation()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.To = 760;
            doubleAnimation.From = 0;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            Ball2.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }
    }
}