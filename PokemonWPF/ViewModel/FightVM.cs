using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using PokemonWPF.Model;
using RelayCommand = CommunityToolkit.Mvvm.Input.RelayCommand;

namespace PokemonWPF.ViewModel
{
    public class FightVM : BaseVM
    {
        private readonly Random _random = new Random();
        private Monster _playerMonster;
        private Monster _enemyMonster;
        private readonly ExerciceMonsterContext _context;


        public FightVM(Monster playerMonster, string connectionString)
        {
            _context = new ExerciceMonsterContext(connectionString);
            PlayerMonster = playerMonster;
            EnemyMonster = GenerateEnemyMonster();

            FightCommand = new RelayCommand(Fight);
            NewFightCommand = new RelayCommand(PrepareNewFight);

            PlayerHealth = PlayerMonster.Health;
            EnemyHealth = EnemyMonster.Health;

            BattleLog = "Prepare for battle!";
        }

        public ICommand FightCommand { get; set; }
        public ICommand NewFightCommand { get; set; }


        public Monster PlayerMonster
        {
            get => _playerMonster;
            set
            {
                _playerMonster = value;
                OnPropertyChanged();
            }
        }

        public Monster EnemyMonster
        {
            get => _enemyMonster;
            set
            {
                _enemyMonster = value;
                OnPropertyChanged();
            }
        }

        private int _playerHealth;
        public int PlayerHealth
        {
            get => _playerHealth;
            set
            {
                _playerHealth = value;
                OnPropertyChanged();
            }
        }

        private int _enemyHealth;
        public int EnemyHealth
        {
            get => _enemyHealth;
            set
            {
                _enemyHealth = value;
                OnPropertyChanged();
            }
        }

        private Spell _selectedSpell;
        public Spell SelectedSpell
        {
            get => _selectedSpell;
            set
            {
                _selectedSpell = value;
                OnPropertyChanged();
            }
        }

        private string _battleLog;
        public string BattleLog
        {
            get => _battleLog;
            set
            {
                _battleLog = value;
                OnPropertyChanged();
            }
        }


        #region Fight

        private void Fight()
        {
            if (SelectedSpell == null)
            {
                BattleLog = "Please select a spell before attacking!";
                return;
            }

            EnemyHealth -= SelectedSpell.Damage;
            BattleLog = $"{PlayerMonster.Name} used {SelectedSpell.Name} and put {SelectedSpell.Damage} damage!\n";

            if (EnemyHealth <= 0)
            {
                BattleLog += $"{EnemyMonster.Name} is dead! You won!";
                return;
            }
            var enemySpell = EnemyMonster.Spells.ToList()[_random.Next(EnemyMonster.Spells.Count)];
            PlayerHealth -= enemySpell.Damage;
            BattleLog += $"{EnemyMonster.Name} used {enemySpell.Name} and put {enemySpell.Damage} damage!\n";

            if (PlayerHealth <= 0)
            {
                BattleLog += $"{PlayerMonster.Name} is dead! Game Over!";
            }
        }

        private void PrepareNewFight()
        {
            EnemyMonster = GenerateEnemyMonster();
            EnemyHealth = EnemyMonster.Health;
            PlayerHealth = PlayerMonster.Health;
            BattleLog = "A new enemy monster has appeared!";
        }

        private Monster GenerateEnemyMonster()
        {
            var existingMonsters = FetchAllMonsters();
            if (!existingMonsters.Any())
                throw new InvalidOperationException("No monsters!");

            var baseMonster = existingMonsters[_random.Next(existingMonsters.Count)];
            return new Monster
            {
                Name = baseMonster.Name,
                Health = baseMonster.Health,
                ImageUrl = baseMonster.ImageUrl,
                Spells = baseMonster.Spells
            };
        }

        private ObservableCollection<Monster> FetchAllMonsters()
        {
            var Monsters = new ObservableCollection<Monster>(_context.Monsters.Include(m => m.Spells).ToList());
            return Monsters;
        }

        #endregion
    }
}
