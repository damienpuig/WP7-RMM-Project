using GalaSoft.MvvmLight;
using RMM.Phone.Execution;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using Microsoft.Live.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Live;

namespace RMM.Phone.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class LiveSignInViewModel : BugnionReverseViewModelBase
    {
        /// <summary>
        /// The applications client ID.
        /// </summary>
        private static readonly string ClientId = "00000000400A0117";

        /// <summary>
        /// The applications redirect URI (does not need to exist).
        /// </summary>
        private static readonly string RedirectUri = "https://oauth.live.com/desktop";

        public RelayCommand SauvegarderCommand { get; set; }
        public RelayCommand RestaurerCommand { get; set; }

        public bool IsActionPossible { get; set; }
        public LiveConnectSession Session { get; set; }
        

        public RelayCommand<LiveConnectSessionChangedEventArgs> SessionStateChanged { get; set; }

        public LiveSignInViewModel()
        {
            SessionStateChanged = new RelayCommand<LiveConnectSessionChangedEventArgs>((e) => 
                {
                    
                    OnSessionStateChanged(e);
                    MessageBox.Show("session state changed : " + e.Status.ToString());
                } );


            SauvegarderCommand = new RelayCommand(() => SauvegarderAction(), () =>
            {
                if (IsActionPossible)
                    return true;

                return false;
            });

            RestaurerCommand = new RelayCommand(() => RestaurerAction(), () =>
            {
                if (IsActionPossible)
                    return true;

                return false;
            });

        }

        public void OnSessionStateChanged(LiveConnectSessionChangedEventArgs evenementDeConnection)
        {
            if (evenementDeConnection.Status != LiveConnectSessionStatus.Connected)
            {
                IsActionPossible = false;
            }
            else
            {
                IsActionPossible = true;
                Session = evenementDeConnection.Session;
            }

            SauvegarderCommand.RaiseCanExecuteChanged();
            RestaurerCommand.RaiseCanExecuteChanged();
        }


        private bool InternetIsAvailable()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                MessageBox.Show("No internet connection is available.  Try again later.");
                return false;
            }
            return true;
        }

        private void SauvegarderAction()
        {
            MessageBox.Show("BOOM");
        }

        private void RestaurerAction()
        {
            NavigateTo("/View/Restaurer.xaml", null);
        }

        public override void Cleanup()
        {

            var aC = new LiveAuthClient(ClientId, RedirectUri);
            aC.Logout();
            base.Cleanup();
        }
    }
}