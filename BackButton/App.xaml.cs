using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BackButton
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            var sysNavMan = SystemNavigationManager.GetForCurrentView();
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }

            //----------------------------------------------------------------------------------------
            // The first thing you need to do to enable the system back button, is to actually
            // opt in to show it. There is no mechanism to automatically show the back button
            // only when the navigation frame acutally can go back, so we need to take care of
            // showing the back button only when the frame we're using for navigation has at least
            // one page in it's history stack, i.e. the CanGoBack property returns true.
            rootFrame.Navigated += (s2, e2) =>
            {
                sysNavMan.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;
                if (rootFrame.CanGoBack)
                {
                    sysNavMan.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
                }
            };
            //----------------------------------------------------------------------------------------



            if (rootFrame.Content == null)
            {
                rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }





            //----------------------------------------------------------------------------------------
            // The second thing you need to take care of is to handle back navigation. We do this
            // as an inline anonymous event handler, so that we can make use of the already
            // initialized 'rootFrame' variable, which we can assume is not null in the event handler.
            sysNavMan.BackRequested += (s2, e2) =>
            {
                if(rootFrame.CanGoBack && !e2.Handled)
                {
                    e2.Handled = true;
                    rootFrame.GoBack();
                }
            };
            //----------------------------------------------------------------------------------------

            Window.Current.Activate();
        }
    }
}
