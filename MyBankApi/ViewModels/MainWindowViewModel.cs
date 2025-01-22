using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;

namespace MyBankApi.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        // Свойства для команды и ошибок (если нужно)
        [ObservableProperty]
        private string _currencyName = string.Empty;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private string _selectedCurrency = string.Empty;

        [ObservableProperty]
        private string _errorMessage;

        // Команды для действий
        public ICommand DepositWalletCommand { get; }
        public ICommand ConvertCurrencyCommand { get; }
        public ICommand AddCurrencyCommand { get; }
        public ICommand ViewBalanceCommand { get; }
        public ICommand ViewTransactionHistoryCommand { get; }

        // Конструктор
        public MainWindowViewModel()
        {
            DepositWalletCommand = new RelayCommand(ExecuteDepositWallet);
            ConvertCurrencyCommand = new RelayCommand(ExecuteConvertCurrency);
            AddCurrencyCommand = new RelayCommand(ExecuteAddCurrency);
            ViewBalanceCommand = new RelayCommand(ExecuteViewBalance);
            ViewTransactionHistoryCommand = new RelayCommand(ExecuteViewTransactionHistory);
        }

        // Логика для пополнения кошелька
        private void ExecuteDepositWallet()
        {
            if (Amount <= 0)
            {
                ErrorMessage = "Сумма должна быть больше нуля!";
                return;
            }
            
            // Логика пополнения кошелька
            ErrorMessage = string.Empty; // Очистить ошибку, если пополнение прошло успешно
        }

        // Логика для конвертации валют
        private void ExecuteConvertCurrency()
        {
            if (Amount <= 0)
            {
                ErrorMessage = "Сумма должна быть больше нуля!";
                return;
            }

            // Логика конвертации валюты
            ErrorMessage = string.Empty; // Очистить ошибку, если конвертация прошла успешно
        }

        // Логика для добавления валюты
        private void ExecuteAddCurrency()
        {
            if (string.IsNullOrWhiteSpace(CurrencyName))
            {
                ErrorMessage = "Название валюты не может быть пустым!";
                return;
            }

            if (Amount <= 0)
            {
                ErrorMessage = "Сумма валюты должна быть больше нуля!";
                return;
            }

            // Логика добавления валюты
            ErrorMessage = string.Empty; // Очистить ошибку, если добавление валюты прошло успешно
        }

        // Логика для просмотра баланса
        private void ExecuteViewBalance()
        {
            // Логика для отображения баланса
        }

        // Логика для просмотра истории транзакций
        private void ExecuteViewTransactionHistory()
        {
            // Логика для отображения истории транзакций
        }

        // Свойство для приветственного сообщения
        public string Greeting => "Добро пожаловать!";
    }
}
