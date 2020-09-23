import { Component, OnInit, Output, Input, EventEmitter } from '@angular/core';
import { DynamicValueDto, DynamicFieldsServiceProxy } from '@shared/service-proxies/service-proxies';
import { DomSanitizer } from '@angular/platform-browser';
import 'devextreme/integration/jquery';
import * as $ from 'jquery';

@Component({
    selector: 'dynamicModule',
    templateUrl: './dynamicModule.component.html'
})
export class DynamicModuleComponent {
    // @Input() currentId: any;
    @Input() link = '';
    @Input() parameters: any;

    @Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

    currentId: any;
    dynamicFields: any;
    formData: DynamicValueDto;
    dynamicData: any;
    objectID: any;
    moduleId: any;
    isSetData = false;
    error = true;
    constructor(private _dynamicFieldService: DynamicFieldsServiceProxy,
                private _sanitizer: DomSanitizer) {
    }

    loadDynamicField(inputId : any, link: string){ 
        console.log(inputId, link, this.link);
        return new Promise((resolve, reject)=>{
            if (this.error) {
                const self = this;
                // this.href = this.router.url;
                // console.log(this.router.url);
                this.currentId = inputId;              
                self._dynamicFieldService.getDynamicFields(link, inputId).subscribe((res: any) => {
                    console.log(res);
                    if (res.length > 0 ) {
                        self.isSetData = true;
                        self.dynamicData = res;            
                        var div = '';
                        self.objectID = this.currentId;
                        self.moduleId = res[0].moduleID;
                        res.forEach(function (value) {
                            div += '<div class="col-sm-'+value.classAttach+'">';
                            div += '<label class="col-sm-'+value.widthDescription+' col-form-label margin-label">'+value.nameDescription+'</label>'
                            //comment vì dòng này thêm vào class col sẽ làm element bị thụt vào thêm 1 đoạn nữa
                            //div += '<div class="col-sm-'+value.width+'">'
                            div += '<div>'
                            if(value.typeField == 2){ // input
                                if(value.valueId != 0){
                                    div += '<input id="dynamic'+value.name+'" type="text" class="form-control" value="'+value.value+'" >'
                                }else{
                                    div += '<input id="dynamic'+value.name+'" type="text" class="form-control" >'
                                }
                            }else if(value.typeField == 3 || value.typeField == 1 || value.typeField == 4){ // combobox,checkbox,datebox
                                div += '<div id="dynamic'+value.name+'"></div>';
                            }

                            div += '</div></div>'
                        });

                        self.dynamicFields = this._sanitizer.bypassSecurityTrustHtml(div);
                        console.log(self.dynamicFields);
                    }else{
                        self.isSetData = false;
                        self.dynamicFields = this._sanitizer.bypassSecurityTrustHtml("");
                    }
                    resolve('done');
                });
            } else {
                reject('error');
            }
        }).then((val) => {
                    if(this.isSetData){
                        setTimeout(() => 
                        {
                            this.datebox();
                            this.selectBox();
                            this.checkbox();
                        },500)}
                    },
                (err) => {
                    console.error(err);
                    });
    }

    selectBox(){
        this.dynamicData.forEach(obj =>{
            if(obj.typeField == 3){
                this.dataSourceSelectBox(obj.id, this.parameters).then((val) => {setTimeout(() => {
                    var dataSource = [];
                    dataSource.push(val);
                    var id = 'dynamic'+obj.name;
                    $("#"+id).dxSelectBox({
                        items: dataSource[0],
                        valueExpr: "key",
                        displayExpr: "value",
                    });
                    if(obj.value != 0 && obj.value != "")
                    {
                        console.log(obj.value)
                        $("#"+id).dxSelectBox("instance").option("value", obj.value);
                    }
                },500) });
            }
        });
    }

    dataSourceSelectBox(dynamicFieldId : any, parameters: any){
        return new Promise((resolve, reject)=>{
            this._dynamicFieldService.getDataSourceDynamic(dynamicFieldId, this.currentId, parameters).subscribe((res: any) => {
                console.log(res);
                resolve(res);
            });
        });

    }

    checkbox(){
        this.dynamicData.forEach(obj =>{
            if(obj.typeField == 1){
                var id = 'dynamic'+obj.name;
                var check = false;
                //var check = (obj.value == "true") ? true : false;
                if(obj.value){
                    check = (obj.value == "true") ? true : false;
                }
                $("#"+id).dxCheckBox({
                    value: check
                });
            }
        });
    }

    datebox(){
        this.dynamicData.forEach(obj => {
            if(obj.typeField == 4){
                var id = 'dynamic'+obj.name;
                let x = document.getElementById(id);
                ($("#"+id) as any).dxDateBox({
                    displayFormat: "dd/MM/yyyy",
                    type: 'date',
                    value: (obj.value != '') ? new Date(obj.value) : new Date()
                });
            }
        });
    }

    saveDynamicValue(): boolean{
        const self = this;
        var objData =[];
        this.dynamicData.forEach(obj =>{
            this.formData = new DynamicValueDto();        
            var id = 'dynamic'+obj.name;
            self.formData.key = obj.name;
            if(obj.typeField == 1)
            {
                if($("#"+id).dxCheckBox('instance').option('value')){
                    self.formData.value = "true";
                }else{
                    self.formData.value = "false";
                }
            }
            else if(obj.typeField == 2){
                self.formData.value = $("#"+id).val().toString();
            }else if(obj.typeField == 3){
                self.formData.value = $("#"+id).dxSelectBox('instance').option('value');
            }else if(obj.typeField == 4){
                self.formData.value = $("#"+id).dxDateBox('instance').option('value');
            }
            self.formData.dynamicFieldId = obj.id;
            self.formData.objectId = self.currentId;
            self.formData.id = obj.valueId;
            //console.log(self.formData);
            objData.push(self.formData);
        });
        this._dynamicFieldService.insertUpdateDynamicFields(objData).subscribe((res: any) => {

        });
        return;
    }

    ngOnInit() {
        //this.loadDynamicField(10);
    }

}
