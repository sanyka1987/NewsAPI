using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApiWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        

            private ObservableCollection<Object> news;
            public ObservableCollection<Object> News { get { return news; } set { news = value; onPropertyChanged(nameof(News)); } }


            private string searchedName;

            public string SearchedName
            {
                get { return searchedName; }

                set
                {
                    searchedName = value;


                    News = new ObservableCollection<Object>(News.Where(x => x.Equals(value)).ToList());// тут поміняти обєкт на модель та посилання на проперті
                    onPropertyChanged();
                }
            }



        }
    }

