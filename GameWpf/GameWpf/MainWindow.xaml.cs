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
        bool goRight;
        bool goLeft;
        int speed = 5;
        int ballX = 5;
        int ballY = 5;
        int score = 0;
        int cpuPoint = 0;

        DoubleAnimation doubleAnimation = new DoubleAnimation();
        DispatcherTimer dTimer;

        private void GoodTimer()
        {
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 20);
            dispatcherTimer.Start();
        
           
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            PlayerScore.Text = "" + score;
            Canvas.SetTop(Ball1, Canvas.GetTop(Ball1) - ballY);
            Canvas.SetLeft(Ball1, Canvas.GetLeft(Ball1) - ballX);
           
            if (Canvas.GetLeft(Ball1) < 0)
            {
                Canvas.SetLeft(Ball1, 370);
                ballX = -ballX;
                ballX -= 2;

            }

            if (Canvas.GetTop(Ball1) + Ball1.Width > 800)
            {
                ballY = -ballY;
            }

            if (Canvas.GetLeft(Ball1) < 0)
            {
                Canvas.SetLeft(Ball1, 434);
                ballX = -ballX;
                ballX -= 2;
            }

            if (Canvas.GetTop(Ball1) < 0 || Canvas.GetTop(Ball1) + Ball1.Width < 80)
            {
                Canvas.SetLeft(Ball1, 434);
                ballX = -ballX;
                ballX -= 2;
            }


            if (Canvas.GetLeft(Ball1) + Ball1.Width < 50)
            {
                Canvas.SetLeft(Ball1, 434);
                ballX = -ballX;
                ballX -= 2;
            }
            //if (Ball1.RenderedGeometry.Bounds.IntersectsWith(Bar))
            //{
            // ballx = -ballx;
            //}



        }


        private void InitTimer()
        {
            dTimer = new DispatcherTimer();
            //dTimer.Tick += new EventHandler(TickdTimer);
            dTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            dTimer.Start();
        }

    
        private void MenageAnimations()
        {
          
                MoveRight();
        
        }

        private void MoveUp()
        {

        }
        private void MoveDown()
        {

        }
        private void MoveLeft()
        {
            //DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.To = 0;
            doubleAnimation.From = 750;
            doubleAnimation.Duration = TimeSpan.FromSeconds(20);

            Ball.BeginAnimation(LeftProperty, doubleAnimation);
            
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
            //RectAnimationExample();
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












//public void RectAnimationExample()
//{

//     Create a NameScope for this page so that
//     Storyboards can be used.
//    NameScope.SetNameScope(this, new NameScope());

//    RectangleGeometry myRectangleGeometry = new RectangleGeometry();
//    myRectangleGeometry.Rect = new Rect(0, 200, 100, 100);

//     Assign the geometry a name so that
//     it can be targeted by a Storyboard.
//    this.RegisterName(
//        "MyAnimatedRectangleGeometry", myRectangleGeometry);

//    Path myPath = new Path();
//    myPath.Fill = Brushes.LemonChiffon;
//    myPath.StrokeThickness = 1;
//    myPath.Stroke = Brushes.Black;
//    myPath.Data = myRectangleGeometry;

//    RectAnimation myRectAnimation = new RectAnimation();
//    myRectAnimation.Duration = TimeSpan.FromSeconds(2);
//    myRectAnimation.FillBehavior = FillBehavior.HoldEnd;

//     Set the animation to repeat forever. 
//    myRectAnimation.RepeatBehavior = RepeatBehavior.Forever;

//     Set the From and To properties of the animation.
//    myRectAnimation.From = new Rect(0, 200, 100, 100);
//    myRectAnimation.To = new Rect(0, 50, 200, 50);

//     Set the animation to target the Rect property
//     of the object named "MyAnimatedRectangleGeometry."
//    Storyboard.SetTargetName(myRectAnimation, "MyAnimatedRectangleGeometry");
//    Storyboard.SetTargetProperty(
//        myRectAnimation, new PropertyPath(RectangleGeometry.RectProperty));

//     Create a storyboard to apply the animation.
//    Storyboard ellipseStoryboard = new Storyboard();
//    ellipseStoryboard.Children.Add(myRectAnimation);

//     Start the storyboard when the Path loads.
//    myPath.Loaded += delegate (object sender, RoutedEventArgs e)
//    {
//        ellipseStoryboard.Begin(this);
//    };


//    Can1.Children.Add(myPath);

//    Content = Can1;
//}
