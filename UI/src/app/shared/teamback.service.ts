import { Injectable } from '@angular/core';
import { Team } from './team.model';
import {HttpClient} from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class TeambackService {

  constructor(private http:HttpClient) { }
  readonly baseUrl = 'https://localhost:44300/api/players/'
  formData: Team = new Team();
  list: Team[];

  postTeamDetail(){
    return this.http.post(this.baseUrl,this.formData);
  }

  putTeamDetail(){
    return this.http.put<Team>(`${this.baseUrl}${this.formData.number}`,this.formData);
  }

  deletePlayer(number:number){
    return this.http.delete(`${this.baseUrl}${number}`);
  }

  refreshList(){
    this.http.get(this.baseUrl).toPromise().then(res =>this.list = res as Team[]);
  }

  
}
