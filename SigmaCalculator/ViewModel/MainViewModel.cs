using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SigmaCalculator.Services;

namespace SigmaCalculator.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly Calculator calculator;
        public MainViewModel(Calculator calculator) 
        {
            this.calculator = calculator;
        }

        [ObservableProperty]
        string text;

        [RelayCommand]
        void Calculate()
        {
            var calculateResult = calculator.EvaluateExpression(text);
            Text = calculateResult;
        }

        [RelayCommand]
        void Add(string s)
        {
            Text += s;
        }

        [RelayCommand]
        void AS(string s)
        {
            Text = "";
        }

    }
}
