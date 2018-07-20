using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sulimn.Classes.Items
{
    internal abstract class Consumable : Item
    {
        private int _restoreHealth, _restoreMagic;
        private bool _cures;

        /// <summary>Amount of health this <see cref="Consumable"/> restores.</summary>
        public int RestoreHealth
        {
            get => _restoreHealth;
            set
            {
                _restoreHealth = value;
                OnPropertyChanged("RestoreHealth");
            }
        }

        /// <summary>Amount of health this <see cref="Consumable"/> restores.</summary>
        public int RestoreMagic
        {
            get => _restoreMagic;
            set
            {
                _restoreMagic = value;
                OnPropertyChanged("RestoreMagic");
            }
        }

        /// <summary>Does this <see cref="Consumable"/> cure?</summary>
        public bool Cures
        {
            get => _cures;
            set
            {
                _cures = value;
                OnPropertyChanged("Cures");
            }
        }
    }
}