using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using tareaIIIP.Modelo;
using Xamarin.Forms;

namespace tareaIIIP.VModels
{
    public class VMempleado : baseModeloEmpleados
    {


        private string id1;
        private string nombres1;
        private string apellidos1;
        private string edad1;
        private string direccion1;
        private string icono1;
        private string puesto1;

         string key1;
        MediaFile file;
        ImageSource image;

        string rutafoto="0";

        public string id
        {
            get { return id1; }
            set { this.id1 = value; OnPropertyChange(); }
        }

        public string nombres
        {
            get { return nombres1; }
            set { this.nombres1 = value; OnPropertyChange(); }
        }

        public string apellidos
        {
            get { return apellidos1; }
            set { this.apellidos1 = value; OnPropertyChange(); }
        }

        public string edad
        {
            get { return edad1; }
            set { this.edad1 = value; OnPropertyChange(); }
        }

        public string direccion
        {
            get { return direccion1; }
            set { this.direccion1 = value; OnPropertyChange(); }
        }
        public string icono
        {
            get { return icono1; }
            set { this.icono1 = value; OnPropertyChange(); }
        }
        public string puesto
        {
            get { return puesto1; }
            set { this.puesto1 = value; OnPropertyChange(); }
        }

        public ICommand CleanCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand fotoCommand { get; set; }

        void limpiar()
        {
            id = "";
            nombres = "";
            apellidos = "";
            edad = "";
            direccion = "";
            icono = "";
            puesto = "";
        }



        async void AddAlumn()
        {

                VMuser funcion = new VMuser();
                Mempleado parametros = new Mempleado();

                parametros.nombres = nombres;
                parametros.apellidos = apellidos;
                parametros.puesto = puesto;
                parametros.icono = "-";
                parametros.direccion = direccion;
                parametros.edad = edad;

                key1 = await funcion.insertar_usuario(parametros);


           await SubirImagenesStore();

           await EditarFoto();

        }
         void foto()
        {
            foto2();
          
           
        }

        public async void foto2()
        {

            await CrossMedia.Current.Initialize();
            try
            {
                //para poder acceder a la galeria y agregar la img que queramos  NOTA: NIVEL LOCAL aun no se sube la imagen
                file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                {

                    return;
                }
                else
                {

                    image = ImageSource.FromStream(() =>
                    {
                        //GetStream extrae toda la ruta de la imagen
                        var rutaImagen = file.GetStream();

                        return rutaImagen;
                    });

                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
      
        }


        private async Task SubirImagenesStore()
        {
            VMuser funcion = new VMuser();
            rutafoto = await funcion.SubirImagenesStorage(file.GetStream(), key1);

        }
        private async Task EditarFoto()
        {
            VMuser funcion = new VMuser();
            Mempleado parametros = new Mempleado();

            parametros.nombres = nombres;
            parametros.apellidos = apellidos;
            parametros.puesto = puesto;
            parametros.icono = rutafoto;
            parametros.direccion = direccion;
            parametros.edad = edad;
            parametros.id = key1;

            await funcion.EditarFoto(parametros);

        }


        public VMempleado()
        {
            CleanCommand = new Command(() => limpiar());
            AddCommand = new Command(() => AddAlumn());
            fotoCommand = new Command(() => foto());
        }

    }
}
