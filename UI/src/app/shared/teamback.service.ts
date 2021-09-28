import { Injectable } from '@angular/core';
import { Team } from './team.model';
import {HttpClient} from '@angular/common/http';
import * as Amqp from "amqp-ts";

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

  getMessage(){
    var connection = new Amqp.Connection("amqp://localhost:5672");
var exchange = connection.declareExchange("Consumer");
var queue = connection.declareQueue("test-queue");
queue.bind(exchange);
queue.activateConsumer((message) => {
    console.log("Message received: " + message.getContent());
});

var msg = new Amqp.Message("Test");
exchange.send(msg);
 
connection.completeConfiguration().then(() => {
    // the following message will be received because
    // everything you defined earlier for this connection now exists
    var msg2 = new Amqp.Message("Test2");
    exchange.send(msg2);
});
  }
}
