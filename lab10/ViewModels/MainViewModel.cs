using lab10.Models;
using lab10.Services;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace lab10.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;

        public ObservableCollection<Contact> Contacts { get; }

        private string _name = "";
        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }

        private string _phone = "";
        public string Phone
        {
            get => _phone;
            set => Set(ref _phone, value);
        }

        private Contact _selectedContact;
        public Contact SelectedContact
        {
            get => _selectedContact;
            set => Set(ref _selectedContact, value);
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public MainViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Contacts = new ObservableCollection<Contact>();

            AddCommand = new RelayCommand(AddContact, CanAddContact);
            DeleteCommand = new RelayCommand(DeleteContact, CanDeleteContact);
        }

        private void AddContact()
        {
            if (Contacts.Any(c => c.Phone == Phone))
            {
                _dialogService.ShowWarning("Контакт с таким номером уже существует!");
                return;
            }

            try
            {
                var contact = new Contact(Name, Phone);
                Contacts.Add(contact);

                _dialogService.ShowInfo("Контакт добавлен!");

                Name = "";
                Phone = "";
            }
            catch (Exception ex)
            {
                _dialogService.ShowError(ex.Message);
            }
        }

        private bool CanAddContact()
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   !string.IsNullOrWhiteSpace(Phone);
        }

        private void DeleteContact()
        {
            if (SelectedContact == null) return;

            if (_dialogService.ShowConfirmation("Удалить контакт?"))
            {
                Contacts.Remove(SelectedContact);
            }
        }

        private bool CanDeleteContact()
        {
            return SelectedContact != null;
        }
    }
}
