using AutoMapper;
using Employees.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Employees.WebMvc.Controllers
{
    public class DepartmentsController : Controller
    {
        private Employees.Abstract.ILogic _logic;

        public DepartmentsController(Employees.Abstract.ILogic ctx)
        {
            _logic = ctx;
        }

        public IActionResult Index()
        {
            var departmentsDTO = _logic.GetDepartments(null);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employees.Models.DTO.DepartmentDTO, DepartmentViewModel>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore()));
            var mapper = config.CreateMapper();

            var departments = mapper.Map<List<DepartmentViewModel>>(departmentsDTO);

            return View(departments);
        }

        public IActionResult Create()
        {
            var model = new DepartmentViewModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentViewModel, Employees.Models.DTO.DepartmentDTO>());
                var mapper = config.CreateMapper();

                var modelDTO = mapper.Map<Employees.Models.DTO.DepartmentDTO>(model);

                _logic.AddOrUpdateDepartment(modelDTO);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var modelDTO = _logic.GetDepartment(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employees.Models.DTO.DepartmentDTO, DepartmentViewModel>()
            .ForMember(dest=>dest.Employees, opt=>opt.Ignore()));
            var mapper = config.CreateMapper();

            var model = mapper.Map<DepartmentViewModel>(modelDTO);

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentViewModel, Employees.Models.DTO.DepartmentDTO>());
                var mapper = config.CreateMapper();

                var modelDTO = mapper.Map<Employees.Models.DTO.DepartmentDTO>(model);

                _logic.AddOrUpdateDepartment(modelDTO);

                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var modelDTO = _logic.GetDepartment(id);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employees.Models.DTO.DepartmentDTO, DepartmentViewModel>();
                cfg.CreateMap<Employees.Models.DTO.EmployeeDTO, EmployeeViewModel>();
            });
            var mapper = config.CreateMapper();

            var model = mapper.Map<DepartmentViewModel>(modelDTO);

            return View(model);
        }
    }
}