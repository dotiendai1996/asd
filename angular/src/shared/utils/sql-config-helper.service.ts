import { Injectable } from '@angular/core';
import * as _ from 'lodash';
import * as moment from 'moment';

@Injectable()
export class SqlConfigHelperService {
    columnBuilder = [];
    columnConfig = [];

    addColumn(column: any[], ID: number, caption: string, cellTemplate: string, visible = true, fixed = false){
        column.push({ID: ID, caption: caption, visible: visible, cellTemplate: cellTemplate, fixed: fixed, dataField: ''})
        return column;
    }

    addMultipleColumns(oldColumns: any[], newColumns: any[]){
        oldColumns.push(newColumns);
        console.log(oldColumns);
        return oldColumns;
    }

    generateColumns(jsonString, flag = false): any {
        const reportDisplayList = jsonString;
        for (let i = 0; i < reportDisplayList.length; i++) {
            reportDisplayList[i]["isAdd"] = false;
        }

        this.columnConfig = reportDisplayList;
        this.columnBuilder = [];
        const columns = this.getColumnConfig(this.columnBuilder, flag);
        return columns;
    }

    getColumnConfig(columnBuilder, flag = false) {
        const self = this;
        if (columnBuilder.length == 0) {
            this.columnConfig.forEach(column => {
                const t = new Object();
                if (column.parent == true && !column.isAdd) {
                    t["code"] = column.code.toString();
                    t["cellTemplate"] = column.cellTemplate;
                    //chuyển ký tự đầu thành viết thường, để đồng bộ với model của Angular
                    t["dataField"] = column.code.toString();
                    //t["dataField"] = _.camelCase(column.code.toString());
                    // t['dataField'] = column.name.toString().toLowerCase();
                    t["caption"] = column.name.toString();
                    if (column.isDisplay == true) {
                        t["visible"] = true;
                    } else {
                        t["visible"] = false;
                    }
                    if (column.isFreePane == true) {
                        t["fixed"] = true;
                    } else {
                        t["fixed"] = false;
                    }
                    if ((column.width = "")) {
                        t["width"] = "*";
                    } else {
                        t["width"] = column.width;
                    }
                    t["alignment"] = column.textAlign;
                    if (column.groupLevel != null && column.groupLevel != 0) {
                        t["groupIndex"] = column.groupLevel;
                    }
                    // set type
                    t["dataType"] = column.type;

                    // set format cho cot
                    const formatObject = new Object();
                    formatObject["type"] = column.type;
                    formatObject["formatString"] = column["format"];
                    t["format"] = function(param) {
                        return self.formatString(formatObject, param);
                    };
                    t["ID"] = column.id;
                    t["columns"] = self.getColumnConfig(t);
                    column.isAdd = true;
                    columnBuilder.push(t);
                } else if (
                    (column.parent == false || column.parent == null) &&
                    (column.parentCode == "" || column.parentCode == null) &&
                        !column.isAdd
                ) {
                    t["code"] = column.code.toString();
                    t["cellTemplate"] = column.cellTemplate;
                    //chuyển ký tự đầu thành viết thường, để đồng bộ với model của Angular
                    t["dataField"] = column.code.toString();
                    // t["dataField"] = _.camelCase(column.code.toString());
                    // t['dataField'] = column.name.toString().toLowerCase();
                    t["caption"] = column.name.toString();
                    if (column.isDisplay == true) {
                        t["visible"] = true;
                    } else {
                        t["visible"] = false;
                    }
                    if (column.isFreePane == true) {
                        t["fixed"] = true;
                    } else {
                        t["fixed"] = false;
                    }
                    if (column.width == "") {
                        t["width"] = "*";
                    } else {
                        t["width"] = column.width;
                    }
                    t["alignment"] = column.textAlign;
                    if (column.groupLevel != null && column.groupLevel != 0) {
                        t["groupIndex"] = column.groupLevel;
                    }
                    // set type
                    t["dataType"] = column.type;

                    // set format cho cot
                    const formatObject = new Object();
                    formatObject["type"] = column.type;
                    formatObject["formatString"] = column["format"];
                    // t["format"] = function (param) {
                    //  return self.formatString(formatObject, param);
                    // };
                    // t["format"] = {
                    //     style: "currency",
                    //     currency: "EUR",
                    //     useGrouping: true
                    // };
                    t["ID"] = column.id;
                    column.isAdd = true;
                    columnBuilder.push(t);
                }
            });
            if(flag){
                this.addColumn(columnBuilder, columnBuilder.length + 1, 'Hành động', 'CustomTemplate');
                // this.addColumn(columnBuilder, columnBuilder.length + 2, 'File đính kèm', 'CustomTemplate1');
                // this.addColumn(columnBuilder, columnBuilder.length + 3, '#', 'CustomTemplate2');
            }
            return columnBuilder;
        } else {
            const child = [];
            this.columnConfig.forEach(column => {
                const t = new Object();
                if (column.parentCode == columnBuilder.code && !column.isAdd) {
                    //chuyển ký tự đầu thành viết thường, để đồng bộ với model của Angular
                    //t["dataField"] = column.code.toString();
                    t["cellTemplate"] = column.cellTemplate;
                    t["dataField"] = _.camelCase(column.code.toString());
                    // t['dataField'] = column.name.toString().toLowerCase();
                    // t["caption"] = this.convertFieldName(
                    //     column.name.toString()
                    // );
                    t["caption"] = column.name.toString();
                    if (column.isDisplay == true) {
                        t["visible"] = true;
                    } else {
                        t["visible"] = false;
                    }
                    if (column.isFreePane == true) {
                        t["fixed"] = true;
                    } else {
                        t["fixed"] = false;
                    }
                    if ((column.width = "")) {
                        t["width"] = "*";
                    } else {
                        t["width"] = column.width;
                    }
                    t["alignment"] = column.textAlign;
                    if (column.groupLevel != null && column.groupLevel != 0) {
                        t["groupIndex"] = column.groupLevel;
                    }
                    // set type
                    t["dataType"] = column.type;

                    // set format cho cot
                    const formatObject = new Object();
                    formatObject["type"] = column.type;
                    formatObject["formatString"] = column["format"];
                    t["format"] = function(param) {
                        return self.formatString(formatObject, param);
                    };
                    t["ID"] = column.id;
                    if (column.isParent) {
                        t["columns"] = self.getColumnConfig(t);
                    }
                    column.isAdd = true;
                    child.push(t);
                }
            });
            if(flag){
                this.addColumn(child, child.length + 1, 'Action', 'CustomTemplate');
            }
            console.log(child);
            return child;
        }
    }

    formatString(formatObject, param) {
        switch (formatObject["type"].toLowerCase()) {
            case "date": {
                const date = moment(param);
                return date.format(formatObject["formatString"]);
            }

            case "number": {
                // param = $.formatNumber(param, {format: formatObject["formatString"], locale: "en"});
                return param;
            }

            case "string": {
                return param;
            }
        }
    }
}
