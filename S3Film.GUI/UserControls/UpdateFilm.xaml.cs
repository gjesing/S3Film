using S3Film.DAL;
using S3Film.Entities;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace S3Film.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for UpdateFilm.xaml
    /// </summary>
    public partial class UpdateFilm : UserControl
    {
        public Film Film { get; set; }
        public UpdateFilm(Film film)
        {
            InitializeComponent();
            Film = film;
            titel.Text = Film.Titel;
            land.Text = Film.Land;
            year.Text = Film.Year.ToString();
            genre.Text = Film.Genre;
            oscars.Text = Film.Oscars.ToString();
        }

        private void CloseWindow() => Application.Current.Windows[1].Close();

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(year.Text, out int filmYear) && int.TryParse(oscars.Text, out int filmOscars))
            {
                Film.Titel = titel.Text;
                Film.Land = land.Text;
                Film.Year = filmYear;
                Film.Genre = genre.Text;
                Film.Oscars = filmOscars;
                MessageBox.Show($"{new FilmRepository().UpdateFilm(Film)}");
                CloseWindow();
            }
            else
            {
                MessageBox.Show("Felterne \"Udgivelsesår\" og \"Antal Oscars\" kan kun indeholde tal.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => CloseWindow();
    }
}
