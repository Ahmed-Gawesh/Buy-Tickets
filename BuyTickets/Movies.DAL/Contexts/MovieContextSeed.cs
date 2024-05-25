using Movies.DAL.Data;
using Movies.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Movies.DAL.Contexts
{
    public  class MovieContextSeed
    {
        public static async Task SeedAsync(MovieContext dbContex)
        {

            if (!dbContex.Movies_Actors.Any())
            {
                dbContex.Movies_Actors.AddRange(new List<Movies_Actors>()
                {
                    new Movies_Actors()
                    {
                        ActorId=1,
                        MovieId=1,
                    },
                    new Movies_Actors()
                     {
                            ActorId = 3,
                            MovieId = 1
                     },

                         new Movies_Actors()
                        {
                            ActorId = 1,
                            MovieId = 2
                        },
                         new Movies_Actors()
                        {
                            ActorId = 4,
                            MovieId = 2
                        },

                        new Movies_Actors()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new Movies_Actors()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new Movies_Actors()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },


                        new Movies_Actors()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new Movies_Actors()
                        {
                            ActorId = 3,
                            MovieId = 4
                        }
                });
                await dbContex.SaveChangesAsync();

                
            }




            if (!dbContex.Cinemas.Any())
            {
                var cinemasData = File.ReadAllText("../Movies.DAL/DataSeed/cinema.json");

                var cinemas = JsonSerializer.Deserialize<List<Cinema>>(cinemasData);

                if (cinemas?.Count > 0)
                {
                    foreach (var cinema in cinemas)
                    {
                        await dbContex.Cinemas.AddAsync(cinema);

                        await dbContex.SaveChangesAsync();
                    }
                }
            }

            if (!dbContex.Actors.Any())
            {
                var actorsData = File.ReadAllText("../Movies.DAL/DataSeed/actor.json");

                var actors = JsonSerializer.Deserialize<List<Actor>>(actorsData);

                if(actors?.Count>0)
                {
                    foreach(var actor in actors)
                    {
                          await dbContex.Actors.AddAsync(actor);

                       await dbContex.SaveChangesAsync();
                    }
                }
            }


            

            if (!dbContex.Producers.Any())
            {
                var producersData = File.ReadAllText("../Movies.DAL/DataSeed/producer.json");

                var producers = JsonSerializer.Deserialize<List<Producer>>(producersData);

                if (producers?.Count > 0)
                {
                    foreach (var producer in producers)
                    {
                        await dbContex.Producers.AddAsync(producer);

                        await dbContex.SaveChangesAsync();
                    }
                }
            }



       


            if (!dbContex.Movies.Any())
            {
                dbContex.Movies.AddRange(new List<Movie>()
                {
                    new Movie()
                    {
                        Name = "Life",
                        Description = "This is the Life movie description",
                        Price =60,
                        ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",
                        StartDate = DateTime.Now.AddDays(-10),
                        EndDate = DateTime.Now.AddDays(10),
                        CinemaId = 3,
                        ProducerId = 3,
                        MovieCategory = MovieCategory.Documentary
                    },
                        new Movie()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29,
                            ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Ghost",
                            Description = "This is the Ghost movie description",
                            Price = 39,
                            ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            CinemaId = 3,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 37,
                            ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                         new Movie()
                        {
                            Name = "Scoob",
                            Description = "This is the Scoob movie description",
                            Price = 40,
                            ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 2,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 2,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Drama
                        }
                });
                   await dbContex.SaveChangesAsync();

            }




        }
    }
}
