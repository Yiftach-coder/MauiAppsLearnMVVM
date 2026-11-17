
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ViewModels
{
    public class CurrencyConverterViewModel : BaseViewModel
    {
        private string _amount = string.Empty;
        private string _sourceCurrency = "ILS";
        private string _targetCurrency = "USD";
        private string _result = string.Empty;

        public List<string> Currencies { get; } = new List<string>
        {
            "ILS",
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

        public string SourceCurrency
        {
            get => _sourceCurrency;
            set
            {
                if (_sourceCurrency != value)
                {
                    _sourceCurrency = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TargetCurrency
        {
            get => _targetCurrency;
            set
            {
                if (_targetCurrency != value)
                {
                    _targetCurrency = value;
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
            if (!double.TryParse(Amount, out double amount) || amount <= 0)
            {
                await ShowAlertAsync(
                    "Invalid Amount",
                    "Please enter a valid amount greater than zero.",
                    "OK");

                return;
            }

            double sourceRate = GetExchangeRate(SourceCurrency);
            double targetRate = GetExchangeRate(TargetCurrency);

            if (sourceRate == 0 || targetRate == 0)
            {
                await ShowAlertAsync(
                    "Error",
                    "Please select valid currencies.",
                    "OK");

                return;
            }

            double convertedAmount = amount * sourceRate / targetRate;

            Result = $"{convertedAmount:N2} {TargetCurrency}";
        }

        private double GetExchangeRate(string currency)
        {
            return currency switch
            {
                "ILS" => 1.00,
                "USD" => 3.70,
                "EUR" => 4.00,
                "GBP" => 4.70,
                _ => 0
            };
        }
    }
}
