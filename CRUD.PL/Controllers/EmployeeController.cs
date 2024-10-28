using AutoMapper;
using CRUD.BLL.CQRS.DepartmentCqrs.Queries;
using CRUD.BLL.Interfaces;
using CRUD.DAL.Models;
using CRUD.PL.Helpers;
using CRUD.PL.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUD.PL.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        public EmployeeController(IMapper mapper,
                                  IUnitOfWork unitOfWork,
                                  IMediator mediator)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchValue)
        {
            if (searchValue == null)
            {
                var employees = await _unitOfWork.EmployeeRepository.GetAllAsync();
                var employeesVM=_mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(employeesVM);
            }
            else
            {
                var employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(searchValue);
                var employeesVM = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);
                return View(employeesVM);
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.departments = await _mediator.Send(new GetAllDepartmentQuery());
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {
            ViewBag.Email = ViewBag.Email;
            string email = ViewBag.Email;
            if (ModelState.IsValid)
            {
                if (employeeVM.Image is not null)  employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
                var employee = _mapper.Map<Employee>(employeeVM);
                await _unitOfWork.EmployeeRepository.AddAsync(employee);
                await _unitOfWork.CompleteAsync();
                TempData["Success"] = "Add Done";
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null) return BadRequest();
            var employee =await _unitOfWork.EmployeeRepository.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();
            if(ViewName== "Edit") TempData["Edit"] = "Edit";
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);
            return View(ViewName, employeeVM);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.departments =await _mediator.Send(new GetAllDepartmentQuery());
            return await Details(id, "Edit");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            
            if (id != employeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (employeeVM.prevImageName is not null && employeeVM.Image is null) { employeeVM.ImageName = employeeVM.prevImageName; employeeVM.prevImageName = null; }
                    if (employeeVM.Image is not null) employeeVM.ImageName = DocumentSetting.UploadFile(employeeVM.Image, "Images");
                    var employee = _mapper.Map<Employee>(employeeVM);
                    _unitOfWork.EmployeeRepository.Update(employee);
                    if (await _unitOfWork.CompleteAsync() > 0 && employeeVM.prevImageName is not null) DocumentSetting.DeleteFile(employeeVM.prevImageName, "Images");
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }

            }
            return View(employeeVM);
        }
        [HttpGet]
        public Task<IActionResult> Delete(int? id)
        {
            return Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id) return BadRequest();
            try
            {
                var employee = _mapper.Map<Employee>(employeeVM);
                _unitOfWork.EmployeeRepository.Delete(employee);
                if(await _unitOfWork.CompleteAsync()>0 && employeeVM.ImageName is not null) DocumentSetting.DeleteFile(employeeVM.ImageName, "Images");
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }

        }
    }
}
