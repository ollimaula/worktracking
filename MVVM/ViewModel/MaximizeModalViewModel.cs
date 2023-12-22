using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TTC8440.Core;

namespace TTC8440.MVVM.ViewModel
{
    internal class MaximizeModalViewModel
    {
        private RelayCommand _close_modal;
        public ICommand CloseModal
        {
            get
            {
                if (_close_modal == null)
                {
                    _close_modal = new RelayCommand(PerformCloseModal);
                }

                return _close_modal;
            }
        }

        private void PerformCloseModal(object commandParameter)
        {
            Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive).Close();
        }
    }
}
