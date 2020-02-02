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
        public List<Film> GetFilms()
        {
            string sql = "SELECT * FROM Film ORDER BY Titel";
            return HandleData(ExecuteQuery(sql));
        }

        /// <summary>
        /// Gets all Films from the database where Titel contains the specified string.
        /// </summary>
        /// <param name="titel">The string to search for in the Film Titels</param>
        /// <returns>A list of Films containing the specified string</returns>
        public List<Film> GetFilms(string titel)
        {
            string sql = $"SELECT * FROM Film WHERE Titel LIKE '%{titel}%'";
            return HandleData(ExecuteQuery(sql));
        }

        /// <summary>
        /// Inserts Film into database.
        /// </summary>
        /// <param name="film">The Film to be inserted into the database</param>
        /// <returns>A string telling the validation result</returns>
        public string InsertFilm(Film film)
        {
            bool isValid = IsValid(film, out string message);
            if (!isValid)
            {
                return message;
            }
            string sql = $"INSERT INTO Film VALUES ('{film.Titel}', '{film.Land}', {film.Year}, '{film.Genre}', {film.Oscars})";
            int affectedRows = ExecuteNonQuery(sql);
            if (affectedRows > 0)
            {
                return message;
            }
            message = "Filmen kunne ikke oprettes.";
            return message;
        }

        /// <summary>
        /// Helper method used to convert DataTable to list of Films.
        /// </summary>
        /// <param name="dataTable">The DataTable to be converted to list of Films</param>
        /// <returns>A list of Films from the database (empty list if the parameter is null)</returns>
        private List<Film> HandleData(DataTable dataTable)
        {
            List<Film> films = new List<Film>();
            if (dataTable is null)
                return films;

            foreach (DataRow row in dataTable.Rows)
            {
                Film film = new Film()
                {
                    Id = (int)row["FilmId"],
                    Titel = (string)row["Titel"],
                    Land = (string)row["Land"],
                    Year = (int)row["Year"],
                    Genre = (string)row["Genre"],
                    Oscars = (int)row["Oscars"]
                };
                films.Add(film);
            }
            return films;
        }

        /// <summary>
        /// Validates Film before inserting it into the database.
        /// </summary>
        /// <param name="film">The Film to be validated</param>
        /// <param name="message">A string telling the result of the validation</param>
        /// <returns>false if any of the properties of the Film is null, else true</returns>
        private bool IsValid(Film film, out string message)
        {
            if (string.IsNullOrWhiteSpace(film.Titel) || string.IsNullOrWhiteSpace(film.Land) || string.IsNullOrWhiteSpace(film.Genre))
            {
                message = "Alle felter skal udfyldes.";
                return false;
            }
            message = $"Filmen {film.Titel} er oprettet.";
            return true;
        }
    }
}
