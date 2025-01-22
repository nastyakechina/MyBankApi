using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyBankApi.Services;
using System.Windows.Input;

namespace MyBankApi.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly ApiService _apiService;

        public MainWindowViewModel() : this(ServiceLocator.ApiService)
        {
        }

        public MainWindowViewModel(ApiService apiService)
        {
            _apiService = apiService;

            DepositWalletCommand = new AsyncRelayCommand(ExecuteDepositWalletAsync);
            ConvertCurrencyCommand = new AsyncRelayCommand(ExecuteConvertCurrencyAsync);
            AddCurrencyCommand = new AsyncRelayCommand(ExecuteAddCurrencyAsync);
            ViewBalanceCommand = new AsyncRelayCommand(ExecuteViewBalanceAsync);
            ViewTransactionHistoryCommand = new AsyncRelayCommand(ExecuteViewTransactionHistoryAsync);
            ShowDepositFormCommand = new RelayCommand(() => IsDepositFormVisible = true);
            HideDepositFormCommand = new RelayCommand(() => IsDepositFormVisible = false);
        }

        [ObservableProperty]
        private string _currencyName = string.Empty;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private string _selectedCurrency = string.Empty;

        [ObservableProperty]
        private string _errorMessage;

        [ObservableProperty]
        private decimal _walletBalance;

        private bool _isDepositFormVisible;
        public bool IsDepositFormVisible
        {
            get => _isDepositFormVisible;
            set => SetProperty(ref _isDepositFormVisible, value);
        }

        [ObservableProperty]
        private bool _isMainMenuVisible = true;

        public ICommand DepositWalletCommand { get; }
        public ICommand ConvertCurrencyCommand { get; }
        public ICommand AddCurrencyCommand { get; }
        public ICommand ViewBalanceCommand { get; }
        public ICommand ViewTransactionHistoryCommand { get; }
        public ICommand ShowDepositFormCommand { get; }
        public ICommand HideDepositFormCommand { get; }
        
        

        private async Task ExecuteDepositWalletAsync()
        {
            ErrorMessage = string.Empty; // Очистка ошибки перед началом
            try
            {
                if (Amount <= 0)
                {
                    ErrorMessage = "Сумма должна быть больше нуля!";
                    return;
                }

                if (string.IsNullOrWhiteSpace(SelectedCurrency))
                {
                    ErrorMessage = "Необходимо выбрать валюту!";
                    return;
                }

                // Отладочная информация
                Console.WriteLine($"Попытка пополнить кошелек. Валюта: {SelectedCurrency}, Сумма: {Amount}");

                await _apiService.DepositAsync(SelectedCurrency, Amount);

                ErrorMessage = "Пополнение выполнено!";
                IsDepositFormVisible = false; // Возврат на главный экран
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка: {ex.Message}";
                Console.WriteLine($"Ошибка выполнения команды: {ex.Message}");
            }
        }


        private async Task ExecuteConvertCurrencyAsync()
        {
            if (Amount <= 0)
            {
                ErrorMessage = "Сумма должна быть больше нуля!";
                return;
            }

            try
            {
                await _apiService.ConvertCurrencyAsync(Guid.Empty, SelectedCurrency, "USD", Amount);
                ErrorMessage = "Конвертация выполнена!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка: {ex.Message}";
            }
        }

        private async Task ExecuteAddCurrencyAsync()
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

            try
            {
                await _apiService.AddCoinAsync(CurrencyName, Amount);
                ErrorMessage = "Валюта добавлена!";
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка: {ex.Message}";
            }
        }

        private async Task ExecuteViewBalanceAsync()
        {
            try
            {
                var balance = await _apiService.GetWalletBalanceAsync(Guid.Empty);
                WalletBalance = balance;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка: {ex.Message}";
            }
        }

        private async Task ExecuteViewTransactionHistoryAsync()
        {
            try
            {
                var transactions = await _apiService.GetTransactionHistoryAsync();
                // Здесь можно реализовать логику отображения транзакций
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка: {ex.Message}";
            }
        }

        public string Greeting => "Добро пожаловать!";
    }
}
