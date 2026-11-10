
using System;
using System.Windows.Input;

namespace ViewModels
{
    public class CalculatorViewModel : BaseViewModel
    {
        private string _number1 = string.Empty;
        private string _number2 = string.Empty;
        private string _result = "0";

        public string Number1
        {
            get => _number1;
            set
            {
                if (_number1 != value)
                {
                    _number1 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Number2
        {
            get => _number2;
            set
            {
                if (_number2 != value)
                {
                    _number2 = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Result
        {
            get => _result;
            set
            {
                if (_result != value)
                {
                    _result = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddCommand { get; }
        public ICommand SubtractCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }

        public CalculatorViewModel()
        {
            Title = "Calculator";

            AddCommand = new Command(async () => await CalculateAsync("+"));
            SubtractCommand = new Command(async () => await CalculateAsync("-"));
            MultiplyCommand = new Command(async () => await CalculateAsync("*"));
            DivideCommand = new Command(async () => await CalculateAsync("/"));
        }

        private async Task CalculateAsync(string operation)
        {
            bool isNum1Valid = double.TryParse(Number1, out double num1);
            bool isNum2Valid = double.TryParse(Number2, out double num2);

            if (!isNum1Valid || !isNum2Valid)
            {
                await ShowAlertAsync(
                    "Invalid Input",
                    "Please enter valid numbers in both fields.",
                    "OK");

                return;
            }

            double result;

            switch (operation)
            {
                case "+":
                    result = num1 + num2;
                    break;

                case "-":
                    result = num1 - num2;
                    break;

                case "*":
                    result = num1 * num2;
                    break;

                case "/":
                    if (num2 == 0)
                    {
                        await ShowAlertAsync(
                            "Error",
                            "Cannot divide by zero!",
                            "OK");

                        Result = "Error";
                        return;
                    }

                    result = num1 / num2;
                    break;

                default:
                    return;
            }

            Result = result.ToString();
        }
    }
}
