using AutoMapper;
using BuyMovies.Helpers;
using BuyMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Movies.BLL.Interfaces;
using Movies.BLL.Repositories;
using Movies.DAL.Models;

namespace BuyMovies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProducerController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProducerController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Producer> producers;
            if(string.IsNullOrEmpty(searchValue))
                producers= await unitOfWork.ProducerRepository.GetAll();
            else
                producers=unitOfWork.ProducerRepository.GetProducersbyName(searchValue);


            var mappedProducer = mapper.Map<IEnumerable<Producer>, IEnumerable<ProducerViewModel>>(producers);
            return View(mappedProducer);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProducerViewModel producerVM)
        {
            if (ModelState.IsValid)
            {
                var mappedProducer = mapper.Map<ProducerViewModel, Producer>(producerVM);
                await unitOfWork.ProducerRepository.Add(mappedProducer);

                int Result = await unitOfWork.Complete();
                if (Result > 0)
                {
                    TempData["Message"] = "Producer Is Added";
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Details(int? id, string viewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var Producer = await unitOfWork.ProducerRepository.GetbyID(id.Value);

            if (Producer == null)
                return NotFound();

            var MappedProducer = mapper.Map<Producer, ProducerViewModel>(Producer);

            return View(viewName, MappedProducer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int? id, ProducerViewModel producerVM)
        {
            if (id != producerVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedProducer = mapper.Map<ProducerViewModel, Producer>(producerVM);

                    unitOfWork.ProducerRepository.Update(MappedProducer);
                    int Result = await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);

                }

            }
            return View(producerVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, ProducerViewModel producerVM)
        {
            if (id != producerVM.Id)
                return BadRequest();

            try
            {
                var mappedProducer = mapper.Map<ProducerViewModel, Producer>(producerVM);
                unitOfWork.ProducerRepository.Delete(mappedProducer);
                int result = await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(producerVM);

            }


        }
    }
}
