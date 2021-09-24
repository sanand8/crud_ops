import { Component, OnInit } from '@angular/core';
import { TeambackService } from 'src/app/shared/teamback.service';
import { FormsModule, NgForm } from '@angular/forms';
import { Team } from 'src/app/shared/team.model';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-teamform',
  templateUrl: './teamform.component.html',
  styles: [
  ]
})
export class TeamformComponent implements OnInit {

  constructor(public service:TeambackService,
    private toastr:ToastrService) { }

  ngOnInit(): void {
  }
  onSubmit(form:NgForm){
    if(this.service.formData.number==0){
      this.inserRecord(form);
    }
    else{
      this.updateRecord(form);
    }
  }

  inserRecord(form:NgForm){
    this.service.postTeamDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.success('Submitted Successfully','Player Registered');
      },
      err => { console.log(err);},
    )
  }

  updateRecord(form:NgForm){
    this.service.putTeamDetail().subscribe(
      res => {
        this.resetForm(form);
        this.service.refreshList();
        this.toastr.info('Updated Successfully','Player Registered');
      },
      err => { console.log(err);},
    )
  }

  resetForm(form:NgForm){
    form.form.reset();
    this.service.formData = new Team();
  }
}
