using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tareaIIIP.VModels;

namespace tareaIIIP.Vistas
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Empleados : ContentPage
    {
        public Empleados()
        {
            InitializeComponent();
            mostrar();


        }

        private async Task mostrar()
        {
            VMuser funcion = new VMuser();

            cole.ItemsSource = await funcion.mostrar_usuarios();
        }
        

    }
}