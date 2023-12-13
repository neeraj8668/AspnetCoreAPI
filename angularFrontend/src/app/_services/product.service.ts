import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { environment } from '@environments/environment';
import { Product, User } from '@app/_models';

const baseUrl = `${environment.apiUrl}/products`;
const headers = new HttpHeaders({
  'Content-Security-Policy': '...',
  'Strict-Transport-Security': 'max-age=31536000; includeSubDomains',
  'X-Content-Type-Options': 'nosniff',
  'X-Frame-Options': 'DENY',
  'X-XSS-Protection': '1; mode=block'
});
@Injectable({ providedIn: 'root' })
export class ProductService {
    constructor(private http: HttpClient) { }

    getAll(): Observable<Product[]> {
        return this.http.get<Product[]>(baseUrl, { headers }).pipe(catchError(this.handleError));
      }
    
      getById(id: string): Observable<Product> {
        return this.http.get<Product>(`${baseUrl}/${id}`,{headers}).pipe(catchError(this.handleError));
      }
    
      create(product: Product): Observable<Product> {
        return this.http.post<Product>(baseUrl, product, { headers }).pipe(catchError(this.handleError));
      }
    
      update(id: string, product: Product): Observable<void> {
        return this.http.put<void>(`${baseUrl}/${id}`, product, { headers }).pipe(catchError(this.handleError));
      }
    
      delete(id: string): Observable<void> {
        return this.http.delete<void>(`${baseUrl}/${id}`, { headers }).pipe(catchError(this.handleError));
      }
    
      private handleError(error: any) {
        console.error('API error:', error);
        return throwError('An error occurred. Please try again later.');
      }
}