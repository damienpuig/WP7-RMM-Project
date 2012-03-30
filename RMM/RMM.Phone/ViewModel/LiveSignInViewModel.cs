using GalaSoft.MvvmLight;
using RMM.Phone.Execution;
using GalaSoft.MvvmLight.Command;
using System;
using System.Windows;
using Microsoft.Live.Controls;

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
        public RelayCommand<LiveConnectSessionChangedEventArgs> SessionStateChanged { get; set; }

        public LiveSignInViewModel()
        {
            SessionStateChanged = new RelayCommand<LiveConnectSessionChangedEventArgs>((e) => MessageBox.Show("session state changed : " + e.Status.ToString()));
           
        }
    }
}