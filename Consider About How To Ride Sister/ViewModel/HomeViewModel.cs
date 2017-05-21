using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consider_About_How_To_Ride_Sister.ViewModel
{
    class HomeViewModel:BaseViewModel
    {
        private ObservableCollection<Model.Contentlist> videoresult;
        public ObservableCollection<Model.Contentlist> VideoResult
        {
            get
            {
                return videoresult;
            }
            set
            {
                videoresult = value;
                RaisePropertyChanged(nameof(VideoResult));
            }
        }
    }
}
