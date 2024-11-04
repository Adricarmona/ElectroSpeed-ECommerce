using ElectroSpeed_server.Models.Data;
using ElectroSpeed_server.Models.Data.Dto;
using ElectroSpeed_server.Models.Data.Entities;
using F23.StringSimilarity;
using F23.StringSimilarity.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Razor.Infrastructure;
using System.Globalization;
using System.Text;
using static ElectroSpeed_server.Models.Data.Dto.FiltroBicis;

namespace ElectroSpeed_server.Recursos
{
    public class FiltroRecurso
    {
        private const double THRESHOLD = 0.75;
        private readonly INormalizedStringSimilarity _stringSimilarityComparer;
        private readonly ElectroSpeedContext _electroSpeedContext;

        public FiltroRecurso(ElectroSpeedContext electroSpeedContext)
        {
            _stringSimilarityComparer = new JaroWinkler();
            _electroSpeedContext = electroSpeedContext;
        }


        public IEnumerable<Bicicletas> Pages(FiltroBicis model, IEnumerable<Bicicletas> bicis)
        {
            if (model.CantidadProductos == 0 || model.PaginaActual == 0)
            {
                return null;
            }
            List<Bicicletas> biciPagina = new List<Bicicletas>();
           
            int totalPaginas = bicis.Count() / model.CantidadProductos;
            for (int i = 0; i < totalPaginas; i++)
            {
                if (i == model.PaginaActual - 1 )
                {
                    for (int j = 0; j < model.CantidadProductos; j++)
                    {
                        try
                        {
                            biciPagina.Add(bicis.ElementAt(j + (i * model.CantidadProductos)));
                        } 
                        catch 
                        {
                            // aqui implicaria que no hay procutos, viva el betis   
                        }
                    }
                }
            }

            return biciPagina;
        }

        public IEnumerable<Bicicletas> Order(FiltroBicis model, IEnumerable<Bicicletas> bicis)
        {

            switch (model.Criterio)
            {
                case Criterio.Marca:
                    // Ordenar bicicletas por marca

                    switch (model.Orden)
                    {
                        case Orden.Asc:
                            // Ordenar bicicletas por marca ascendente alfabeticamente
                            var marcasOrdenadosAsc = bicis
                                    .OrderBy(b => b.MarcaModelo)
                                    .ToList();
                            return marcasOrdenadosAsc;

                        case Orden.Desc:
                            // Ordenar bicicletas por marca descendiente alfabeticamente
                            var marcasOrdenadosDesc = bicis
                                     .OrderByDescending(b => b.MarcaModelo)
                                     .ToList();
                            return marcasOrdenadosDesc;
                    }
                    break;

                case Criterio.Precio:
                    // Ordenar bicicletas por precio

                    switch (model.Orden)
                    {
                        case Orden.Asc:
                            // Ordenar bicicletas por precio ascendente
                            var preciosOrdenadosAsc = bicis
                                .OrderBy(b => b.Precio)
                                .ToList();
                            return preciosOrdenadosAsc;

                        case Orden.Desc:
                            // Ordenar bicicletas por precio descendente
                            var preciosOrdenadosDesc = bicis
                                .OrderByDescending(b => b.Precio)
                                .ToList();
                            return preciosOrdenadosDesc;
                    }

                    break;
            }
            return null;
        }

        public IEnumerable<Bicicletas> Search(string query, IEnumerable<Bicicletas> bicis)
        {
            IEnumerable<Bicicletas> result;

            // Si la consulta está vacía o solo tiene espacios en blanco, devolvemos todos los items
            if (string.IsNullOrWhiteSpace(query))
            {
                result = bicis;
            }
            // En caso contrario, realizamos la búsqueda
            else
            {
                // Limpiamos la query y la separamos por espacios
                string[] queryKeys = GetKeys(ClearText(query));
                // Aquí guardaremos los items que coincidan
                List<Bicicletas> matches = new List<Bicicletas>();

                foreach (Bicicletas item in bicis)
                {
                    // Limpiamos el item y lo separamos por espacios
                    string[] itemKeys = GetKeys(ClearText(item.MarcaModelo));

                    // Si coincide alguna de las palabras de item con las de query
                    // entonces añadimos item a la lista de coincidencias
                    if (IsMatch(queryKeys, itemKeys))
                    {
                        matches.Add(item);
                    }
                }

                result = matches;
            }
            return result;
            
        }

        private bool IsMatch(string[] queryKeys, string[] itemKeys)
        {
            bool isMatch = false;

            for (int i = 0; !isMatch && i < itemKeys.Length; i++)
            {
                string itemKey = itemKeys[i];

                for (int j = 0; !isMatch && j < queryKeys.Length; j++)
                {
                    string queryKey = queryKeys[j];

                    isMatch = IsMatch(itemKey, queryKey);
                }
            }

            return isMatch;
        }

        // Hay coincidencia si las palabras son las mismas o si item contiene query o si son similares
        private bool IsMatch(string itemKey, string queryKey)
        {
            return itemKey == queryKey
                || itemKey.Contains(queryKey)
                || _stringSimilarityComparer.Similarity(itemKey, queryKey) >= THRESHOLD;
        }

        // Separa las palabras quitando los espacios
        private string[] GetKeys(string query)
        {
            return query.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        }

        // Normaliza el texto quitándole las tildes y pasándolo a minúsculas
        private string ClearText(string text)
        {
            return RemoveDiacritics(text.ToLower());
        }

        // Quita las tildes a un texto
        private string RemoveDiacritics(string text)
        {
            string normalizedString = text.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder(normalizedString.Length);

            for (int i = 0; i < normalizedString.Length; i++)
            {
                char c = normalizedString[i];
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
