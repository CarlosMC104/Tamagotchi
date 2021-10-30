using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace TamagotchiDefinitivo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string nombre;
        DispatcherTimer t1;
        double decremento = 2.0;
        double puntuacion = 0.0;
        int contadorHambre = 0;
        int contadorCansancio = 0;
        int contadorDiversion = 0;
        int controlLogroBeberCerveza = 0;
        int controlLogroSaludo = 0;
        int controlChupete = 0;
        int controlSaxo = 0;

        public MainWindow()
        {
            InitializeComponent();
            VentanaBienvenida pantallaInicio = new VentanaBienvenida(this);
            pantallaInicio.ShowDialog();
            t1 = new DispatcherTimer();
            t1.Interval = TimeSpan.FromMilliseconds(2000.0);
            t1.Tick += new EventHandler(reloj);
            t1.Start();

        }

        private void reloj(object sender, EventArgs e)
        {
            this.pbHambre.Value -= decremento;
            this.pbCansancio.Value -= decremento;
            this.pbDiversion.Value -= decremento;
            puntuacion += 1.0;
            if (puntuacion == 20.0)
            {
                Storyboard sbaux = (Storyboard)this.Resources["animacionJugabilidad1"];
                sbaux.Begin();



            }
            if (pbHambre.Value == 100.0 && pbDiversion.Value == 100.0 && pbCansancio.Value <= 100.0)
            {
                MessageBoxResult resultado = MessageBox.Show("Has conseguido tener a Homer con las 3 barras llenas ", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
                LGPerfecto.Background = Brushes.Cyan;
            }
            if (pbCansancio.Value <= 35.0)
            {
                pbCansancio.Foreground = Brushes.Red;
                Storyboard sbaux = (Storyboard)this.Resources["animacionTenerSueño"];
                sbaux.Begin();
            }
            else
            {
                pbCansancio.Foreground = Brushes.Green;
                Storyboard sbaux = (Storyboard)this.Resources["animacionTenerSueño"];
                sbaux.Remove();
            }

            if (pbDiversion.Value <= 35.0)
            {
                pbDiversion.Foreground = Brushes.Red;
                Storyboard sbaux = (Storyboard)this.Resources["animacionAburrido"];
                sbaux.Begin();
            }
            else
            {
                pbDiversion.Foreground = Brushes.Green;
                Storyboard sbaux = (Storyboard)this.Resources["animacionAburrido"];
                sbaux.Remove();
            }
            if (pbHambre.Value <= 35.0)
            {
                pbHambre.Foreground = Brushes.Red;
                Storyboard sbaux = (Storyboard)this.Resources["animacionTenerHambre"];
                sbaux.Begin();
            }
            else
            {
                pbHambre.Foreground = Brushes.Green;
                Storyboard sbaux = (Storyboard)this.Resources["animacionTenerHambre"];
                sbaux.Remove();
            }
            if (pbHambre.Value == 100.0 && pbDiversion.Value == 100.0 && pbCansancio.Value == 100.0)
            {
                MessageBoxResult resultado = MessageBox.Show("Has conseguido las 3 barras a 100\n", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
                LGPerfecto.Background = Brushes.Cyan;
            }

            if (pbHambre.Value <= 0.0 || pbDiversion.Value <= 0.0 || pbCansancio.Value <= 0.0)
            {
                t1.Stop();
                this.lblGameOver.Visibility = Visibility.Visible;
                txtPuntuacion.Text = "Tu puntuación ha sido: " + puntuacion;
                tbNombreJugador.Visibility = Visibility.Hidden;
                this.txtPuntuacion.Visibility = Visibility.Visible;
                LVRanking.Items.Add(nombre + " Puntos: " + puntuacion + " NEW");
                this.btnComer.IsEnabled = false;
                this.btnDescansar.IsEnabled = false;
                this.btnJugar.IsEnabled = false;

            }
        }


        private void btnComer_Click(object sender, RoutedEventArgs e)
        {
            contadorHambre += 1;
            if (contadorHambre == 10)
            {
                MessageBoxResult resultado = MessageBox.Show("Has conseguido alimentar a Homer 10 veces\n", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
                LGComer.Background = Brushes.Cyan;
            }

            this.pbHambre.Value += 10;
            decremento += 0.25;
            btnComer.IsEnabled = false;
            Storyboard sbaux = (Storyboard)this.Resources["animacionComer"];
            sbaux.Completed += new EventHandler(FinComer);
            sbaux.Begin();
            if (imFondo.Source == imFondoMoe.Source && controlLogroBeberCerveza == 0)
            {
                MessageBoxResult resultado = MessageBox.Show("Has bebido en el bar de Moe\n", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
                LGMoe.Background = Brushes.Cyan;
                this.pbHambre.Value += 15;
                controlLogroBeberCerveza = 1;

            }

        }

        private void btnDescansar_Click(object sender, RoutedEventArgs e)
        {
            contadorCansancio += 1;
            if (contadorCansancio == 10)
            {
                MessageBoxResult resultado = MessageBox.Show("Has conseguido dormir a Homer 10 veces\n", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
                LGDescansar.Background = Brushes.Cyan;
                this.pbCansancio.Value += 25;

                this.pbHambre.Value += 25;
            }
            this.pbCansancio.Value += 20;
            decremento += 0.25;
            btnDescansar.IsEnabled = false;

            Storyboard sbaux = (Storyboard)this.Resources["animacionDormir"];
            sbaux.Completed += new EventHandler(FinCerrarOjoDrch);
            sbaux.Begin();


            DoubleAnimation cerrarOjoDrch = new DoubleAnimation();
            cerrarOjoDrch.To = 16;
            cerrarOjoDrch.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            cerrarOjoDrch.AutoReverse = true;

            DoubleAnimation cerrarOjoIzq = new DoubleAnimation();
            cerrarOjoIzq.To = 16;
            cerrarOjoIzq.Duration = new Duration(TimeSpan.FromMilliseconds(500));
            cerrarOjoIzq.AutoReverse = true;

            cerrarOjoIzq.Completed += new EventHandler(FinCerrarOjoDrch);
            OjoDrch.BeginAnimation(Ellipse.HeightProperty, cerrarOjoDrch);
            OjoIzq.BeginAnimation(Ellipse.HeightProperty, cerrarOjoIzq);

        }

        private void btnJugar_Click(object sender, RoutedEventArgs e)
        {
            contadorDiversion += 1;
            if (contadorDiversion == 10)
            {
                MessageBoxResult resultado = MessageBox.Show("Has conseguido jugar con Homer 10 veces\n", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
                LGJugar.Background = Brushes.Cyan;
                this.pbDiversion.Value += 10;

            }

            this.pbDiversion.Value += 10;
            decremento += 0.5;
            btnJugar.IsEnabled = false;
            Storyboard sbaux = (Storyboard)this.Resources["animacionJugar"];
            sbaux.Completed += new EventHandler(FinJugar);
            sbaux.Begin();
        }



        private void FinCerrarOjoDrch(object sender, EventArgs e)
        {
            if (pbHambre.Value <= 0.0 || pbDiversion.Value <= 0.0 || pbCansancio.Value <= 0.0)
            {
                btnDescansar.IsEnabled = false;
            }
            else btnDescansar.IsEnabled = true;

        }
        private void FinComer(object sender, EventArgs e)
        {
            if (pbHambre.Value <= 0.0 || pbDiversion.Value <= 0.0 || pbCansancio.Value <= 0.0)
            {
                btnComer.IsEnabled = false;
            }
            else btnComer.IsEnabled = true;

        }
        private void FinJugar(object sender, EventArgs e)
        {
            if (pbHambre.Value <= 0.0 || pbDiversion.Value <= 0.0 || pbCansancio.Value <= 0.0)
            {
                btnJugar.IsEnabled = false;
            }
            else btnJugar.IsEnabled = true;

        }

        private void cambiarFondo(object sender, MouseButtonEventArgs e)
        {
            this.imFondo.Source = ((Image)sender).Source;  //imFondoVolcan.source
            if (imFondo.Source == imFondoMoe.Source)
            {
                FotoMoe.Visibility = Visibility.Visible;
            }
            else FotoMoe.Visibility = Visibility.Hidden;

            if (imFondo.Source == ImHospital.Source && controlSaxo == 0)
            {
                imSaxofonLisa.Visibility = Visibility.Visible;
            }
            else imSaxofonLisa.Visibility = Visibility.Hidden;

            if (imFondo.Source == imSalon.Source && controlChupete == 0)
            {
                imChupeteMaggie.Visibility = Visibility.Visible;
            }
            else imChupeteMaggie.Visibility = Visibility.Hidden;
        }

        private void acercaDe(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult resultado = MessageBox.Show("Programa realizado por\n\n Carlos Mohedano Callejo\n\n\n ¿Desea salir?", "Acerca de", MessageBoxButton.YesNo);

            switch (resultado)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }

        }

        public void setNombre(string nombre)
        {
            this.nombre = nombre;
            tbNombreJugador.Text = "Bienvenido " + nombre;

        }

        private void inicioArrastrarGorro(object sender, MouseButtonEventArgs e)
        {

            DataObject dobj = new DataObject((Image)sender);
            DragDrop.DoDragDrop((Image)sender, dobj, DragDropEffects.Move);
        }

        private void colocarColeccionable(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));

            switch (aux.Name)
            {
                case "imGorroMini":
                    imGorro1.Visibility = Visibility.Hidden;
                    imGorro.Visibility = Visibility.Visible;
                    break;
                case "imGorroPerry":
                    imGorro.Visibility = Visibility.Hidden;
                    imGorro1.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void AtraparMaggie(object sender, MouseButtonEventArgs e)
        {
            this.pbDiversion.Value = 90;
            this.pbHambre.Value = 90;
            this.pbCansancio.Value = 90;
            FotoMagie.Visibility = Visibility.Hidden;
            Storyboard sbaux = (Storyboard)this.Resources["animacionJugabilidad1"];
            sbaux.Remove();
            MessageBoxResult resultado = MessageBox.Show("Has conseguido atrapar a Maggie\n", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
       
            LGMaggie.Background = Brushes.Cyan;


        }



        private void SaludarMoe(object sender, MouseButtonEventArgs e)
        {
            if (imFondo.Source == imFondoMoe.Source && controlLogroSaludo == 0)
            {
                Storyboard sbaux = (Storyboard)this.Resources["animacionJugabilidad2"];
                sbaux.Begin();
                MessageBoxResult resultado = MessageBox.Show("Has saludado a Moe en el bar\n", "Logro Desbloqueado\n\n", MessageBoxButton.OK);
                this.pbDiversion.Value += 25;
                LGSaludo.Background = Brushes.Cyan;
                controlLogroSaludo = 1;

            }

        }

        private void LVRanking_Initialized(object sender, EventArgs e)
        {
            LVRanking.Items.Add("Carlos Puntos: 47");
            LVRanking.Items.Add("Copen Puntos: 38");
            LVRanking.Items.Add("Alfredo Puntos: 37");
            LVRanking.Items.Add("Luis Puntos: 28");
            LVRanking.Items.Add("Ana Puntos: 17");
            LVRanking.Items.Add("Noelia Puntos: 19");


        }

        private void QuitarGorro(object sender, MouseButtonEventArgs e)
        {
            DataObject dataO = new DataObject(((Image)sender));
            DragDrop.DoDragDrop((Image)sender, dataO, DragDropEffects.Move);
        }
        private void eliminarColeccionable(object sender, DragEventArgs e)
        {
            Image aux = (Image)e.Data.GetData(typeof(Image));

            switch (aux.Name)
            {
                case "imGorro":
                    imGorro.Visibility = Visibility.Hidden;
                    break;
                case "imGorro1":
                    imGorro1.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void EncontrarChupete(object sender, MouseButtonEventArgs e)
        {
            imChupeteMaggie.Visibility = Visibility.Hidden;
            controlChupete = 1;
            PmChupete.Visibility = Visibility.Visible;
            decremento = 2;
        }

        private void EncontrarSaxofon(object sender, MouseButtonEventArgs e)
        {
            imSaxofonLisa.Visibility = Visibility.Hidden;
            controlSaxo = 1;
            PmSaxofon.Visibility = Visibility.Visible;
            decremento = 2;


        }
    }
}
