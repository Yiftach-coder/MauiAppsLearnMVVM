
using ViewModels;

namespace Views
{
    public partial class CurrencyConverterPage : ContentPage
    {
        public CurrencyConverterPage() : this(new CurrencyConverterViewModel())
        {
        }

        public CurrencyConverterPage(CurrencyConverterViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
