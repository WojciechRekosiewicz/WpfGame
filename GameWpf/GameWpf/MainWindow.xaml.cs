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
       

      DoubleAnimation doubleAnimation = new DoubleAnimation();

        private void GoodTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            dispatcherTimer.Start();
        
           
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            PlayerScore.Text = String.Format("{0} :X  {1} :Y ::: {2} : Height {3} : Width", Canvas.GetLeft(Ball1), Canvas.GetTop(Ball1), canvas.ActualWidth, canvas.ActualHeight) ;
            Canvas.SetTop(Ball1, Canvas.GetTop(Ball1) - ballY);
            Canvas.SetLeft(Ball1, Canvas.GetLeft(Ball1) - ballX);
            Pos.Text = String.Format("BallX : {0} BallY: {1} ///// Bar X :{2} Bar  Y: {3}", ballX, ballY, Canvas.GetLeft(Bar), Canvas.GetTop(Bar));




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

            Rect rect1 = new Rect(Canvas.GetLeft(Ball1), Canvas.GetTop(Ball1), Ball1.Width, Ball1.Height);
            Rect rect2 = new Rect(Canvas.GetLeft(Bar), Canvas.GetTop(Bar), Bar.Width, Bar.Height);
            if (rect1.IntersectsWith(rect2))
            {
                ballY = -ballY;
                ballY += 100;
            }

            if(Canvas.GetTop(Ball1) == Canvas.GetTop(Bar) && Canvas.GetLeft(Ball1) == Canvas.GetLeft(Bar))
            {
                ballY = -ballY;
                ballY += 10;
            }

            //if (rect1.Bounds.IntersectsWith(rect2.Bounds))
            //{
            //    ballY = -ballY;
            //}

            //if (rect1.renderedgeometry.bounds.intersectswith(rect2))
            //{
            //    ballY = -ballY;
            //    ballY += 100;
            //}

        }

        private void MenageAnimations()
        {
          
                MoveRight();
        }

        private void MoveRight()
        {

            //DoubleAnimation doubleAnimation = new DoubleAnimation();
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
            MenageAnimations();
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
                if (Bar.Margin.Left > 0)
                {
                    
                    Bar.Margin = new Thickness((Bar.Margin.Left - 10), 395, 0, 0);
                }
            }
            if (e.Key == Key.Right)
            {
                if (Bar.Margin.Left < 459)
                {
                   
                    Bar.Margin = new Thickness((Bar.Margin.Left + 10), 395, 0, 0);
                }
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}









