using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MaqtaContext _context;

        public EmployeeRepository(MaqtaContext context)
        {
            this._context = context;
        }

        public async Task DeleteAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            Employee employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<Employee> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            return await _context.Employees.FindAsync(id);
        }

        public async Task<IReadOnlyList<Employee>> GetAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            Employee existingEmployee = _context.Employees.FirstOrDefault(e => e.Id == employee.Id);

            if (existingEmployee == null)
            {
                throw new ArgumentNullException(nameof(existingEmployee));
            }

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            existingEmployee.MobileNumber = employee.MobileNumber;
            existingEmployee.Address = employee.Address;
            existingEmployee.Email = employee.Email;

            _context.Attach(existingEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
