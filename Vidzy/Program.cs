using System.Linq;

namespace Vidzy
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new VidzyContext();

            var queryActionMovies = context.Videos.Where(m => m.Genre.Name == "Action");

            foreach (var movies in queryActionMovies)
            {
                System.Console.WriteLine(movies.Name);
            }
            var queryGoldDramaMovies = context.Videos
                .Where(m => m.Genre.Name == "Drama" && m.Classification == Classification.Gold)
                .OrderBy(m => m.ReleaseDate);


            foreach (var movie in queryGoldDramaMovies)
            {
                System.Console.WriteLine(movie.Name);
            }

            var queryAnonymousObj = context.Videos.Select(v => new { MovieName = v.Name, Genre = v.Genre.Name });

            foreach (var movie in queryAnonymousObj)
            {
                System.Console.WriteLine($"{movie.MovieName}\t {movie.Genre}");
            }


            var queryGroupingByClassification = context.Videos
                .GroupBy(m => m.Classification)
                .Select(g => new
                {
                    Classification = g.Key.ToString(),
                    Movies = g.OrderBy(v => v.Name)
                });

            foreach (var group in queryGroupingByClassification)
            {
                System.Console.WriteLine($"Classification: {group.Classification}");

                foreach (var movie in group.Movies)
                {
                    System.Console.WriteLine("\t" + movie.Name);
                }
            }

            var queryGroupAndCounting = context.Videos
                .GroupBy(m => m.Classification)
                .Select(g => new
                {
                    Name = g.Key.ToString(),
                    Count = g.Count()
                })
                .OrderBy(g => g.Name);

            foreach (var classification in queryGroupAndCounting)
            {
                System.Console.WriteLine(classification.Name + classification.Count);
            }

            //var query = context.Genres
            //    .Select(g => new
            //    {
            //        Name = g.Name,
            //        VideoCount = g.Videos.Count()
            //    })
            //    .OrderByDescending(g => g.VideoCount);

            //foreach (var genre in query)
            //{
            //    System.Console.WriteLine(genre.Name + " " + genre.VideoCount);
            //}
        }
    }
}
