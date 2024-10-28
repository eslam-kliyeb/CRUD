using AutoMapper;
using CRUD.BLL.CQRS.DepartmentCqrs.Commands;
using CRUD.BLL.CQRS.DepartmentCqrs.Queries;
using CRUD.BLL.Interfaces;
using CRUD.BLL.Repositories;
using CRUD.DAL.Models;
using CRUD.PL.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.PL.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public DepartmentController(IMapper mapper,IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string SearchValue)
        {
            if (SearchValue is null)
            {
                var departments = await _mediator.Send(new GetAllDepartmentQuery());
                var departmentsVM = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
                return View(departmentsVM);
            }
            else
            {
                var departments= await _mediator.Send(new GetDepartmentsByNameQuery(SearchValue));
                var departmentsVM = _mapper.Map<IEnumerable<DepartmentViewModel>>(departments);
                return View(departmentsVM);
            }
            
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid){
                var department = _mapper.Map<Department>(departmentVM);
                await _mediator.Send(new InsertDepartmentCommand(department));
                TempData["Success"] = "Add Done";
                return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id,string ViewName="Details")
        {
            if (id is null) return BadRequest();
            var department = await _mediator.Send(new GetDepartmentQuery(id.Value));
            if(department == null) return NotFound();
            var departmentVM = _mapper.Map<DepartmentViewModel>(department);
            return View(ViewName,departmentVM);
        }
        [HttpGet]
        public Task<IActionResult> Edit(int? id)
        {
            return Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DepartmentViewModel departmentVM,[FromRoute]int id)
        {
            if (id!=departmentVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var department = _mapper.Map<Department>(departmentVM);
                    await _mediator.Send(new UpdateDepartmentCommand(department));
                    return RedirectToAction(nameof(Index));
                }
                catch(System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                
            }
            return View(departmentVM);
        }
        [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
            if (id != departmentVM.Id) return BadRequest();
            try
            {
                var department = _mapper.Map<Department>(departmentVM);
                await _mediator.Send(new RemoveDepartmentCommand(department));
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(departmentVM);
            }
        }
        public async Task<IActionResult> DepartmentsPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var departments = await _mediator.Send(new GetAllDepartmentQuery());
            DepartmentDocRepository documentRepository = new DepartmentDocRepository(departments);
            documentRepository.GeneratePdfAndShow();
            TempData["Pdf"] = "Pdf Done";
            return RedirectToAction(nameof(Index));
        }

    }
}
