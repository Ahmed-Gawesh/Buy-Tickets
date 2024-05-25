using AutoMapper;
using BuyMovies.Helpers;
using BuyMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Interfaces;
using Movies.BLL.Repositories;
using Movies.DAL.Models;
using System.Reflection.Metadata;

namespace BuyMovies.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CinemaController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CinemaController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Cinema> cinemas;
            if(string.IsNullOrEmpty(searchValue))
                cinemas= await unitOfWork.CinemaRepository.GetAll();
            else
                cinemas=unitOfWork.CinemaRepository.GetCinemasByName(searchValue);

            var mappedCinema = mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaViewModel>>(cinemas);

            return View(mappedCinema);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CinemaViewModel cinemaVM)
        {
            if (ModelState.IsValid)
            {
                var mappedCinema = mapper.Map<CinemaViewModel, Cinema>(cinemaVM);
                mappedCinema.Logo = DocumentSettings.UploadImages(cinemaVM.LogoFile, "Images");
                await unitOfWork.CinemaRepository.Add(mappedCinema);

                int Result = await unitOfWork.Complete();
                if (Result > 0)
                {
                    TempData["Message"] = "Cinema Is Added";
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var cinema = await unitOfWork.CinemaRepository.GetbyID(id.Value);

            if (cinema == null)
                return NotFound();

            var mappedCinema = mapper.Map<Cinema, CinemaViewModel>(cinema);

            return View(viewName, mappedCinema);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, CinemaViewModel cinemaVM)
        {
            if (id != cinemaVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedCinema = mapper.Map<CinemaViewModel, Cinema>(cinemaVM);
                    mappedCinema.Logo = DocumentSettings.UploadImages(cinemaVM.LogoFile, "Images");
                    unitOfWork.CinemaRepository.Update(mappedCinema);
                    int Result = await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(cinemaVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, CinemaViewModel cinemaVM)
        {
            if (id != cinemaVM.Id)
                return BadRequest();

            try
            {
                var mappedCinema = mapper.Map<CinemaViewModel, Cinema>(cinemaVM);
                unitOfWork.CinemaRepository.Delete(mappedCinema);
                int result = await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(cinemaVM);

            }


        }
    }
}
