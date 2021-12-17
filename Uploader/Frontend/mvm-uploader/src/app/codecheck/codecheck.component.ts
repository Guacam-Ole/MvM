import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup,  Validators } from '@angular/forms';

@Component({
  selector: 'app-codecheck',
  templateUrl: './codecheck.component.html',
  styleUrls: ['./codecheck.component.css']
})

export class CodecheckComponent implements OnInit {
  isLinear=true;
  stepCodeInput=new FormGroup({});
  stepParticipants=new FormGroup({});
  uploadFormControl=new FormControl('',[]);


  constructor(private _formbuilder:FormBuilder) { }

  ngOnInit() {
    // this.stepCodeInput=this._formbuilder.group({
    
    //   firstCtrl: ['', Validators.required]
    // });
  }
}
