import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Team } from '../shared/team.model';
import { TeambackService } from '../shared/teamback.service';

@Component({
  selector: 'app-teamdetails',
  templateUrl: './teamdetails.component.html',
  styles: [
  ]
})
export class TeamdetailsComponent implements OnInit {

  constructor(public service: TeambackService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
    this.service.refreshList();
  }
  populateForm(selectedRecord:Team){
    this.service.formData = Object.assign({},selectedRecord);
  }

  onDelete(id:number){
    if(confirm('Are you sure to delete')){
      this.service.deletePlayer(id)
    .subscribe(
      res=>{
        this.service.refreshList();
        this.toastr.error("Deleted Successfully",'Team List Refreshed');
      },
      err=>{console.log(err)}
    ) 
    }
    
  }
}
