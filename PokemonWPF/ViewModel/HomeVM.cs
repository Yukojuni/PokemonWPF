using PokemonWPF.Model;
using PokemonWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using CommunityToolkit.Mvvm.Input;

namespace PokemonWPF.ViewModel
{
    public partial class HomeVM : BaseVM
    {
        private ExerciceMonsterContext _context;
        //public RelayCommand ReloadCommand => new RelayCommand(execute => ReloadContext());
        private string connectionString = "Server=PC_DE_MARCINIAK\\SQLEXPRESS;Database=ExerciceMonster;Trusted_Connection=True;TrustServerCertificate=True;";
        public string ConnectionString
        {
            get { return connectionString; }
            set
            {
                connectionString = value;
                ReloadContext();
            }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }



        public ICommand RequestChangeViewCommand { get; set; }
        public ICommand ReloadCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public HomeVM()
        {
            ReloadCommand = new RelayCommand(ReloadContext);
            LoginCommand = new RelayCommand(LoginVerif);
            RegisterCommand = new RelayCommand(RegisterVerif);
        }

        //Override from BaseVM
        public override void OnShowVM()
        {
            //Display popup for information
            //MessageBox.Show("Main view display");
        }

        private void ReloadContext()
        {
            try
            {
                using (var testContext = new ExerciceMonsterContext(ConnectionString))
                {
                    testContext.Database.OpenConnection();
                    testContext.Database.CloseConnection();
                }
                _context?.Dispose();
                _context = new ExerciceMonsterContext(ConnectionString);
                MessageBox.Show($"Bien joué", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void LoginVerif()
        {
            if (_context == null)
            {
                MessageBox.Show("Base de données non connectée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Veuillez entrer un nom d'utilisateur et un mot de passe.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Hacher le mot de passe
                string hashedPassword = PasswordHelper.HashPasswordBase64(Password);

                // Rechercher dans la base de données
                var user = _context.Logins
                                   .FirstOrDefault(u => u.Username == Username && u.PasswordHash == hashedPassword);

                if (user != null)
                {
                    MessageBox.Show("Connexion réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                    MainWindowVM.OnRequestVMChange?.Invoke(new GameVM(connectionString));
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la vérification : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RegisterVerif()
        {
            if (_context == null)
            {
                MessageBox.Show("Base de données non connectée.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                MessageBox.Show("Veuillez entrer un nom d'utilisateur et un mot de passe.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Vérifier si l'utilisateur existe déjà
                var existingUser = _context.Logins
                                           .FirstOrDefault(u => u.Username == Username);

                if (existingUser != null)
                {
                    // L'utilisateur existe déjà
                    MessageBox.Show("Nom d'utilisateur déjà pris.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Hacher le mot de passe
                string hashedPassword = PasswordHelper.HashPasswordBase64(Password);

                // Créer un nouvel utilisateur
                var newUser = new Login
                {
                    Username = Username,
                    PasswordHash = hashedPassword
                };

                // Ajouter l'utilisateur à la base de données
                _context.Logins.Add(newUser);
                _context.SaveChanges();

                MessageBox.Show("Inscription réussie !", "Succès", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindowVM.OnRequestVMChange?.Invoke(new GameVM(connectionString));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'inscription : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static class PasswordHelper
        {
            public static string HashPasswordBase64(string password)
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(passwordBytes);
            }
        }
    }
}