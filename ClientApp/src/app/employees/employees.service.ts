import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from 'src/environments/environment';
import { IEmployee } from '../shared/models/employee';

@Injectable({
  providedIn: 'root',
})
export class EmployeesService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getEmployees(): Observable<IEmployee[]> {
    return this.http.get<IEmployee[]>(this.baseUrl + 'employees');
  }
  getEmployeeById(employeeId: number): Observable<IEmployee> {
    return this.http.get<IEmployee>(this.baseUrl + 'employees/' + employeeId);
  }
  createEmployee(employee: IEmployee) {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
    };
    return this.http.post(this.baseUrl + 'employees', employee, {
      responseType: 'text',
    });
  }
  updateEmployee(employee: IEmployee) {
    return this.http.put(this.baseUrl + 'employees', employee, {
      responseType: 'text',
    });
  }
  deleteEmployeeById(employeeid: number): Observable<string> {
    return this.http.delete(this.baseUrl + 'employees/' + employeeid, {
      responseType: 'text',
    });
  }
}
