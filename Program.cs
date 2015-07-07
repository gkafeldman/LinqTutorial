using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            ProjectionToAnonymousType();

            Console.ReadKey();
        }

        static void SimpleQuery()
        {
            List<string> musicalArtists = new List<string> { "Adele", "Maroon 5", "Queen", "Smashing Pumkins" };

            IEnumerable<string> aArtists /* data source */ =
                from artist /* range variable: holds the individual reference for each object */ in musicalArtists
                where artist.Contains("oo")
                select artist; // projection: the part of the object you want to return.  In this example it is the entire object.

            foreach (string item in aArtists)
            {
                Console.WriteLine(item);
            }
        }

        static void SimpleProjection()
        {
            List<MusicalArtist> artistDataSource = GetMusicalArtists();

            // Simple projection:
            IEnumerable<MusicalArtist> artistResult =
                from artist in artistDataSource  // artist is the range variable
                select artist; // select specifies the range variable as the projection.  Since items in the
            // collection artistDatasource are of type MusicalArtist, the projection will
            // be MusicalArtist too

            foreach (MusicalArtist artist in artistResult)
            {
                Console.WriteLine("Name: {0}\nGenre: {1}\nLatest Hit: {2}\n",
                    artist.Name,
                    artist.Genre,
                    artist.LatestHit);
            }
        }

        static void CustomProjection()
        {
            List<MusicalArtist> artistsDataSource = GetMusicalArtists();

            // Custom Projection:
            IEnumerable<MusicalArtist> artistsResult =
                from artist in artistsDataSource
                select new MusicalArtist
                {
                    Name = artist.Name,
                    LatestHit = artist.LatestHit
                };
            // Unlike the previous example, the projection is not on the range variable.  It is on a new
            // declaration of MusicalArtist.  The range variable artist contains the values to assign to
            // the new declaration of MusicalArtist

            foreach (MusicalArtist artist in artistsResult)
            {
                Console.WriteLine("Name: {0}\nLatest Hit: {1}\n",
                    artist.Name,
                    artist.LatestHit);
            }
        }

        static void ProjectionToDifferentType()
        {
            List<MusicalArtist> artistDataSource = GetMusicalArtists();

            IEnumerable<ArtistViewModel> artists =
                from artist in artistDataSource
                select new ArtistViewModel
                {
                    ArtistName = artist.Name,
                    Song = artist.LatestHit
                };
            // The range variable type and the type intantiated by the select clause are different.
            // The names of the properties of the projected type and the range type do not have to be the same.

            foreach (ArtistViewModel item in artists)
            {
                Console.WriteLine("Artist Name: {0}\nSong: {1}\n",
                    item.ArtistName,
                    item.Song);
            }
        }

        static void ProjectionToAnonymousType()
        {
            List<MusicalArtist> artistsDataSource = GetMusicalArtists();

            var artistResult =
                from artist in artistsDataSource
                select new  // No type name
                {
                    Name = artist.Name,
                    NumberOfAlbums = artist.Albums.Count
                };

            Console.WriteLine("\nProjecting Anonymous Types:");
            Console.WriteLine("---------------------------\n");

            foreach (var artist in artistResult)
            {
                Console.WriteLine(
                    "Artist Name: {0}\nNumber of Albums: {1}\n",
                    artist.Name,
                    artist.NumberOfAlbums);
            }
        }

        static List<MusicalArtist> GetMusicalArtists()
        {
            return new List<MusicalArtist>
            {
                new MusicalArtist 
                { 
                    Name = "Adele", 
                    Genre = "Pop", 
                    LatestHit = "Someone Like You",
                    Albums = new List<Album>
                    {
                        new Album { Name = "21", Year = "2011" },
                        new Album { Name = "19", Year = "2008" },
                    }
                },
                new MusicalArtist 
                { 
                    Name = "Maroon 5", 
                    Genre = "Adult Alternative", 
                    LatestHit = "Moves Like Jaggar",
                    Albums = new List<Album>
                    {
                        new Album { Name = "Misery", Year = "2010" },
                        new Album { Name = "It Won't Be Soon Before Long", Year = "2008" },
                        new Album { Name = "Wake Up Call", Year = "2007" },
                        new Album { Name = "Songs About Jane", Year = "2006" },
                    }
                },
                new MusicalArtist 
                { 
                    Name = "Lady Gaga", 
                    Genre = "Pop", 
                    LatestHit = "You And I",
                    Albums = new List<Album>
                    {
                        new Album { Name = "The Fame", Year = "2008" },
                        new Album { Name = "The Fame Monster", Year = "2009" },
                        new Album { Name = "Born This Way", Year = "2011" },
                    }
                }
            };
        }
    }

    public class MusicalArtist
    {
        public string Name { get; set; }
        public string Genre { get; set; }
        public string LatestHit { get; set; }
        public List<Album> Albums { get; set; }
    }

    public class Album
    {
        public string Name { get; set; }
        public string RecordingLabel { get; set; }
        public string Year { get; set; }
    }

    public class ArtistViewModel
    {
        public string ArtistName { get; set; }
        public string Song { get; set; }
    }
}
