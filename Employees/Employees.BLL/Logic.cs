using AutoMapper;
using Employees.Models.DTO;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Employees.BLL
{
    public class Logic : Employees.Abstract.ILogic
    {
        private readonly Employees.Abstract.IDALContext _dbContext;
        protected Employees.Abstract.IDALContext dbContext { get { return _dbContext; } }

        public Logic(Employees.Abstract.IDALContext ctx)
        {
            _dbContext = ctx;
        }

        /// <summary>
        /// Add ir update employee
        /// </summary>
        /// <param name="employeeDTO"></param>
        public void AddOrUpdateEmployee(EmployeeDTO employeeDTO)
        {
            if (employeeDTO?.Id > 0 && employeeDTO?.DepartmentId > 0)
            {
                var employee = dbContext.Employees.Find(employeeDTO.Id);
                var department = dbContext.Departments.Find(employeeDTO.DepartmentId);
                employee.Department = department;
                employee.FullName = employeeDTO.FullName;
                employee.TelNo = employeeDTO.TelNo;
                employee.Position = employeeDTO.Position;

                dbContext.SaveChanges();
            }
            else if (employeeDTO?.DepartmentId > 0)
            {
                var department = dbContext.Departments.Find(employeeDTO.DepartmentId);

                var config = new MapperConfiguration(cfg => cfg.CreateMap<EmployeeDTO, Employees.Entities.Employee>());
                var mapper = config.CreateMapper();

                var employee = mapper.Map<Employees.Entities.Employee>(employeeDTO);
                employee.Department = department;

                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Get employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeDTO GetEmployee(int id)
        {
            var employee = dbContext.Employees.Include(x=>x.Department).FirstOrDefault(x=>x.Id == id);
            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Employees.Entities.Employee, EmployeeDTO>()
                    .ForMember(dest => dest.DepartmentId, src => src.MapFrom(s => s.Department.Id))
                    .ForMember(dest => dest.DepartmentCode, src => src.MapFrom(s => s.Department.Code))
                    .ForMember(dest => dest.DepartmentTitle, src => src.MapFrom(s => s.Department.Title)));
            var mapper = config.CreateMapper();

            var employeeDTO = mapper.Map<EmployeeDTO>(employee);
            return employeeDTO;
        }

        /// <summary>
        /// Get employees, can set parameters to search
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<EmployeeDTO> GetEmployees(EmployeeDTO search)
        {
            var employeesDTO = dbContext.Employees.AsQueryable();
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.FullName))
                    employeesDTO = employeesDTO.Where(x => x.FullName.Contains(search.FullName));

                if (!string.IsNullOrEmpty(search.TelNo))
                    employeesDTO = employeesDTO.Where(x => x.TelNo.Contains(search.TelNo));

                if (search.DepartmentId > 0)
                    employeesDTO = employeesDTO.Where(x => x.Department.Id == search.DepartmentId);

            }

            return employeesDTO.Select(x => new EmployeeDTO
            {
                DepartmentCode = x.Department.Code,
                DepartmentId = x.Department.Id,
                DepartmentTitle = x.Department.Title,
                FullName = x.FullName,
                Position = x.Position,
                Id = x.Id,
                TelNo = x.TelNo
            }).ToList();
        }

        /// <summary>
        /// Remove eomployee
        /// </summary>
        /// <param name="id"></param>
        public void RemoveEmployee(int id)
        {
            var employee = dbContext.Employees.Find(id);

            if(employee != null)
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Add or uodate department
        /// </summary>
        /// <param name="departmentDTO"></param>
        public void AddOrUpdateDepartment(DepartmentDTO departmentDTO)
        {
            if (departmentDTO?.Id > 0)
            {
                var department = dbContext.Departments.Find(departmentDTO.Id);
                department.Title = departmentDTO.Title;
                department.Code = departmentDTO.Code;

                dbContext.SaveChanges();
            }
            else
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DepartmentDTO, Employees.Entities.Department>());
                var mapper = config.CreateMapper();

                var department = mapper.Map<Employees.Entities.Department>(departmentDTO);

                dbContext.Departments.Add(department);
                dbContext.SaveChanges();
            }
        }

        /// <summary>
        /// Get department by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DepartmentDTO GetDepartment(int id)
        {
            var department = dbContext.Departments.Include(x=>x.Employees).FirstOrDefault(x=>x.Id == id);
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Employees.Entities.Department, DepartmentDTO>();
                cfg.CreateMap<Employees.Entities.Employee, EmployeeDTO>();
                });
            var mapper = config.CreateMapper();

            var departmentDTO = mapper.Map<DepartmentDTO>(department);
            return departmentDTO;
        }

        /// <summary>
        /// Get departments, we can set parameters to search
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<DepartmentDTO> GetDepartments(DepartmentDTO search)
        {
            var departmentsDTO = dbContext.Departments.AsQueryable();
            if (search != null)
            {
                if (!string.IsNullOrEmpty(search.Title))
                    departmentsDTO = departmentsDTO.Where(x => x.Title.Contains(search.Title));

                if (!string.IsNullOrEmpty(search.Code))
                    departmentsDTO = departmentsDTO.Where(x => x.Code.Contains(search.Code));
            }

            return departmentsDTO.Select(x => new DepartmentDTO
            {
                Title = x.Title,
                Code = x.Code,
                Id = x.Id,
            }).ToList();
        }

        /// <summary>
        /// Create sample data
        /// </summary>
        public void CreateData()
        {
            if (!dbContext.Departments.Any())
            {
                dbContext.Departments.AddRange(
                    new Entities.Department
                    {
                        Title = "Planning",
                        Code = "100"
                    },
                    new Entities.Department
                    {
                        Title = "Purchase",
                        Code = "101"
                    },
                    new Entities.Department
                    {
                        Title = "Tech support",
                        Code = "102"
                    },
                    new Entities.Department
                    {
                        Title = "Development",
                        Code = "103"
                    }
                );

                dbContext.SaveChanges();
            }

            if (!dbContext.Employees.Any())
            {
                dbContext.Employees.AddRange(
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "100"),
                        FullName = "John Stillman",
                        TelNo = "15-24",
                        Position = "Manager"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "100"),
                        FullName = "Alex Mayer",
                        TelNo = "65-77",
                        Position = "Economist lead"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "100"),
                        FullName = "Evan Stark",
                        TelNo = "12-56",
                        Position = "Economist"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "101"),
                        FullName = "Samuel Ferguson",
                        TelNo = "99-54",
                        Position = "Manager"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "101"),
                        FullName = "Max Gonzales",
                        TelNo = "63-47",
                        Position = "Supply engineer"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "101"),
                        FullName = "Kate Fisher",
                        TelNo = "48-73",
                        Position = "Engineer lead"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "102"),
                        FullName = "Sam Smith",
                        TelNo = "32-54",
                        Position = "Manager"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "102"),
                        FullName = "Karl Michael",
                        TelNo = "35-47",
                        Position = "System administrator"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "102"),
                        FullName = "Valentine Kuprin",
                        TelNo = "64-36",
                        Position = "System administrator lead"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "103"),
                        FullName = "Jessica Parkson",
                        TelNo = "96-87",
                        Position = "Manager"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "103"),
                        FullName = "Mona Gisulina",
                        TelNo = "34-75",
                        Position = "Lead developer"
                    },
                    new Entities.Employee
                    {
                        Department = dbContext.Departments.FirstOrDefault(x => x.Code == "103"),
                        FullName = "Aaron Alfred",
                        TelNo = "12-47",
                        Position = "Developer"
                    }
                    );
                dbContext.SaveChanges();
            }
        }
    }
}
