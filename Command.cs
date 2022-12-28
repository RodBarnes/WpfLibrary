using System;
using System.Windows.Input;

namespace Common
{
    /// <summary>
    /// This class simplifies the ability for Binding commands to WPF actions.  It supports commands from the View
    /// to the ViewModel including passing a parameter.  To avoid encountering conflicts between the command, action,
    /// and method signatures, it is recommended to use "[operation]Command", "[operation]Action", and "[operation]"
    /// for the respective Command, Action, and Method.  See the example, below.
    /// 
    /// XAML:
    ///   <Button Content="Load" Command="{Binding ShowPersonCommand}" CommandParameter="{Binding SelectedPerson}" />
    ///
    /// ViewModel:
    ///     public ICommand ShowPersonCommand { get; set; }
    ///      
    ///     ShowPersonCommand = new Command(ShowPersonAction);
    ///      
    ///     private void ShowPersonAction(object obj) => ShowPersion((Person)obj);
    ///      
    ///     private void ShowPerson(Person person) => MessageBox.Show("Person",$"The selected person is {person.FullName}");
    /// 
    /// </summary>
    public class Command : ICommand
    {
        private Action<object> execute;
        private Predicate<object> canExecute;
        private event EventHandler CanExecuteChangedInternal;

        public Command(Action<object> execute) : this(execute, DefaultCanExecute) { }

        public Command(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException("execute");
            this.canExecute = canExecute ?? throw new ArgumentNullException("canExecute");
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                CanExecuteChangedInternal += value;
            }

            remove
            {
                CommandManager.RequerySuggested -= value;
                CanExecuteChangedInternal -= value;
            }
        }

        public bool CanExecute(object parameter) => canExecute != null && canExecute(parameter);

        public void Execute(object parameter) => execute(parameter);

        public void OnCanExecuteChanged()
        {
            EventHandler handler = CanExecuteChangedInternal;
            if (handler != null)
            {
                //DispatcherHelper.BeginInvokeOnUIThread(() => handler.Invoke(this, EventArgs.Empty));
                handler.Invoke(this, EventArgs.Empty);
            }
        }

        public void Destroy()
        {
            canExecute = _ => false;
            execute = _ => { return; };
        }

        private static bool DefaultCanExecute(object parameter) => true;
    }
}
