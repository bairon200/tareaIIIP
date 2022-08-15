using Firebase.Database.Query;
using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tareaIIIP.Modelo;
using tareaIIIP.Servicio;

namespace tareaIIIP.VModels
{
    public class VMuser
    {

        string id;
        string rutafoto;

        public async Task<string> insertar_usuario(Mempleado parametros)
        {
            //child agregar o poder utilizar una tabla y PostAsync es para insertat datos a la tabla
            var data = await Conexionfirebase.firebase
                  .Child("empleados")
                  .PostAsync(new Mempleado()
                  {
                      id = parametros.id,
                      nombres = parametros.nombres,
                      apellidos = parametros.apellidos,
                      puesto = parametros.puesto,
                      icono = parametros.icono,
                      edad = parametros.edad,
                      direccion = parametros.direccion

                  });

            id = data.Key;
            return id;

        }


        public async Task<string> SubirImagenesStorage(Stream ImagenStream, string Idusuarios)
        {
            var dataAlmacenamiento = await new FirebaseStorage("tarea31-f250a.appspot.com")
                .Child("empleados")
                .Child(Idusuarios + ".jpg")
                .PutAsync(ImagenStream);
            rutafoto = dataAlmacenamiento;
            return rutafoto;
        }

        public async Task EditarFoto(Mempleado parametros)
        {
            var obtenerData = (await Conexionfirebase.firebase
                .Child("empleados") //comparamos si es la misma key
                .OnceAsync<Mempleado>()).Where(a => a.Key == parametros.id).FirstOrDefault();

            await Conexionfirebase.firebase
                .Child("empleados")
                .Child(obtenerData.Key)
                .PutAsync(new Mempleado()
                {

                    id = parametros.id,
                    nombres = parametros.nombres,
                    apellidos = parametros.apellidos,
                    puesto = parametros.puesto,
                    icono = parametros.icono,
                    edad = parametros.edad,
                    direccion = parametros.direccion


                });
        }




    }
}
