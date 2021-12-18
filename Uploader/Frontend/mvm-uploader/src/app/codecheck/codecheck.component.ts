import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { ThrowStmt } from '@angular/compiler';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup,  Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';

@Component({
  selector: 'app-codecheck',
  templateUrl: './codecheck.component.html',
  styleUrls: ['./codecheck.component.css']
})

export class CodecheckComponent implements OnInit, AfterViewInit {
  
  isLinear=true;
  uploaderGroup:FormGroup;
  uploadFormControl=new FormControl('',[]);
  codeError:string | null;

  constructor(private _formbuilder:FormBuilder) { 
    this.uploaderGroup=this._formbuilder.group( {
      name:'',
      code: '',
      participants:this._formbuilder.array([])
    });
    this.codeError=null;
  }


  participants():FormArray {
    return this.uploaderGroup.get("participants") as FormArray;
  }

  newParticipant():FormGroup {
    return this._formbuilder.group({
      podcast:'',
      name:'',
      twitter:''
    });
  }

  newKnownParticipant( podcast: string,name:string, twitter:string):FormGroup {
    return  this._formbuilder.group( {
      podcast:podcast,
      name:name,
      twitter:twitter
    });
  }

  fillDemoData() {
   this.participants().push(   this.newKnownParticipant("schnuff","di","schnuff"));
   this.participants().push(this.newKnownParticipant("Schnaff", "di", "Schniff"));  
  }

  addParticipant() {
    this.participants().push(this.newParticipant());
  }

  removeParticipant(i:number) {
    this.participants().removeAt(i)
  }

  ngAfterViewInit() {
    this.fillDemoData();
  }

  validateCode() {
    var codeField=this.uploaderGroup.get("code");
    var code=codeField?.value;
    console.log("code="+code);

    // TODO: Api-Call (obviously)
    var codeIsValid=code=="dbddhkp";

this.codeError="Bissu blöde?";

    if (codeIsValid) {
      codeField?.clearValidators();
      codeField?.markAsPristine();
    } else {
      codeField?.setValidators(Validators.required);
      codeField?.setErrors({required:true })
      codeField?.markAsDirty();
    }
    
    return code=="dbddhkp";
  }

  ngOnInit() { }
}
