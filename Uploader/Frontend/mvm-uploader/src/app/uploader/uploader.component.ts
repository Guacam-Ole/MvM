import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { ThrowStmt } from '@angular/compiler';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup,  Validators } from '@angular/forms';
import { MatStepper } from '@angular/material/stepper';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { lastValueFrom } from 'rxjs';

const url:String="http://localhost:5000";  // TODO: Config

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.css']
})



export class UploaderComponent implements OnInit, AfterViewInit {
  isLinear=true;
  uploaderGroup:FormGroup;
  episodeId:string="missing";
  lastfile:any=null;
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
      participants:this._formbuilder.array([])
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

initEpisode() {
  var codeField=this.uploaderGroup.get("code");
  var code=codeField?.value;

  // Store Episode-Data to Server
  this._http.post<any>(url+'/episode/',  
  { 
    'code': code,
    'podcasttitle':'whatever',
    'description':'superduper',
    'participants':this.uploaderGroup.get("participants")?.value
  } , { headers:this.httpHeaders}).subscribe(data => {
    if (data.success) {
      this.episodeId=data.value;  
    }    
  });
}

up() {
  if (this.lastfile==null) return;
 // if (this.lastfile==file.Name) return;
  var episodeContents=new FormData();
  episodeContents.append('id',this.episodeId);
  episodeContents.append('file',this.lastfile);
  this._http.post<any>(this.getBaseUrl()+'/episode/start', episodeContents, {
      headers: {}
  }).subscribe(data=> {
    console.log(data);
    var productionStarted=data.success;
    var productionId=data.value;

  })
  console.log(this.lastfile);
}

fileselected(file:HTMLInputElement) {
  console.log(file);
  if (file.files==null || file.files.length!=1) {
    this.lastfile=null;
    return;
  }
  this.lastfile=file.files[0];
}


getBaseUrl():String {
  return url;
}

createUploadUrl():string {
  return 'http://localhost:5000/episode/'+this.episodeId+'/start';
}




  validateCode() {
    var codeField=this.uploaderGroup.get("code");
    var code=codeField?.value;
  
    this._http.post<any>(url+'/episode/codeisvalid',  { 'code': code} , { headers:this.httpHeaders}).subscribe(data => {
      this.codeIsValid=data.success;
      if (!this.codeIsValid) codeField?.setErrors({"codevalid":"false"});
      this.codeChecked=true;
    });
  }

  ngOnInit() {


   }
}
