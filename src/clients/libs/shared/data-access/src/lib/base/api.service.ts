/* eslint-disable @typescript-eslint/ban-types */
import { HttpClient, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { AppConfig, APP_CONFIG } from '@drreview/shared/utils/app-config';

import { Observable } from 'rxjs';

@Injectable()
export class ApiService {
  public httpHeaders: HttpHeaders = new HttpHeaders({
    'Content-Type': 'application/json',
    "Access-Control-Allow-Origin": "*",
    'Accept': 'application/json'});

  public constructor(private http: HttpClient,
                     @Inject(APP_CONFIG) private appConfig: AppConfig) { }

  public get<T>(url: string, params: HttpParams = new HttpParams()): Observable<T> {
    return this.http.get<T>(`${this.appConfig.apiUrl}${url}`, { headers: this.httpHeaders, params });
  }


  public getWithOriginalUrl<T>(url: string, responseType: string, parameters: HttpParams = new HttpParams()): Observable<T> {
    const options = {
      headers: this.httpHeaders,
      params: parameters,
      origin: window.location.host
     };

    return this.http.get<T>(`${url}`, options);
  }

  public getWithHeaderAppend<T>(url: string, name: string, value: string, params: HttpParams = new HttpParams()): Observable<T> {
    return this.http.get<T>(`${this.appConfig.apiUrl}${url}`, { headers: this.httpHeaders.append(name, value), params });
  }

  public post<T>(url: string, data: Object = { }): Observable<T> {
    return this.http.post<T>(`${this.appConfig.apiUrl}${url}`, JSON.stringify(data), { headers: this.httpHeaders });
  }

  public put<T>(url: string, data: Object = { }): Observable<T> {
    return this.http.put<T>(`${this.appConfig.apiUrl}${url}`, JSON.stringify(data), { headers: this.httpHeaders });
  }

  public delete<T>(url: string): Observable<T> {
    return this.http.delete<T>(`${this.appConfig.apiUrl}${url}`, { headers: this.httpHeaders });
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  public deleteWithBody<R>(url: string, data: Object = { }): Observable< R> {
    return this.http.request<R>('delete', `${this.appConfig.apiUrl}${url}`, { body: JSON.stringify(data), headers: this.httpHeaders});
  }

  public patch<T>(url: string, data: Object = { }): Observable<T> {
    return this.http.patch<T>(`${this.appConfig.apiUrl}${url}`, JSON.stringify(data), { headers: this.httpHeaders});
  }

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  public request(request: HttpRequest<any>): Observable<any> {
      return this.http.request(request);
  }
}
