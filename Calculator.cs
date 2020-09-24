using System;
using Xamarin.Forms;

namespace MortgageCalculator
{
    class Calculator : MainViewModel
    {
        public Command calculateCommand { get; set; }
        public Command resetViewCommand { get; set; }

        private float _loanAmount;
        public float loanAmount
        {
            get => _loanAmount;
            set { _loanAmount = value; OnPropertyChanged(nameof(loanAmount)); }
        }

        private float _interestRate;
        public float interestRate
        {
            get => _interestRate;
            set { _interestRate = value; OnPropertyChanged(nameof(interestRate)); }
        }

        private int _paymentPeriod;
        public int paymentPeriod
        {
            get => _paymentPeriod;
            set { _paymentPeriod = value; OnPropertyChanged(nameof(paymentPeriod)); }
        }

        private string _result;
        public string result
        {
            get => _result;
            set { _result = value; OnPropertyChanged(nameof(result)); }
        }

        public Calculator()
        {
            calculateCommand = new Command(calculateMortgage);
            resetViewCommand = new Command(resetView);
        }

        void calculateMortgage()
        {
            if (areInputsEmpty())
            {
                result = "Please, fill in correct values";
                return;
            }

            float i = (interestRate / 100 / 12);
            float v = 1 / (1 + i);

            double A = (i * loanAmount) / (1 - Math.Pow(v, paymentPeriod * 12));

            result = $"Monthly payment {Math.Round(A, 2)} Kč," +
                $" You will pay in total {Math.Round((A * paymentPeriod * 12), 2)} Kč.";
        }

        void resetView()
        {
            result = String.Empty;
            interestRate = 0;
            loanAmount = 0;
            paymentPeriod = 0;
        }

        private bool areInputsEmpty()
        {
            if (interestRate <= 0 || loanAmount <= 0 || paymentPeriod <= 0) 
                return true;

            return false;
        }
    }
}
