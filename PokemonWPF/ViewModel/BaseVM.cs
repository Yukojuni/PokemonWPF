using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonWPF.ViewModel
{
    abstract public class BaseVM : ObservableObject
    {

        // Virtual because want override on child
        public virtual void OnShowVM() { }
        public virtual void OnHideVM() { }


    }
}
