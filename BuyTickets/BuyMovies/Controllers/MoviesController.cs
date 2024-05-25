using AutoMapper;
using BuyMovies.Helpers;
using BuyMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.BLL.Interfaces;
using Movies.BLL.Repositories;
using Movies.DAL.Models;
using System.Security;

namespace BuyMovies.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MoviesController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Movie> movies;
            if (string.IsNullOrEmpty(searchValue))
                movies = await unitOfWork.MovieRepository.GetAll();
            else
            {
                
                movies = unitOfWork.MovieRepository.GetMoviesByName(searchValue).Include(c=>c.Cinema);
            }
               

            var mappedMovie = mapper.Map<IEnumerable<Movie>, IEnumerable<MovieViewModel>>(movies);

            return View(mappedMovie);
        }
        [HttpGet]
       
        
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel MovieVM)
        {
            if (ModelState.IsValid)
            {
  
                var mappedMovie = mapper.Map<MovieViewModel, Movie>(MovieVM);
                mappedMovie.ImageURL = DocumentSettings.UploadImages(MovieVM.ImageFile, "Images");
                await unitOfWork.MovieRepository.Add(mappedMovie);

                int Result = await unitOfWork.Complete();
                if (Result > 0)
                {
                    TempData["Message"] = "Movie Is Added";
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var Movie = await unitOfWork.MovieRepository.GetbyID(id.Value);

            if (Movie == null)
                return NotFound();

            var MappedMovie = mapper.Map<Movie, MovieViewModel>(Movie);

            return View(viewName, MappedMovie);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public async Task<IActionResult> Edit([FromRoute] int? id, MovieViewModel MovieVM)
        {
            if (id != MovieVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedMovie = mapper.Map<MovieViewModel, Movie>(MovieVM);
                    MappedMovie.ImageURL = DocumentSettings.UploadImages(MovieVM.ImageFile, "Images");
                    unitOfWork.MovieRepository.Update(MappedMovie);
                    int Result = await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(MovieVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, MovieViewModel MovieVM)
        {
            if (id != MovieVM.Id)
                return BadRequest();

            try
            {
                var mappedMovie = mapper.Map<MovieViewModel, Movie>(MovieVM);
                unitOfWork.MovieRepository.Delete(mappedMovie);
                int result = await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(MovieVM);

            }


        }

   
    }
}
