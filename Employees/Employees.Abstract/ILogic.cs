using System;
using System.Collections.Generic;
using System.Text;
using Employees.Models.DTO;

namespace Employees.Abstract
{
    public interface ILogic
    {

        /// <summary>
        /// Добавляем или изменяем сотрудника
        /// </summary>
        /// <param name="employeeDTO"></param>
        void AddOrUpdateEmployee(EmployeeDTO employeeDTO);

        /// <summary>
        /// Получаем сотрудника
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EmployeeDTO GetEmployee(int id);

        /// <summary>
        /// Получаем сотрудников с учетом фильтрации
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<EmployeeDTO> GetEmployees(EmployeeDTO search);

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="id"></param>
        void RemoveEmployee(int id);

        /// <summary>
        /// Добавляем или изменяем подразделение
        /// </summary>
        /// <param name="departmentDTO"></param>
        void AddOrUpdateDepartment(DepartmentDTO departmentDTO);

        /// <summary>
        /// Получаем подразделение
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DepartmentDTO GetDepartment(int id);

        /// <summary>
        /// Получаем подразделение с учетом фильтрации
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<DepartmentDTO> GetDepartments(DepartmentDTO search);

        /// <summary>
        /// Создаем данные для примера
        /// </summary>
        void CreateData();
    }
}
