
using System.Windows.Input;

namespace ViewModels
{
    public class WelcomeViewModel : BaseViewModel
    {
        public ICommand NavigateToAgeCalculatorCommand { get; }
        public ICommand NavigateToMathCalculatorCommand { get; }

        public WelcomeViewModel()
        {
            Title = "Welcome";

            NavigateToAgeCalculatorCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//CalcAge");
            });

            NavigateToMathCalculatorCommand = new Command(async () =>
            {
                await Shell.Current.GoToAsync("//CalculatorPage");
            });
        }
    }
}
