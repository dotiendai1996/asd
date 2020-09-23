import {
    Component,
    Injector,
    ViewEncapsulation,
    ViewChild,
    OnInit
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import {
    SqlConfigDetailsServiceProxy,
    SqlConfigDetailDto,
    SqlConfigsServiceProxy
} from "@shared/service-proxies/service-proxies";
import { NotifyService } from "@abp/notify/notify.service";
import { AppComponentBase } from "@shared/common/app-component-base";
import { appModuleAnimation } from "@shared/animations/routerTransition";
import * as _ from "lodash";
import * as moment from "moment";
import { isNullOrUndefined } from "util";
import { DxDataGridComponent, DxFormComponent } from "devextreme-angular";
import { Location } from '@angular/common';

@Component({
    templateUrl: "./modify-sqlConfigDetails.component.html",
    encapsulation: ViewEncapsulation.None,
    animations: [appModuleAnimation()]
})
export class ModifySqlConfigDetailsComponent extends AppComponentBase
    implements OnInit {
    @ViewChild("reportDg", { static: true }) reportDg: DxDataGridComponent;
    @ViewChild("data2table", { static: true }) data2table: DxDataGridComponent;
    @ViewChild("form", { static: true }) form: DxFormComponent;

    //@ViewChild(DxTreeViewComponent) treeView;
    treeBoxValue: string[];
    treeView: any;
    currentReportId: any;
    currentTab: number;
    searchData: any;
    dataSourceTab1: any;
    dataSourceTab2: any;
    columnItems: any = [];
    dataSourceTest3: any;
    dataTemp = [];
    listColumnSave: any;
    columnBuilder = [];
    columnConfig = [];
    isEdit = false;
    listItem: any = [];
    sqlContent = "";
    parentOptions: any;
    dataSource: any;
    //key: any;
    //modalAdd: any;
    reportName: any;
    formData: any = [];
    paramTest: any = [];
    pageSize: any = 100;
    pageIndex: any = 100;
    listParentCode: any = [];
    curentControlData: any;
    templateChartData: any = [];
    visible: boolean = false;
    tbTestVisible3: boolean = false;
    textAlignment = [
        {
            key: "center",
            display: "Center"
        },
        {
            key: "left",
            display: "Left"
        },
        {
            key: "right",
            display: "Right"
        }
    ];

    _dataType = [
        {
            key: "string",
            display: "String"
        },
        {
            key: "number",
            display: "Number"
        },
        {
            key: "date",
            display: "Date"
        },
        {
            key: "boolean",
            display: "Boolean"
        },
        {
            key: "datetime",
            display: "DateTime"
        }
    ];

    groupSortdata = [
        {
            key: "asc",
            display: "Tăng dần"
        },
        {
            key: "desc",
            display: "Giảm dần"
        }
    ];

    groupLeveldata = [
        {
            key: -1,
            display: "không"
        },
        {
            key: 0,
            display: 1
        },
        {
            key: 1,
            display: 2
        },
        {
            key: 2,
            display: 3
        },
        {
            key: 3,
            display: 4
        },
        {
            key: 4,
            display: 5
        }
    ];
    constructor(
        injector: Injector,
        private _sqlConfigDetailsService: SqlConfigDetailsServiceProxy,
        private _sqlConfigService: SqlConfigsServiceProxy,
        private router: Router,
        private _location: Location,
        private activeRoute: ActivatedRoute
    ) {
        super(injector);
        // this.searchData = retrieveState() || {
        //     name: '',
        //     code: '',
        //     pageSize: 20,
        //     pageIndex: 0,
        //     sortBy: 'orderId'
        // };
        //this.initDataSourceTab1();
    }

    ngOnInit() {
        const self = this;
        this.activeRoute.params.subscribe(param => {
            this.currentReportId = param.id;
            self._sqlConfigService
                .getSqlConfigForView(this.currentReportId)
                .subscribe(res => {
                    self.sqlContent = res.sqlConfig.sqlContent;
                    //self.reportName = res.sqlConfig.name;
                    // if (res.c.formID) {
                    //     this.visible = false;
                    // } else {
                    //     this.visible = true;
                    // }
                });
        });
    }

    add() {
        this.router.navigate([
            "/pages/config-report/report/addfilter/" +
                this.currentReportId +
                "/null"
        ]);
    }

    detail(id) {
        this.router.navigate([
            "/pages/config-report/report/addfilter/" +
                this.currentReportId +
                "/" +
                id
        ]);
    }

    loadtab2() {}

    delete(id) {
        const self = this;
        // showConfirm('Bạn có muốn xóa', 'Xóa tiêu chí này sẽ không được phục hồi', () => {
        //     self.configReportService.deleteReportFilter(id).subscribe(res => {
        //         noticeSuccess('Xóa custom field thành công');
        //         self.initDataSourceTab1();
        //     });
        // });
    }

    back() {
        this._location.back();
    }

    check() {
        const self = this;
        self.paramTest = [];

        // self.configReportService.checkSQLwithParam(self.currentReportId, self.sqlContent).subscribe((res: any) => {
        //     if (res.isSucceeded == true) {
        //         noticeSuccess('Thành công');
        //     }
        //     else {
        //         noticeFail('Lỗi');
        //     }
        // })
    }

    createColumn() {
        const self = this;
        self.listColumnSave = [];
        self.columnItems = [];
        var rowcount = self.data2table.instance.totalCount();
        if (rowcount != 0) {
            self.data2table.instance.selectAll();
            self.data2table.dataSource = [];
        }
        this.dataSourceTab2 = {
            load: function(loadOptions: any) {
                const promise = new Promise((resolve, reject) => {
                    self._sqlConfigDetailsService
                        .getColumn(self.currentReportId, self.sqlContent)
                        .subscribe(res => {
                            // console.log(res);
                            if (res.isSucceeded == true) {
                                if (self.isEdit == false) {
                                    self.columnItems = res.data;
                                    // console.log(res.data);
                                }
                                resolve(self.columnItems);
                                self.isEdit = false;
                            } else {
                                //noticeFail('Lỗi');
                            }
                        });
                });
                return promise;
            },
            key: "code",
            update: function(key, values) {
                var index = self.data2table.instance.getRowIndexByKey(key);
                console.log(values);
                console.log(self.columnItems[index]);
                if (!isNullOrUndefined(values.isActive))
                    self.columnItems[index].isActive = values.isActive;
                if (!isNullOrUndefined(values.isDisplay))
                    self.columnItems[index].isDisplay = values.isDisplay;
                if (!isNullOrUndefined(values.isSum))
                    self.columnItems[index].isSum = values.isSum;
                if (!isNullOrUndefined(values.groupLevel))
                    self.columnItems[index].groupLevel = values.groupLevel;
                if (!isNullOrUndefined(values.width))
                    self.columnItems[index].width = values.width;
                if (!isNullOrUndefined(values.name))
                    self.columnItems[index].name = values.name;
                if (!isNullOrUndefined(values.groupSort))
                    self.columnItems[index].groupSort = values.groupSort;
                if (!isNullOrUndefined(values.type))
                    self.columnItems[index].type = values.type;
                if (!isNullOrUndefined(values.format))
                    self.columnItems[index].format = values.format;
                if (!isNullOrUndefined(values.colNum)) {
                    self.columnItems[index].colNum = values.colNum;
                }
                if (!isNullOrUndefined(values.cellTemplate)) {
                    self.columnItems[index].cellTemplate = values.cellTemplate;
                }
                self.isEdit = true;
            }
        };
    }

    save() {
        const self = this;
        self.data2table.instance.saveEditData();
        // console.log(self.columnItems);
        // console.log(this.data2table.instance.getDataSource());
        this._sqlConfigDetailsService
            .createConfigIfNotExists(this.currentReportId, self.columnItems)
            .subscribe(
                result => {
                    console.log(result);
                    this.message.success("Lưu thành công!");
                    self._sqlConfigDetailsService
                        .getColumn(self.currentReportId, self.sqlContent)
                        .subscribe(res => {
                            // console.log(res);
                            if (res.isSucceeded == true) {
                                // if (self.isEdit == false) {
                                    self.columnItems = res.data;
                                    // console.log(res.data);
                                //}
                                //self.isEdit = false;
                            } else {
                                //noticeFail('Lỗi');
                            }
                        });
                },
                err => {
                    console.log(err);
                    this.message.error("Đã có lỗi xảy ra");
                }
            );

        // self.columnItems.forEach(element => {
        //     delete element.id;
        // });
        // this.configReportService.deleteListReportDisplayByReportId(self.currentReportId).subscribe((res: any) => {
        //     this.configReportService.createListReportDisplay(self.columnItems).subscribe((res: any) => {
        //         if (res.isSucceeded == true) {
        //             this.configReportService.updateSQL(self.currentReportId, self.sqlContent).subscribe((res: any) => {
        //                 if (res.isSucceeded == true) {
        //                     noticeSuccess('Thành công');
        //                 }
        //             })
        //         }
        //         else {
        //             noticeFail('Lỗi');
        //         }
        //     })
        // });
    }

    findIndex(str: string, arr: any, returnValues) {
        var i = 0;
        arr.forEach(element => {
            if (element.Varible == str) return (returnValues = i);
            else i++;
        });
    }

    testSQL() {
        const self = this;

        this.dataSourceTest3 = {
            load: function(loadOptions: any) {
                const promise = new Promise((resolve, reject) => {
                    if (!isNullOrUndefined(self.formData)) {
                        var listkey = Object.keys(self.formData);
                        var i = 0;
                        listkey.forEach(element => {
                            var obj = {};
                            var index;
                            index = self.paramTest.findIndex(
                                x => x.Varible == element
                            );
                            self.paramTest[index].Value = self.formData[
                                element
                            ].toString();
                            i++;
                        });
                    }
                    // self.configReportService.viewReport(self.currentReportId, self.sqlContent, self.paramTest).subscribe((res: any) => {
                    //     if (res.isSucceeded == true) {
                    //         resolve(res.data);
                    //         noticeSuccess('Thành công!');
                    //         var key = Object.keys(res.data);
                    //         self.tbTestVisible3 = true;
                    //     }
                    //     else {
                    //         noticeFail('Lỗi');
                    //         resolve(res.data);
                    //     }
                    // });
                });
                return promise;
            }
        };
    }
    // updateSQL() {
    //     const self = this;
    //     this.configReportService.updateSQL(self.currentReportId, self.sqlContent).subscribe((res: any) => {
    //         if (res.isSucceeded == true) {
    //             noticeSuccess('Thành công');
    //         } else {
    //             noticeFail('Lỗi');
    //         }
    //     })
    // }

    onCreateColumnRowUpdated(e) {
        console.log(e);
    }

    onCreateColumnRowUpdating(e) {
        console.log(e);
    }

    onContentReady_UnitGroup(e) {}
    onRowPrepared_UnitGroup(e) {}

    isNullOrUndefined(obj: any) {
        return typeof obj === "undefined" || obj === null;
    }

    //Bảo cmt hàm convert fieldname, caption của dx-col, chả biết cái gì mà nhiều dòng quá, đọc hổng hiểu
    // convertFieldName(name) {
    //     const numParam = (name.match(new RegExp("{{", "g")) || []).length;

    //     if (numParam == 1) {
    //         for (let i = 0; i < this.listFilter.length; i++) {
    //             if (
    //                 name
    //                     .toLowerCase()
    //                     .indexOf(
    //                         "{" + this.listFilter[i].code.toLowerCase() + "}"
    //                     ) != -1
    //             ) {
    //                 let _temp1 = "";
    //                 let _temp2 = "";
    //                 let _temp3 = "";
    //                 if (name.indexOf("{{") > 0 && name.indexOf("}}") != -1) {
    //                     if (name.indexOf("}}") == name.length - 2) {
    //                         _temp1 = name.substring(0, name.indexOf("{{"));
    //                     } else {
    //                         _temp1 = name.substring(0, name.indexOf("{{"));
    //                         _temp3 = name.substring(name.indexOf("}}") + 2);
    //                     }
    //                 } else if (
    //                     name.indexOf("{{") > 0 &&
    //                     name.indexOf("}}") == -1
    //                 ) {
    //                     if (
    //                         name.substring(name.length - 1).indexOf("}") == -1
    //                     ) {
    //                         _temp1 = name.substring(0, name.indexOf("{{"));
    //                         _temp2 = name.split("}")[1];
    //                     } else {
    //                         _temp1 = name.substring(0, name.indexOf("{{"));
    //                         _temp2 = name.split("}")[1];
    //                         _temp3 = name.split("}")[2];
    //                     }
    //                 } else if (
    //                     name.indexOf("{{") == 0 &&
    //                     name.indexOf("}}") != -1
    //                 ) {
    //                     if (name.indexOf("}}") != name.length - 2) {
    //                         _temp3 = name.split("}}")[1];
    //                     }
    //                 } else {
    //                     if (
    //                         name.substring(name.length - 1).indexOf("}}") != -1
    //                     ) {
    //                         _temp2 = name.split("}")[1];
    //                     } else {
    //                         _temp2 = name.split("}")[1];
    //                         _temp3 = name.split("}")[2];
    //                     }
    //                 }
    //                 if (_temp2 != "") {
    //                     _temp2 = (
    //                         parseInt(this.listFilter[i].value, 10) +
    //                         parseInt(_temp2, 10)
    //                     ).toString();
    //                 } else {
    //                     _temp2 = parseInt(
    //                         this.listFilter[i].value,
    //                         10
    //                     ).toString();
    //                 }
    //                 name = _temp1 + _temp2 + _temp3;
    //             }
    //         }
    //     } else if (numParam == 2) {
    //         const strName = name;
    //         let _temp1 = "";
    //         let _tempValue1 = "";
    //         let _temp2 = "";
    //         let _tempValue2 = "";
    //         let _temp3 = "";
    //         if (strName.indexOf("{{") > 0) {
    //             _temp1 = strName.substring(0, strName.indexOf("{{"));
    //         }
    //         let indextempValue = strName.indexOf("}", strName.indexOf("}") + 1);
    //         _tempValue1 = strName.substring(_temp1.length, indextempValue + 1);
    //         _temp2 = strName.substring(
    //             _temp1.length + _tempValue1.length,
    //             strName.indexOf("{{", _temp1.length + _tempValue1.length)
    //         );
    //         indextempValue = strName.indexOf(
    //             "}",
    //             strName.indexOf(
    //                 "}",
    //                 _temp1.length + _tempValue1.length + _temp2.length
    //             ) + 1
    //         );
    //         _tempValue2 = strName.substring(
    //             _temp1.length + _tempValue1.length + _temp2.length,
    //             indextempValue + 1
    //         );
    //         _temp3 = strName.substring(
    //             _temp1.length +
    //                 _tempValue1.length +
    //                 _temp2.length +
    //                 _tempValue2.length
    //         );
    //         let reportValue1 = "";
    //         let reportValue2 = "";
    //         for (let i = 0; i < this.listFilter.length; i++) {
    //             if (
    //                 _tempValue1
    //                     .toLowerCase()
    //                     .indexOf(
    //                         "{" + this.listFilter[i].code.toLowerCase() + "}"
    //                     ) != -1
    //             ) {
    //                 reportValue1 = this.listFilter[i].value;
    //             }
    //         }
    //         for (let i = 0; i < this.listFilter.length; i++) {
    //             if (
    //                 _tempValue2
    //                     .toLowerCase()
    //                     .indexOf(
    //                         "{" + this.listFilter[i].code.toLowerCase() + "}"
    //                     ) != -1
    //             ) {
    //                 reportValue2 = this.listFilter[i].value;
    //             }
    //         }
    //         if (_tempValue1.indexOf("}}") > 0) {
    //             _tempValue1 = reportValue1;
    //         } else {
    //             const strTemp = _tempValue1.substring(
    //                 _tempValue1.indexOf("}") + 1,
    //                 _tempValue1.length - 1
    //             );
    //             if (strTemp != "") {
    //                 _tempValue1 = (
    //                     parseInt(reportValue1, 10) + parseInt(strTemp, 10)
    //                 ).toString();
    //             } else {
    //                 _tempValue1 = parseInt(reportValue1, 10).toString();
    //             }
    //         }
    //         if (_tempValue2.indexOf("}}") > 0) {
    //             _tempValue2 = reportValue2;
    //         } else {
    //             const strTemp = _tempValue2.substring(
    //                 _tempValue2.indexOf("}") + 1,
    //                 _tempValue2.length - 1
    //             );
    //             if (strTemp != "") {
    //                 _tempValue2 = (
    //                     parseInt(reportValue2, 10) + parseInt(strTemp, 10)
    //                 ).toString();
    //             } else {
    //                 _tempValue2 = parseInt(reportValue2, 10).toString();
    //             }
    //         }
    //         name = _temp1 + _tempValue1 + _temp2 + _tempValue2 + _temp3;
    //     } else if (numParam == 3) {
    //         const strName = name;
    //         let _temp1 = "";
    //         let _tempValue1 = "";
    //         let _temp2 = "";
    //         let _tempValue2 = "";
    //         let _temp3 = "";
    //         let _tempValue3 = "";
    //         let _temp4 = "";
    //         if (strName.indexOf("{{") > 0) {
    //             _temp1 = strName.substring(0, strName.indexOf("{{"));
    //         }
    //         let indextempValue = strName.indexOf("}", strName.indexOf("}") + 1);
    //         _tempValue1 = strName.substring(_temp1.length, indextempValue + 1);
    //         _temp2 = strName.substring(
    //             _temp1.length + _tempValue1.length,
    //             strName.indexOf("{{", _temp1.length + _tempValue1.length)
    //         );
    //         indextempValue = strName.indexOf(
    //             "}",
    //             strName.indexOf(
    //                 "}",
    //                 _temp1.length + _tempValue1.length + _temp2.length
    //             ) + 1
    //         );
    //         _tempValue2 = strName.substring(
    //             _temp1.length + _tempValue1.length + _temp2.length,
    //             indextempValue + 1
    //         );
    //         _temp3 = strName.substring(
    //             _temp1.length +
    //                 _tempValue1.length +
    //                 _temp2.length +
    //                 _tempValue2.length,
    //             strName.indexOf(
    //                 "{{",
    //                 _temp1.length +
    //                     _tempValue1.length +
    //                     _temp2.length +
    //                     _tempValue2.length
    //             )
    //         );
    //         let count = 0;
    //         let indexTemp = 0;
    //         for (let i = 0; i < strName.length; i++) {
    //             if (strName.charAt(i) == "}") {
    //                 count++;
    //                 indexTemp = i;
    //             }
    //             if (count == 6) {
    //                 break;
    //             }
    //         }
    //         _tempValue3 = strName.substring(
    //             _temp1.length +
    //                 _temp2.length +
    //                 _temp3.length +
    //                 _tempValue1.length +
    //                 _tempValue2.length,
    //             indexTemp + 1
    //         );
    //         _temp4 = strName.substring(
    //             _temp1.length +
    //                 _temp2.length +
    //                 _temp3.length +
    //                 _tempValue1.length +
    //                 _tempValue2.length +
    //                 _tempValue3.length
    //         );
    //         let reportValue1 = "";
    //         let reportValue2 = "";
    //         let reportValue3 = "";
    //         for (let i = 0; i < this.listFilter.length; i++) {
    //             if (
    //                 _tempValue1
    //                     .toLowerCase()
    //                     .indexOf(
    //                         "{" + this.listFilter[i].code.toLowerCase() + "}"
    //                     ) != -1
    //             ) {
    //                 reportValue1 = this.listFilter[i].value;
    //             }
    //         }
    //         for (let i = 0; i < this.listFilter.length; i++) {
    //             if (
    //                 _tempValue2
    //                     .toLowerCase()
    //                     .indexOf(
    //                         "{" + this.listFilter[i].code.toLowerCase() + "}"
    //                     ) != -1
    //             ) {
    //                 reportValue2 = this.listFilter[i].value;
    //             }
    //         }
    //         for (let i = 0; i < this.listFilter.length; i++) {
    //             if (
    //                 _tempValue3
    //                     .toLowerCase()
    //                     .indexOf(
    //                         "{" + this.listControl[i].code.toLowerCase() + "}"
    //                     ) != -1
    //             ) {
    //                 reportValue3 = this.listFilter[i].value;
    //             }
    //         }
    //         if (_tempValue1.indexOf("}}") > 0) {
    //             _tempValue1 = reportValue1;
    //         } else {
    //             const strTemp = _tempValue1.substring(
    //                 _tempValue1.indexOf("}") + 1,
    //                 _tempValue1.length - 1
    //             );
    //             if (strTemp != "") {
    //                 _tempValue1 = (
    //                     parseInt(reportValue1, 10) + parseInt(strTemp, 10)
    //                 ).toString();
    //             } else {
    //                 _tempValue1 = parseInt(reportValue1, 10).toString();
    //             }
    //         }

    //         if (_tempValue3.indexOf("}}") > 0) {
    //             _tempValue3 = reportValue3;
    //         } else {
    //             const strTemp = _tempValue3.substring(
    //                 _tempValue3.indexOf("}") + 1,
    //                 _tempValue3.length - 1
    //             );
    //             if (strTemp != "") {
    //                 _tempValue3 = (
    //                     parseInt(reportValue3, 10) + parseInt(strTemp, 10)
    //                 ).toString();
    //             } else {
    //                 _tempValue3 = parseInt(reportValue3, 10).toString();
    //             }
    //         }

    //         if (_tempValue2.indexOf("}}") > 0) {
    //             _tempValue2 = reportValue2;
    //         } else {
    //             const strTemp = _tempValue2.substring(
    //                 _tempValue2.indexOf("}") + 1,
    //                 _tempValue2.length - 1
    //             );
    //             if (strTemp != "") {
    //                 _tempValue2 = (
    //                     parseInt(reportValue2, 10) + parseInt(strTemp, 10)
    //                 ).toString();
    //             } else {
    //                 _tempValue2 = parseInt(reportValue2, 10).toString();
    //             }
    //         }
    //         name =
    //             _temp1 +
    //             _tempValue1 +
    //             _temp2 +
    //             _tempValue2 +
    //             _temp3 +
    //             _tempValue3 +
    //             _temp4;
    //     }

    //     return name;
    // }
}
