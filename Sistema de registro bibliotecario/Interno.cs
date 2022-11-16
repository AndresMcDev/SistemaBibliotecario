using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_registro_bibliotecario
{
    public partial class FrmInterno : Form
    {
        List<_Libro> MisLibros = new List<_Libro>();//Se crea una lista "MisLibros" al inicializar el formulario
        public FrmInterno()
        {
            
            InitializeComponent();

            string RutaJsonFile = File.ReadAllText("Libros.json");//Se obtiene en una variable el contenido del archivo Json que contiene los primeros libros
            DataTable dt = (DataTable)JsonConvert.DeserializeObject(RutaJsonFile, typeof(DataTable));//Se deseariliza la información en un dataTable "dt"
            
            for(int i =0; i<dt.Rows.Count; i++)
            {
                _Libro MiLibro = new _Libro(); //Se crean un objeto por cada row del dataTable 

                MiLibro.Libro = dt.Rows[i].ItemArray[0].ToString(); //Se llenan los campos del objeto con los rows del dataTable
                MiLibro.Autor = dt.Rows[i].ItemArray[1].ToString(); 
                MiLibro.Ubicacion = dt.Rows[i].ItemArray[2].ToString(); 
                MiLibro.Cantidad = dt.Rows[i].ItemArray[3].ToString(); 

                MisLibros.Add(MiLibro); //Se agrega el abjeto a la lista "MisLibros"
            }

            DtgLibros.DataSource = MisLibros;//Se actualiza el datagrid con la información de la lista "MisLibros"
        }

        private void BtnRegistrar_Click(object sender, EventArgs e)
        {
            _Libro MiLibro = new _Libro();//Se crea un objeto (Libro)

            
            if(ValidarNombre() == false)//Validar controles (que no esten vacios)
            {
                return;
            }
            if (ValidarAutor() == false)
            {
                return;
            }
            if (ValidarUbicacion() == false)
            {
                return;
            }
            if (ValidarCantidad() == false)
            {
                return;
            }
  
            MiLibro.Libro = TxtLibro.Text;//Dat valores a las propiedades del objeto
            MiLibro.Autor = TxtAutor.Text;
            MiLibro.Ubicacion = TxtUbicacion.Text;
            MiLibro.Cantidad = TxtCantidad.Text;
            
            MisLibros.Add(MiLibro);//Agregar el abjeto a la lista "MisLibros"
    
            DtgLibros.DataSource = null;//Actualizar datagrid con los objetos de lista
            DtgLibros.DataSource = MisLibros;
    
            LimpiarControles();//Metodo para limpiar controles
        }

        
        private _Libro ObtenerLibro(string nombre)//Metodo para obtener objeto que contiene el libro a buscar
        {
            return MisLibros.Find(libro => libro.Libro.Contains(nombre));
        }
        
        private _Libro ObtenerAutor(string autor)//Metodo para obtener objeto que contiene el autor a buscar
        {
            return MisLibros.Find(libro => libro.Autor.Contains(autor));
        }
        
        private void BtnBuscarAutor_Click(object sender, EventArgs e)//Metodo para consultar por autor
        {
            if (ValidarAutor2() == false)//Metodo para validar campo de busqueda Autor
            {
                return;
            }

            _Libro MiLibro = ObtenerAutor(TxtBuscarAutor.Text);//Variable que almacena el resultado del metodo ObtenerAutor (Busca el autor en los objetos de tipo Libro (retorna true o flase)
            
            if (MiLibro == null)//Si el autor no existe
            {
                EpvError.SetError(TxtBuscarAutor, "El libro no se encuentra registrado en la lista");
                LimpiarControles();
                return;
            }
            else//Si el autor existe LLena los controles con la información del objeto encontrado
            {
                EpvError.SetError(TxtBuscarAutor, "");
                TxtLibro.Text = MiLibro.Libro;
                TxtAutor.Text = MiLibro.Autor;
                TxtUbicacion.Text = MiLibro.Ubicacion;
                TxtCantidad.Text = MiLibro.Cantidad;
                MessageBox.Show("Libro encontrado", "Informació", MessageBoxButtons.OK, MessageBoxIcon.Information);//Muestra mensaje para confirmar que se encontro el libro
                TxtBuscarAutor.Clear();//Limpiar campos de busqueda
                TxtBuscarLibro.Clear();
            }
        }
        
        private void BtnBuscarLibro_Click(object sender, EventArgs e)//Metodo para consultar libro por nombre
        {
            if (ValidarNombre2() == false)//Metodo para validar campo de busqueda Nombre
            {
                return;
            }

            _Libro MiLibro = ObtenerLibro(TxtBuscarLibro.Text);//Variable que almacena el resultado del metodo ObtenerLibro(Busca el libro en los objetos de tipo Libro (retorna true o flase)
           
            if (MiLibro == null)//Si el autor no existe
            {
                EpvError.SetError(TxtBuscarLibro, "El libro no se encuentra registrado en la lista");
                LimpiarControles();
                return;
            }
            else//Si el libro existe LLena los controles con la información del objeto encontrado
            {
                EpvError.SetError(TxtBuscarLibro, "");
                TxtLibro.Text = MiLibro.Libro;
                TxtAutor.Text = MiLibro.Autor;
                TxtUbicacion.Text = MiLibro.Ubicacion;
                TxtCantidad.Text = MiLibro.Cantidad;

                MessageBox.Show("Libro encontrado", "Informació", MessageBoxButtons.OK, MessageBoxIcon.Information);//Muestra mensaje para confirmar que se encontro el libro
                TxtBuscarAutor.Clear();//Limpiar campos de busqueda
                TxtBuscarLibro.Clear();
            }
        }
        
        private bool ValidarCantidad()//Metodo para validar cantidad
        {
            if (string.IsNullOrEmpty(TxtCantidad.Text))
            {
                EpvError.SetError(TxtCantidad, "Debe ingresar la cantidad");
                return false;
            }
            else
            {
                EpvError.SetError(TxtCantidad, "");
                return true;
            }
        }
        
        private bool ValidarUbicacion()//Metodo para validar ubicacion
        {
            if (string.IsNullOrEmpty(TxtUbicacion.Text))
            {
                EpvError.SetError(TxtUbicacion, "Debe ingresar la ubicación");
                return false;
            }
            else
            {
                EpvError.SetError(TxtUbicacion, "");
                return true;
            }
        }

        
        private bool ValidarAutor()//Metodo para validar autor
        {
            if (string.IsNullOrEmpty(TxtAutor.Text))
            {
                EpvError.SetError(TxtAutor, "Debe ingresar un autor");
                return false;
            }
            else
            {
                EpvError.SetError(TxtAutor, "");
                return true;
            }
        }
        
        private bool ValidarAutor2()//Metodo para validar Autor2
        {
            if (string.IsNullOrEmpty(TxtBuscarAutor.Text))
            {
                EpvError.SetError(TxtBuscarAutor, "Debe ingresar un Autor");
                return false;
            }
            else
            {
                EpvError.SetError(TxtBuscarAutor, "");
                return true;
            }
        }
       
        private bool ValidarNombre() //Metodo para validar nombre
        {
            if (string.IsNullOrEmpty(TxtLibro.Text))
            {
                EpvError.SetError(TxtLibro, "Debe ingresar un Libro");
                return false;
            }
            else
            {
                EpvError.SetError(TxtLibro, "");
                return true;
            }
        }
        
        private bool ValidarNombre2()//Metodo para validar nombre2
        {
            if (string.IsNullOrEmpty(TxtBuscarLibro.Text))
            {
                EpvError.SetError(TxtBuscarLibro, "Debe ingresar un Libro");
                return false;
            }
            else
            {
                EpvError.SetError(TxtBuscarLibro, "");
                return true;
            }
        }
     
        private void LimpiarControles()//Metodo para limpiar controles
        {
            TxtLibro.Clear();
            TxtAutor.Clear();
            TxtUbicacion.Clear();
            TxtCantidad.Clear();
            TxtLibro.Focus();
        }
        //Metodo para regresar a la ventana de Logeo
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            frm1.Show();
            this.Hide();
        }
    }
}
