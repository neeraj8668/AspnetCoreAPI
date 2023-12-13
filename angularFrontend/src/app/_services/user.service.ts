import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { User } from '@app/_models';

const baseUrl = `${environment.apiUrl}/users`;
const headers = new HttpHeaders({
  'Content-Security-Policy': '...',
  'Strict-Transport-Security': 'max-age=31536000; includeSubDomains',
  'X-Content-Type-Options': 'nosniff',
  'X-Frame-Options': 'DENY',
  'X-XSS-Protection': '1; mode=block'
});
@Injectable({ providedIn: 'root' })
export class UserService {
    constructor(private http: HttpClient) { }

    getUsers(): Observable<User[]> {
        return this.http.get<User[]>(baseUrl, { headers }).pipe(catchError(this.handleError));
      }
    
      getUserById(id: string): Observable<User> {
     
        return this.http.get<User>(`${baseUrl}/${id}`, { headers }).pipe(catchError(this.handleError));
      }
    
      createUser(user: User): Observable<User> {
        return this.http.post<User>(baseUrl, user, { headers }).pipe(catchError(this.handleError));
      }
    
      updateUser(id: string, user: User): Observable<void> {
        return this.http.put<void>(`${baseUrl}/${id}`, user, { headers }).pipe(catchError(this.handleError));
      }
    
      deleteUser(id: any): Observable<void> {
        return this.http.delete<void>(`${baseUrl}/${id}`, { headers }).pipe(catchError(this.handleError));
      }
      private handleError(error: any) {
        console.error('API error:', error);
        return throwError('An error occurred. Please try again later.');
      }
}