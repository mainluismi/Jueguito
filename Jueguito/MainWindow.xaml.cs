using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Input;

namespace Jueguito
{
    /// <summary>
    /// Ventana principal del juego.
    /// </summary>
    public partial class MainWindow : Window
    {
        private int tiempoLimite = 3000; // Tiempo límite en milisegundos (hardcodeado inicialmente)
        private DispatcherTimer timer;
        private Button botonAzul;
        private bool juegoIniciado = false;

        /// <summary>
        /// Constructor de la clase MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InicializarJuego();
        }

        /// <summary>
        /// Inicializa los componentes del juego.
        /// </summary>
        private void InicializarJuego()
        {
            foreach (var boton in JuegoGrid.Children)
            {
                if (boton is Button)
                {
                    ((Button)boton).Click += Boton_Click;
                    ((Button)boton).MouseEnter += Boton_MouseEnter;
                    if (((Button)boton).Background is SolidColorBrush && ((SolidColorBrush)((Button)boton).Background).Color == Colors.Blue)
                    {
                        // Obtener el botón de color azul
                        botonAzul = (Button)boton;
                    }
                }
            }
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000); // Cambiado a 1000 ms (1 segundo) para cambios de color después de 1 segundo
            timer.Tick += Timer_Tick;
        }

        /// <summary>
        /// Maneja el evento MouseEnter del botón azul para iniciar el juego.
        /// </summary>
        private void Boton_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!juegoIniciado && sender == botonAzul)
            {
                // Iniciar el juego cuando el ratón entra en el botón azul
                IniciarJuego();
            }
        }

        /// <summary>
        /// Inicia el juego y el temporizador.
        /// </summary>
        private void IniciarJuego()
        {
            juegoIniciado = true;
            timer.Start();
        }

        /// <summary>
        /// Maneja el evento Click de los botones.
        /// </summary>
        private void Boton_Click(object sender, RoutedEventArgs e)
        {
            if (juegoIniciado)
            {
                if (sender == botonAzul)
                {
                    // Si se hace clic en el botón azul a tiempo, mostrar el mensaje y finalizar el juego
                    timer.Stop();
                    juegoIniciado = false;
                    botonAzul.Background = new SolidColorBrush(Colors.Green);
                    MessageBox.Show("¡¡¡Ganador!!!", "Fin del juego");
                    InicializarJuego(); // Reiniciar el juego
                }
                else
                {
                    // Si se hace clic en un botón diferente, detener el temporizador
                    timer.Stop();
                    juegoIniciado = false;
                    botonAzul.Background = new SolidColorBrush(Colors.Red);

                    // Cambiar el color de un botón aleatorio a azul
                    Random random = new Random();
                    int indiceBotonAleatorio = random.Next(JuegoGrid.Children.Count);

                    foreach (var boton in JuegoGrid.Children)
                    {
                        if (boton is Button && boton != botonAzul)
                        {
                            ((Button)boton).Background = new SolidColorBrush(Colors.Red);
                        }
                    }

                    if (JuegoGrid.Children[indiceBotonAleatorio] is Button)
                    {
                        ((Button)JuegoGrid.Children[indiceBotonAleatorio]).Background = new SolidColorBrush(Colors.Blue);
                    }
                }
            }
        }

        /// <summary>
        /// Maneja el evento Tick del temporizador para realizar cambios después de un tiempo establecido.
        /// </summary>
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Cambios de color después de 1 segundo si no se hace clic
            timer.Stop();
            juegoIniciado = false;
            botonAzul.Background = new SolidColorBrush(Colors.Red);

            // Cambiar el color de un botón aleatorio a azul
            Random random = new Random();
            int indiceBotonAleatorio = random.Next(JuegoGrid.Children.Count);

            foreach (var boton in JuegoGrid.Children)
            {
                if (boton is Button && boton != botonAzul)
                {
                    ((Button)boton).Background = new SolidColorBrush(Colors.Red);
                }
            }

            if (JuegoGrid.Children[indiceBotonAleatorio] is Button)
            {
                ((Button)JuegoGrid.Children[indiceBotonAleatorio]).Background = new SolidColorBrush(Colors.Blue);
            }

            MessageBox.Show("¡¡¡Demasiado lento!!!", "Fin del juego");
            InicializarJuego(); // Reiniciar el juego
        }
    }
}
