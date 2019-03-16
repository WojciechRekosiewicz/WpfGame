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
       

     

        private void GoodTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        
           
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
            PlayerScore.Text = String.Format("{0} :X  {1} :Y ::: {2} : Height {3} : Width", Canvas.GetLeft(Ball1), Canvas.GetTop(Ball1), canvas.ActualWidth, canvas.ActualHeight) ;
            Canvas.SetTop(Ball1, Canvas.GetTop(Ball1) - ballY);
            Canvas.SetLeft(Ball1, Canvas.GetLeft(Ball1) - ballX);
            Pos.Text = String.Format("BallX : {0} BallY: {1} ///// Bar X :{2} Bar  Y: {3}", ballX, ballY, Canvas.GetLeft(Bar), Canvas.GetTop(Bar));


            
                if (CheckCollision(Ball1, Bar))
                {
                    ballY = -ballY;
                  //  ballY += 5;
                }
                if (Canvas.GetLeft(Ball1) <= 1)
                {

                    ballX = -ballX;
                    // ballX -= 2;
                }

                if (Canvas.GetLeft(Ball1) >= 770)
                {

                    ballX = -ballX;
                    //  ballX += 2;
                }

                if (Canvas.GetTop(Ball1) < 0 || Canvas.GetTop(Ball1) + Ball1.Height > 400)
                {
                    ballY = -ballY;
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
            MoveRight();
            GoodTimer();
            
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









