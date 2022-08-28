import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { IEmployee } from '../shared/models/employee';
import { EmployeesService } from './employees.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.scss'],
})
export class EmployeesComponent implements OnInit {
  employees?: IEmployee[];
  dataSaved = false;
  employeeForm: any;
  //allEmployees: Observable<IEmployee[]>;
  employeeIdUpdate: any;
  message = '';

  constructor(
    private formbulider: FormBuilder,
    private employeesService: EmployeesService
  ) {}
  ngOnInit(): void {
    this.employeeForm = this.formbulider.group({
      FirstName: ['', [Validators.required]],
      LastName: ['', [Validators.required]],
      Email: ['', [Validators.required]],
      Address: ['', [Validators.required]],
      MobileNumber: ['', [Validators.required]],
    });

    this.loadAllEmployees();
  }

  loadAllEmployees() {
    this.employeesService.getEmployees().subscribe(
      (response) => {
        this.employees = response;
      },
      (error) => console.log(error)
    );
  }
  onFormSubmit(values: any) {
    this.dataSaved = false;
    const employee = values;
    this.CreateEmployee(employee);
    this.employeeForm.reset();
  }
  loadEmployeeToEdit(employeeId: number) {
    this.employeesService.getEmployeeById(employeeId).subscribe((employee) => {
      this.message = '';
      this.dataSaved = false;
      this.employeeIdUpdate = employee.id;
      this.employeeForm.controls['FirstName'].setValue(employee.firstName);
      this.employeeForm.controls['LastName'].setValue(employee.lastName);
      this.employeeForm.controls['Email'].setValue(employee.email);
      this.employeeForm.controls['Address'].setValue(employee.address);
      this.employeeForm.controls['MobileNumber'].setValue(
        employee.mobileNumber
      );
    });
  }
  CreateEmployee(employee: IEmployee) {
    if (this.employeeIdUpdate == null) {
      this.employeesService.createEmployee(employee).subscribe(() => {
        this.dataSaved = true;
        this.message = 'Record saved Successfully';
        this.loadAllEmployees();
        this.employeeIdUpdate = null;
        this.employeeForm.reset();
      });
    } else {
      employee.id = this.employeeIdUpdate;
      this.employeesService.updateEmployee(employee).subscribe(() => {
        this.dataSaved = true;
        this.message = 'Record Updated Successfully';
        this.loadAllEmployees();
        this.employeeIdUpdate = null;
        this.employeeForm.reset();
      });
    }
  }
  deleteEmployee(employeeId: number) {
    if (confirm('Are you sure you want to delete this ?')) {
      this.employeesService.deleteEmployeeById(employeeId).subscribe(() => {
        this.dataSaved = true;
        this.message = 'Record Deleted Succefully';
        this.loadAllEmployees();
        this.employeeIdUpdate = null;
        this.employeeForm.reset();
      });
    }
  }

  resetForm() {
    this.employeeForm.reset();
    this.message = '';
    this.dataSaved = false;
  }
}
