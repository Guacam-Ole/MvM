import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup,  Validators } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { MatStepper } from '@angular/material/stepper';

const url:String="http://localhost:5000";  // TODO: Config

@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.css']
})



export class UploaderComponent implements OnInit, AfterViewInit {

  uploaderGroup:FormGroup;
  lastfile:any=null;
  uploading:boolean=false;
  uploadFormControl=new FormControl('',[]);
  codeIsValid:boolean=false;
  codeChecked:boolean=false;
  readyToUpload:boolean=false;
  httpHeaders = new HttpHeaders({
    'Content-Type' : 'application/json',
    'Cache-Control': 'no-cache'
  });
  stepperConfig:any={}
  



  constructor(private _formbuilder:FormBuilder, private _http:HttpClient) { 
    this.uploaderGroup=this._formbuilder.group( {
      name:'',
      code: '',
      podcasttitle:'jjj',
      desciption:'dfk',
      uploadfile:null,
      participants:this._formbuilder.array([])
    });
    this.stepperConfig=  {
      code:true,
      podcast:false,
      upload:false,
      goodbye:false
    };
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

  storeEpisode(stepper:MatStepper) {
    var codeField=this.uploaderGroup.get("code");
    var code=codeField?.value;

    this.uploading=true;
    // Store Episode-Data to Server
    this._http.post<any>(url+'/episode/',  
    { 
      'code': code,
      'podcasttitle':'whatever',
      'description':'superduper',
      'participants':this.uploaderGroup.get("participants")?.value
    } , { headers:this.httpHeaders}).subscribe(data => {
      if (data.success) {
        var episodeId=data.value;  
        this.up(stepper, episodeId);
      } else {
        this.uploading=false;
        // TODO: Show Error
        
      }
    });
  }

  up(stepper:MatStepper, episodeId:string) {
    if (this.lastfile==null) {
      this.uploading=false;
      return; // TODO: Show Error
    } 


    var codeField=this.uploaderGroup.get("code");
    var code=codeField?.value;

    var episodeContents=new FormData();
    // episodeContents.append('episode', JSON.stringify(
    // { 
    //   'code': code,
    //   'podcasttitle':'whatever',
    //   'description':'superduper',
    //   'participants':this.uploaderGroup.get("participants")?.value
    // }));
    episodeContents.append('id',episodeId);
    episodeContents.append('file',this.lastfile);
    this.uploading=true;
    this._http.post<any>(url+'/episode/start', episodeContents, {
        headers: {}
    }).subscribe(data=> {
      this.uploading=false;
      console.log(data);
      var productionStarted=data.success;
      var productionId=data.value;
      if (productionStarted) {
        this.stepperConfig.upload=false;
        this.stepperConfig.goodbye=true;
        stepper.next();
      } // Else: Error
      
    });
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



  validateCode(stepper:MatStepper) {
    var codeField=this.uploaderGroup.get("code");
    var code=codeField?.value;

    this._http.post<any>(url+'/episode/codeisvalid',  { 'code': code} , { headers:this.httpHeaders}).subscribe(data => {
      this.codeIsValid=data.success;
      if (!this.codeIsValid) {
        codeField?.setErrors({"codevalid":"false"}) ;
      } else {
        this.stepperConfig.code=false;
        this.stepperConfig.podcast=true;
        stepper.next()
      }
      this.codeChecked=true;
    });
  }

  ngOnInit() {


    }
}
