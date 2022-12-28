﻿using System.ComponentModel;
using System.Windows.Input;
using Common;

/// <summary>
/// This is kept in the CommonLibrary because it is generic with the exception that:
/// 1) it has to be copied into the ViewModels folder of the project where it will be used, and
/// 2) after copying, the namespace must be updated to that of the view models; e.g., project_name.ViewModels
/// 3) the InitMessagePanel() must be called at the top of public MainVM()
/// 4) the MainVM() must be declared as a partial class
/// </summary>
namespace <project_name>.ViewModels
{
    partial class MainVM : INotifyPropertyChanged
    {
        public void InitBusyPanel()
        {
            BusyProgressMaximum = 100;
            BusyProgressMinimum = 1;

            BusyCancelCommand = new Command(BusyCancelAction);
        }

        #region Commands

        public ICommand BusyCancelCommand { get; set; }

        #endregion

        #region Actions

        private void BusyCancelAction(object obj)
        {
            if (bw != null && bw.WorkerSupportsCancellation)
            {
                ShowMessagePanel("Cancel!", "Action will terminate after the current operation.");
                bw.CancelAsync();
            }
        }

        #endregion

        #region Properties

        private string busyPanelVisibility = "Hidden";
        public string BusyPanelVisibility

        {
            get => busyPanelVisibility;
            set
            {
                busyPanelVisibility = value;
                NotifyPropertyChanged();
            }
        }

        private string busyTitle;
        public string BusyPanelTitle
        {
            get => busyTitle;
            set
            {
                busyTitle = value;
                NotifyPropertyChanged();
            }
        }

        private double busyProgressValue;
        public double BusyProgressValue
        {
            get => busyProgressValue;
            set
            {
                busyProgressValue = value;
                NotifyPropertyChanged();
            }
        }

        private double busyProgressMinimum;
        public double BusyProgressMinimum
        {
            get => busyProgressMinimum;
            set
            {
                busyProgressMinimum = value;
                NotifyPropertyChanged();
            }
        }

        private double busyProgressMaximum;
        public double BusyProgressMaximum
        {
            get => busyProgressMaximum;
            set
            {
                busyProgressMaximum = value;
                NotifyPropertyChanged();
            }
        }

        private string busyProgressText;
        public string BusyProgressText
        {
            get => busyProgressText;
            set
            {
                busyProgressText = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Methods

        private void ShowBusyPanel(string title)
        {
            BusyPanelTitle = title;
            BusyPanelVisibility = "Visible";
        }

        private void HideBusyPanel() => BusyPanelVisibility = "Hidden";

        #endregion

    }
}
