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
using static Extensions.Extensions;

namespace S3Film.GUI.UserControls
{
    /// <summary>
    /// Interaction logic for FilmList.xaml
    /// </summary>
    public partial class FilmList : UserControl
    {
        public List<Film> Films { get; set; } = new FilmRepository().GetFilms();
        public string EditIcon = IconFolder + "edit.png";
        public string DeleteIcon = IconFolder + "delete.png";
        
        public FilmList()
        {
            InitializeComponent();
            filmGrid.ItemsSource = Films;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Film film = (Film)filmGrid.SelectedItem;
            new Window { Title = "Rediger film", Content = new UpdateFilm(film), Height = 350, Width = 250 }.ShowDialog();
            Films = new FilmRepository().GetFilms();
            filmGrid.ItemsSource = Films;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Film film = (Film)filmGrid.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Slet filmen \"{film.Titel}\" (id: {film.Id})?", "", MessageBoxButton.OKCancel);
            switch (result)
            {
                case MessageBoxResult.OK:
                    FilmRepository filmRepository = new FilmRepository();
                    MessageBox.Show(filmRepository.DeleteFilm(film));
                    Films = filmRepository.GetFilms();
                    filmGrid.ItemsSource = Films;
                    break;
                case MessageBoxResult.Cancel:
                    break;
                default:
                    break;
            }
        }
    }
}
