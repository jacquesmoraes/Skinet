import { Injectable } from '@angular/core';
import { BehaviorSubject, map } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../shared/Models/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
baseUrl = environment.apiUrl;
private currentUserSource = new BehaviorSubject<User | null>(null);
currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private router : Router) { }

login(values:any){
  return this.http.post<User>(this.baseUrl + 'account/login', values).pipe(
    map(user =>{
      localStorage.setItem('token', user.token);
      this.currentUserSource.next(user);
    
    })
  )
}

register(values: any){
  return this.http.post<User>(this.baseUrl + 'account/register', values).pipe(
    map(user =>{
      localStorage.setItem('token', user.token);
      this.currentUserSource.next(user);
    })
  )
}

logout(){
  localStorage.removeItem('token');
  this.currentUserSource.next(null);
  this.router.navigateByUrl('/');
}

checkEmailExists(email: string){
  return this.http.get<boolean>(this.baseUrl + 'account/emailexists?email=' + email);
}



}
