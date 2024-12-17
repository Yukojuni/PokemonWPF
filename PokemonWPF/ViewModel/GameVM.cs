using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using PokemonWPF.Model;
using System.Collections.ObjectModel;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;
using Microsoft.EntityFrameworkCore;
using PokemonWPF.View;

namespace PokemonWPF.ViewModel
{
    public class GameVM : BaseVM
    {
        private readonly ExerciceMonsterContext _context;
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand UnselectCommand { get; set; }
        public ICommand SortByMonsterCommand { get; set; }
        public ICommand FightCommand { get; }

        public GameVM(string connectionString)
        {
            _context = new ExerciceMonsterContext(connectionString);
            Monsters = new ObservableCollection<Monster>(_context.Monsters.Include(m => m.Spells).ToList());
            Spells = new ObservableCollection<Spell>(_context.Spells.ToList());
            SortedSpellsByMonster = new ObservableCollection<Spell>(_context.Spells.ToList());
            AddCommand = new RelayCommand(AddMonster);
            DeleteCommand = new RelayCommand(DeleteMonster, CanDeleteMonster);
            UnselectCommand = new RelayCommand(UnselectMonster);
            SortByMonsterCommand = new RelayCommand(SortSpellsByMonster);
            FightCommand = new RelayCommand(() => OpenFight(connectionString));
        }

        public ObservableCollection<Monster> Monsters { get; set; }
        public ObservableCollection<Spell> Spells { get; set; }
        public ObservableCollection<Spell> SortedSpellsByMonster { get; set; }

        #region MonstersControl

        private Monster _selectedMonster;
        public Monster SelectedMonster
        {
            get { return _selectedMonster; }
            set
            {
                _selectedMonster = value;
                OnPropertyChanged();
                // Mettre à jour l'état du bouton Delete
                ((RelayCommand)DeleteCommand).NotifyCanExecuteChanged();
            }
        }

        private void AddMonster()
        {
            Monster newMonster = new Monster
            {
                Name = "New Monster",
                Health = 100,
                ImageUrl = "https://vignette.wikia.nocookie.net/pokemon/images/a/a3/Salam%C3%A8che-EdC.png/revision/latest?cb=20160527163621&path-prefix=fr",
                Spells = new List<Spell>() // Aucun sort par défaut
            };

            _context.Monsters.Add(newMonster);
            _context.SaveChanges();

            Monsters.Add(newMonster);
        }

        private void DeleteMonster()
        {
            if (SelectedMonster != null)
            {
                _context.Monsters.Remove(SelectedMonster);
                _context.SaveChanges();

                Monsters.Remove(SelectedMonster);
            }
        }

        private bool CanDeleteMonster()
        {
            return SelectedMonster != null;
        }

        private void UnselectMonster()
        {
            SelectedMonster = null;
        }

        #endregion

        #region SpellsControl

        private string spellName;
            public string SpellName
            {
                get { return spellName; }
                set
                {
                    spellName = value;
                    OnPropertyChanged();
                }
            }

            private int spellDamage;
            public int SpellDamage
            {
                get { return spellDamage; }
                set
                {
                    spellDamage = value;
                    OnPropertyChanged();
                }
            }

            private string spellDescription;
            public string SpellDescription
            {
                get { return spellDescription; }
                set
                {
                    spellDescription = value;
                    OnPropertyChanged();
                }
            }

            private Spell selectedSpell;
            public Spell SelectedSpell
            {
                get { return selectedSpell; }
                set
                {
                    selectedSpell = value;
                    OnPropertyChanged();
                    UpdateSpellDetails();
                }
            }

        private void UpdateSpellDetails()
        {
            if (SelectedSpell != null)
            {
                SpellName = SelectedSpell.Name;
                SpellDamage = SelectedSpell.Damage;
                SpellDescription = SelectedSpell.Description;
            }
        }
        private void SortSpellsByMonster()
        {
        }
        #endregion

        private void OpenFight(string connectionString)
        {
            if (SelectedMonster == null)
            {
                MessageBox.Show("Select a monster first!");
                return;
            }

            MainWindowVM.OnRequestVMChange?.Invoke(new FightVM(SelectedMonster, connectionString));
            return;
        }
        public override void OnShowVM()
            {
                MessageBox.Show("Gameview show");
            }
    }

}
