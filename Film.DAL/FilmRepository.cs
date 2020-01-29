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
        /// <returns></returns>
        public List<Film> GetFilms()
        {
            string sql = "SELECT * FROM Film";
            return HandleData(ExecuteQuery(sql));
        }

        /// <summary>
        /// Gets all Films from the database where Titel contains the specified string.
        /// </summary>
        /// <param name="titel">The string to search for in the Film Titels</param>
        /// <returns></returns>
        public List<Film> GetFilms(string titel)
        {
            string sql = $"SELECT * FROM Film WHERE Titel LIKE '%{titel}%'";
            return HandleData(ExecuteQuery(sql));
        }

        /// <summary>
        /// Helper method used to convert DataTable to list of Films.
        /// Returns an empty list if the parameter is null.
        /// </summary>
        /// <param name="dataTable">The DataTable to be converted to list of Films</param>
        /// <returns></returns>
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
    }
}
