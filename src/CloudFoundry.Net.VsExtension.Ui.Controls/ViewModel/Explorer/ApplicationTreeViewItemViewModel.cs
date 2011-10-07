﻿namespace CloudFoundry.Net.VsExtension.Ui.Controls.ViewModel
{
    using System.Windows.Input;
    using CloudFoundry.Net.Types;
    using CloudFoundry.Net.Vmc;
    using CloudFoundry.Net.VsExtension.Ui.Controls.Utilities;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;

    public class ApplicationTreeViewItemViewModel : TreeViewItemViewModel
    {
        private Application application;
        public RelayCommand<MouseButtonEventArgs> OpenApplicationCommand { get; private set; }
        public RelayCommand StartApplicationCommand { get; private set; }
        public RelayCommand StopApplicationCommand { get; private set; }
        public RelayCommand RestartApplicationCommand { get; private set; }
        
        public ApplicationTreeViewItemViewModel(Application application, CloudTreeViewItemViewModel parentCloud) : base(parentCloud, false)
        {
            OpenApplicationCommand = new RelayCommand<MouseButtonEventArgs>(OpenApplication);
            StartApplicationCommand = new RelayCommand(StartApplication, CanStart);
            StopApplicationCommand = new RelayCommand(StopApplication, CanStop);
            RestartApplicationCommand = new RelayCommand(RestartApplication, CanStop);

            this.application = application;
            var manager = new VcapClient(this.Application.Parent);
            var stats = manager.GetStats(this.application);
            foreach (StatInfo statInfo in stats)
            {
                base.Children.Add(new InstanceTreeViewItemViewModel(statInfo, this));
            }
        }

        public string Name
        {
            get { return this.application.Name; }
        }

        public Application Application
        {
            get { return this.application; }
        }

        private void OpenApplication(MouseButtonEventArgs e)
        {
            if (e == null || e.ClickCount >= 2)
                Messenger.Default.Send(new NotificationMessage<Application>(this, this.application, Messages.OpenApplication));
        }

        private bool CanStart()
        {
            return application.CanStart;
        }

        private bool CanStop()
        {
            return application.CanStop;
        }

        private void StartApplication()
        {
            Messenger.Default.Send(new NotificationMessage<Application>(this, this.application, Messages.StartApplication));
        }

        private void StopApplication()
        {
            Messenger.Default.Send(new NotificationMessage<Application>(this, this.application, Messages.StopApplication));
        }
        private void RestartApplication()
        {
            Messenger.Default.Send(new NotificationMessage<Application>(this, this.application, Messages.RestartApplication));
        }
    }
}