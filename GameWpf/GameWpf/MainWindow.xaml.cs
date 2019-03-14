﻿using System;
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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.PreviewKeyDown += new KeyEventHandler(HandleBar);
            AnimateBall();
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

        public void AnimateBall()
        {
        
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.To = 760;
            doubleAnimation.From = 0;
            doubleAnimation.Duration = TimeSpan.FromSeconds(2);

            Ball.BeginAnimation(LeftProperty, doubleAnimation);

            //doubleAnimation.Completed += (s, e) =>
            //{
            //    DoubleAnimation doubleAnimation1 = new DoubleAnimation();
            //    doubleAnimation1.From = 760;
            //    doubleAnimation1.To = 0;
            //    doubleAnimation1.Duration = TimeSpan.FromSeconds(2);

            //    Ball.BeginAnimation(LeftProperty, doubleAnimation);

            //};
        }


        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
