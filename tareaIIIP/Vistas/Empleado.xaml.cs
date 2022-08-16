using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tareaIIIP.VModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tareaIIIP.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empleado : ContentPage
    {
        public Empleado()
        {
            InitializeComponent();
        }

      
        private async void listaclick_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Empleados());
        }

       
    }
}