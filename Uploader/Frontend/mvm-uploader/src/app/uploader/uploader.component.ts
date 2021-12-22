import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { ThrowStmt } from '@angular/compiler';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup,  Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { HttpClient, HttpHeaders } from '@angular/common/http';

const url:String="http://localhost:29438";  // TODO: Config

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.css']
})



export class UploaderComponent implements OnInit, AfterViewInit {
  isLinear=true;
  uploaderGroup:FormGroup;
  uploadFormControl=new FormControl('',[]);
  codeIsValid:boolean=false;
  codeChecked:boolean=false;
  readyToUpload:boolean=false;
  httpHeaders = new HttpHeaders({
    'Content-Type' : 'application/json',
    'Cache-Control': 'no-cache'
  });

  constructor(private _formbuilder:FormBuilder, private _http:HttpClient) { 
    this.uploaderGroup=this._formbuilder.group( {
      name:'',
      code: '',
      podcasttitle:'jjj',
      desciption:'dfk',
      uploadfile:null,
      participants:this._formbuilder.array([]),

    });
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

  startUpload() {
    console.log("jjjjj");
    var file=this.uploaderGroup.get("uploadfile");
    console.log(file);
  }

  validateCode() {
    var codeField=this.uploaderGroup.get("code");
    var code=codeField?.value;
    console.log("code="+code);


  
  
    this._http.post<boolean>(url+'/upload/codeisvalid',  { 'code': code} , { headers:this.httpHeaders}).subscribe(data => {
      this.codeIsValid=data;
      if (!this.codeIsValid) codeField?.setErrors({"codevalid":"false"});
      this.codeChecked=true;
    });
  }

  ngOnInit() {


   }
}
