using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using S3Film.Entities;

namespace S3Film.DAL
{
    public class FilmRepository : BaseRepository
    {
        /// <summary>
        /// Retrieves all Films from the database.
        /// </summary>
        /// <returns>A List of all Films</returns>
        public List<Film> GetFilms() => HandleData(ExecuteQuery("SELECT * FROM Film ORDER BY Titel"));
        
        /// <summary>
        /// Gets all Films from the database where Titel contains the specified string.
        /// </summary>
        /// <param name="titel">The string to search for in the Film Titels</param>
        /// <returns>A list of Films containing the specified string</returns>
        public List<Film> GetFilms(string titel) => HandleData(ExecuteQuery($"SELECT * FROM Film WHERE Titel LIKE '%{titel}%'"));

        /// <summary>
        /// Inserts Film into database.
        /// </summary>
        /// <param name="film">The Film to be inserted into the database</param>
        /// <returns>A string returning the validation result</returns>
        public string InsertFilm(Film film) => IsValid(film) ? (ExecuteNonQuery($"INSERT INTO Film VALUES ('{film.Titel}', '{film.Land}', {film.Year}, '{film.Genre}', {film.Oscars})") > 0 ? $"Filmen \"{film.Titel}\" er oprettet." : "Alle felter skal udfyldes.") : "Filmen kunne ikke oprettes.";

        /// <summary>
        /// Updates Film in database.
        /// </summary>
        /// <param name="film">The film to be updated in the database</param>
        /// <returns>A string returning the validation result</returns>
        public string UpdateFilm(Film film) => IsValid(film) ? (ExecuteNonQuery($"UPDATE Film SET Titel = '{film.Titel}', Land = '{film.Land}', Year = {film.Year}, Oscars = {film.Oscars} WHERE FilmId = {film.Id}") > 0 ? $"Filmen \"{film.Titel}\" (Id: {film.Id}) er opdateret." : "Alle felter skal udfyldes.") : "Filmen kunne ikke opdateres.";

        /// <summary>
        /// Deletes Film from database
        /// </summary>
        /// <param name="film">The Film to be deleted from the database</param>
        /// <returns>A string returning the result</returns>
        public string DeleteFilm(Film film) => (ExecuteNonQuery($"DELETE FROM Film WHERE FilmId = {film.Id}") > 0) ? "Filmen er slettet." : "Filmen kunne ikke slettes";

        /// <summary>
        /// Helper method used to convert DataTable to list of Films.
        /// </summary>
        /// <param name="dataTable">The DataTable to be converted to list of Films</param>
        /// <returns>A list of Films from the database (empty list if the parameter is null)</returns>
        private List<Film> HandleData(DataTable dataTable)
        {
            List<Film> films = new List<Film>();
            if (dataTable is null) return films;
            dataTable.Rows.Cast<DataRow>().ToList().ForEach(row => { films.Add(new Film(){ Id = (int)row["FilmId"], Titel = (string)row["Titel"], Land = (string)row["Land"], Year = (int)row["Year"], Genre = (string)row["Genre"], Oscars = (int)row["Oscars"] }); });
            return films;
        }

        /// <summary>
        /// Validates Film before inserting it into the database.
        /// </summary>
        /// <param name="film">The Film to be validated</param>
        /// <param name="message">A string telling the result of the validation</param>
        /// <returns>false if any of the properties of the Film is null, else true</returns>
        private bool IsValid(Film film) => (string.IsNullOrWhiteSpace(film.Titel) || string.IsNullOrWhiteSpace(film.Land) || string.IsNullOrWhiteSpace(film.Genre)) ? false : true;
         
    }
}
