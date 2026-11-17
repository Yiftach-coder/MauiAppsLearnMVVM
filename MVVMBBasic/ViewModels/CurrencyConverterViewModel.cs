
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ViewModels
{
    public class CurrencyConverterViewModel : BaseViewModel
    {
        private string _amount = string.Empty;
        private string _selectedCurrency = "USD";
        private string _result = string.Empty;

        public List<string> Currencies { get; } = new List<string>
        {
            "USD",
            "EUR",
            "GBP"
        };

        public string Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged();
                }
            }
        }

        public string SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                if (_selectedCurrency != value)
                {
                    _selectedCurrency = value;
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

        public ICommand ConvertCommand { get; }

        public CurrencyConverterViewModel()
        {
            Title = "Currency Converter";

            ConvertCommand = new Command(async () => await ConvertCurrencyAsync());
        }

        private async Task ConvertCurrencyAsync()
        {
            if (!double.TryParse(Amount, out double amount) || amount < 0)
            {
                await ShowAlertAsync(
                    "Invalid Amount",
                    "Please enter a valid positive amount.",
                    "OK");

                return;
            }

            double exchangeRate = SelectedCurrency switch
            {
                "USD" => 3.70,
                "EUR" => 4.00,
                "GBP" => 4.70,
                _ => 0
            };

            if (exchangeRate == 0)
            {
                await ShowAlertAsync(
                    "Error",
                    "Please select a valid currency.",
                    "OK");

                return;
            }

            double convertedAmount = amount / exchangeRate;

            Result = $"{convertedAmount:N2} {SelectedCurrency}";
        }
    }
}
