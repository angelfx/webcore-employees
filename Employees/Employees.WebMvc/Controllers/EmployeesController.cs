using AutoMapper;
using Employees.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Employees.WebMvc.Controllers
{
    public class EmployeesController : Controller
    {
        private Employees.Abstract.ILogic _logic;

        public EmployeesController(Employees.Abstract.ILogic ctx)
        {
            _logic = ctx;
        }

        /// <summary>
        /// List of employees with search
        /// </summary>
        /// <param name="fullName">Full name</param>
        /// <param name="telNo">Telephone number</param>
        /// <param name="position">Position</param>
        /// <param name="depId">Id of the department</param>
        /// <returns></returns>
        public IActionResult Index(string fullName = "", string telNo = "", string position = "", int depId = 0)
        {
            var model = new TableEmployeeViewModel();

            //Search cryteria
            if (!string.IsNullOrEmpty(fullName))
                model.Search.FullName = fullName;

            if (!string.IsNullOrEmpty(telNo))
                model.Search.TelNo = telNo;

            if (!string.IsNullOrEmpty(position))
                model.Search.Position = position;

            if (depId > 0)
                model.Search.DepartmentId = depId;

            //Configure mapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employees.Models.DTO.EmployeeDTO, EmployeeViewModel>();
                cfg.CreateMap<EmployeeViewModel, Employees.Models.DTO.EmployeeDTO>();
            });
            var mapper = config.CreateMapper();

            //Search employees with search cryteria
            var employeesDTO = _logic.GetEmployees(mapper.Map<Employees.Models.DTO.EmployeeDTO>(model.Search));

            //Map results to models
            model.Eomployees = mapper.Map<List<EmployeeViewModel>>(employeesDTO);

            //Get departments for select on the form
            model.Search.DepartmentsList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_logic.GetDepartments(null), "Id", "Title");

            return View(model);
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            //Create empty model
            var employee = new EmployeeViewModel();

            //Get departments for select on the form
            var departments = _logic.GetDepartments(null);

            employee.DepartmentsList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departments, "Id", "Title");

            return View(employee);
        }

        /// <summary>
        /// Create new employee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(EmployeeViewModel model)
        {
            //Check model for valid
            if (ModelState.IsValid)
            {
                //Configure mapper
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeViewModel, Employees.Models.DTO.EmployeeDTO>());
                var mapper = config.CreateMapper();

                //Map model to DTO model
                var modelDTO = mapper.Map<Employees.Models.DTO.EmployeeDTO>(model);

                //Create new employee
                _logic.AddOrUpdateEmployee(modelDTO);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int id)
        {
            //Get employee from data base
            var employeeDTO = _logic.GetEmployee(id);

            //Configure mapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employees.Models.DTO.EmployeeDTO, EmployeeViewModel>());
            var mapper = config.CreateMapper();

            //Map result to view model
            var employee = mapper.Map<EmployeeViewModel>(employeeDTO);

            //Get departments for select on the form
            var departments = _logic.GetDepartments(null);

            employee.DepartmentsList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(departments, "Id", "Title");

            return View(employee);
        }

        /// <summary>
        /// Update employee
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            //Check model for valid
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeViewModel, Employees.Models.DTO.EmployeeDTO>());
                var mapper = config.CreateMapper();

                var modelDTO = mapper.Map<Employees.Models.DTO.EmployeeDTO>(model);

                //Update employee
                _logic.AddOrUpdateEmployee(modelDTO);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// Emplyee information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int id)
        {
            //Get employee from database
            var employeeDTO = _logic.GetEmployee(id);

            //Configure mapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employees.Models.DTO.EmployeeDTO, EmployeeViewModel>());
            var mapper = config.CreateMapper();

            //Map dto model to view model
            var employee = mapper.Map<EmployeeViewModel>(employeeDTO);

            return View(employee);
        }

        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            var employeeDTO = _logic.GetEmployee(id);

            //Configure mapper
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Employees.Models.DTO.EmployeeDTO, EmployeeViewModel>());
            var mapper = config.CreateMapper();

            //Map dto model to view model
            var employee = mapper.Map<EmployeeViewModel>(employeeDTO);

            return View(employee);
        }
        /// <summary>
        /// Delete employee
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(EmployeeViewModel model)
        {
            _logic.RemoveEmployee(model.Id);

            return RedirectToAction("Index");
        }
    }
}