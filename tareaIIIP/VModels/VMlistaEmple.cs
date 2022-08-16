using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tareaIIIP.Modelo;
using Xamarin.Forms;

namespace tareaIIIP.VModels
{
    public class VMlistaEmple : baseModeloEmpleados
    {

        private ObservableCollection<Modelo.Mempleado> ListaAlumnos;
        private Modelo.Mempleado _alumno;

        public ObservableCollection<Modelo.Mempleado> GetListAlumnos
        {
            get { return ListaAlumnos; }
            set { ListaAlumnos = value; OnPropertyChange(); }
        }

        public Modelo.Mempleado GetAlumno
        {
            get { return _alumno; }
            set { _alumno = value; OnPropertyChange(); }
        }

        public ICommand DetallesCommand { set; get; }

        public INavigation Navigation { set; get; }

        //Constructor de la classe
        public VMlistaEmple(INavigation navigation)
        {
            Navigation = navigation;
            DetallesCommand = new Command<Type>(async (pageType) => await LlenarDetalle(pageType));

            GetListAlumnos = new ObservableCollection<Modelo.Mempleado>();
            mostrarUsuarios();


        }

        public VMlistaEmple()
        {
            
            GetListAlumnos = new ObservableCollection<Modelo.Mempleado>();
            mostrarUsuarios();


        }

        private async Task mostrarUsuarios()
        {
            VMuser funcion = new VMuser();
            var dt1 = await funcion.mostrar_usuarios();
       
            foreach (var t in dt1)
            {
                GetListAlumnos.Add(new Modelo.Mempleado()
                {
                    id = t.id,
                    nombres = t.nombres,
                    apellidos = t.apellidos,
                    edad = t.edad,
                    puesto = t.puesto,
                    direccion = t.direccion,
                    icono = t.icono,    
                    
                }) ;
            }
        }
            

        async Task LlenarDetalle(Type pageType)
        {

            if (GetAlumno != null)
            {
                var pagina = (Page)Activator.CreateInstance(pageType);

                pagina.BindingContext = new VMempleado()
                {
                    id = GetAlumno.id,
                    nombres = GetAlumno.nombres,
                    apellidos = GetAlumno.apellidos,
                    direccion = GetAlumno.direccion,
                    edad = GetAlumno.edad,
                    puesto = GetAlumno.puesto,
                    icono = GetAlumno.icono
                };

                await Navigation.PushAsync(pagina);
            }
        }

    }
}
