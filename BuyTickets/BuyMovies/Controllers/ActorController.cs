using AutoMapper;
using BuyMovies.Helpers;
using BuyMovies.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Movies.BLL.Interfaces;
using Movies.BLL.Repositories;
using Movies.DAL;
using Movies.DAL.Models;

namespace BuyMovies.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ActorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ActorController(IUnitOfWork unitOfWork,IMapper mapper )
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }



        public  async Task<IActionResult> Index(string searchValue)
        {
            IEnumerable<Actor> actors;
            if(string.IsNullOrEmpty(searchValue))
            
                 actors = await unitOfWork.ActorRepository.GetAll();
            else
                actors= unitOfWork.ActorRepository.GetActorByName(searchValue);

            var mappedActor = mapper.Map<IEnumerable<Actor>,IEnumerable<ActorViewModel>>(actors);

            return View(mappedActor);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ActorViewModel actorVM)
        {
            if(ModelState.IsValid)
            {
                var mappedActor=mapper.Map<ActorViewModel,Actor>(actorVM);
                mappedActor.ProfilePictureURL = DocumentSettings.UploadImages(actorVM.Picture,"Images");

                await unitOfWork.ActorRepository.Add(mappedActor);

                int Result = await unitOfWork.Complete();
                if (Result > 0)
                {
                    TempData["Message"] = "Actor Is Added";
                }

                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id,string viewName="Details") 
        {
            if (id is null)
                return BadRequest();

            var actor = await unitOfWork.ActorRepository.GetbyID(id.Value);

            if (actor == null)
                return NotFound();

            var MappedActor = mapper.Map<Actor, ActorViewModel>(actor);

            return View(viewName, MappedActor);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int? id,ActorViewModel actorVM)
        {
            if (id !=actorVM.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                try
                {
                    var MappedActor = mapper.Map<ActorViewModel, Actor>(actorVM);
                    MappedActor.ProfilePictureURL = DocumentSettings.UploadImages(actorVM.Picture, "Images");
                     unitOfWork.ActorRepository.Update(MappedActor);
                    int Result =await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    
                }

            }
            return View(actorVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] int id, ActorViewModel actorVM)
        {
            if(id !=actorVM.Id)
                return BadRequest();

                try
                {
                   var mappedActor = mapper.Map<ActorViewModel, Actor>(actorVM);
                   unitOfWork.ActorRepository.Delete(mappedActor);
                   int result = await unitOfWork.Complete();
                   return RedirectToAction(nameof(Index));
      
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(actorVM);

                }


        }
    }
}
