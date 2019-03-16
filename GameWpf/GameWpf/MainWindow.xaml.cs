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
        
        int speed = 5;
        int ballX = 5;
        int ballY = 5;
        int score = 0;
        bool pause = false;
       

     

        private void GoodTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dispatcherTimer.Start();

            if (pause)
            {
                MessageBox.Show("Stop");
                dispatcherTimer.Stop();
            }

        
           
        }

        public static bool CheckCollision(FrameworkElement a, FrameworkElement b)
        {
            Rect rect1 = new Rect((double)a.GetValue(Canvas.LeftProperty), (double)a.GetValue(Canvas.TopProperty), a.Width, a.Height);
            Rect rect2 = new Rect((double)b.GetValue(Canvas.LeftProperty), (double)b.GetValue(Canvas.TopProperty), b.Width, b.Height);

            if (rect1.IntersectsWith(rect2))
            {
                Console.WriteLine("true");
                return true;

            }

            else
            {
                Console.WriteLine("false");
                return false;
            }
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            PlayerScore.Text = String.Format("{0} :X  {1} :Y ::: {2} : Height {3} : Width, SCORE: {4}", Canvas.GetLeft(Ball1), Canvas.GetTop(Ball1), canvas.ActualWidth, canvas.ActualHeight, score) ;
            Canvas.SetTop(Ball1, Canvas.GetTop(Ball1) - ballY);
            Canvas.SetLeft(Ball1, Canvas.GetLeft(Ball1) - ballX);
            Pos.Text = String.Format("Bar X :{0} Bar  Y: {1} {2}", Canvas.GetLeft(Bar), Canvas.GetTop(Bar), pause);

            PauseHandler();

            if (CheckCollision(Ball1, Bar))
                {
                    ballY = -ballY;
                    score += 1;
                }
                if (Canvas.GetLeft(Ball1) <= 1)
                {
                    ballX = -ballX;
                }

                if (Canvas.GetLeft(Ball1) >= 770)
                {
                    ballX = -ballX;
                }

                if (Canvas.GetTop(Ball1) < 0 || Canvas.GetTop(Ball1) + Ball1.Height > 400)
                {
                    ballY = -ballY;
                }

                if (Canvas.GetTop(Ball1) >= 380)
                {
                    score -= 1;
                }
        }

        public void PauseHandler()
        {
            if (pause)
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("You want to continue?", "Game Paused",
                    System.Windows.MessageBoxButton.YesNo);
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

        private void MoveRight()
        {

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.To = 760;
            doubleAnimation.From = 0;
            doubleAnimation.AutoReverse = true;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = TimeSpan.FromSeconds(2);

            Ball.BeginAnimation(Canvas.LeftProperty, doubleAnimation);

        }


        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.PreviewKeyDown += new KeyEventHandler(HandleBar);
            this.PreviewKeyDown += new KeyEventHandler(Pause);

            MoveRight();
            GoodTimer();
            
        }

        private void Pause(object sender, KeyEventArgs e)
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
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation",
                    System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Close();
                }
            }
        }

        public void HandleBar(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left)
            {
                if(Canvas.GetLeft(Bar) > 0)
                {
                    Canvas.SetLeft(Bar, Canvas.GetLeft(Bar) - 10);
                }
            }
            if (e.Key == Key.Right)
            {
                if (Canvas.GetLeft(Bar) < 459)
                {
                    Canvas.SetLeft(Bar, Canvas.GetLeft(Bar) + 10);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e) { }
    }
}









