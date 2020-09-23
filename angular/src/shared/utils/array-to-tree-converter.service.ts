import { Injectable } from '@angular/core';
import * as _ from 'lodash';

@Injectable()
export class ArrayToTreeConverterService {

    createMenu(array: any[], parentProperty, idProperty, parentValue, childrenProperty: string): any {
        let tree = [];

        let nodes = _.filter(array, [parentProperty, parentValue]);

        // dòng này nodes đã k có dữ liệu rồi
        _.forEach(nodes, node => {
            // let newNode = {
            //     data: node
            // };
            let newNode = node;
            //chỗ này nó đưa appmenuitem vào field data nè, data đang là object, h có cách nào lấy hết đống thuộc tính ra ngang cấp là đc

            newNode[childrenProperty] = this.createMenu(
                array,
                parentProperty,
                idProperty,
                node[idProperty],
                childrenProperty
            );

            tree.push(newNode);
        });

        return tree;
    }

    createTree(array: any[], parentIdProperty, idProperty, parentIdValue, childrenProperty: string, fieldMappings): any {
        let tree = [];

        let nodes = _.filter(array, [parentIdProperty, parentIdValue]);

        _.forEach(nodes, node => {
            let newNode = {
                data: node
            };

            this.mapFields(node, newNode, fieldMappings);

            newNode[childrenProperty] = this.createTree(
                array,
                parentIdProperty,
                idProperty,
                node[idProperty],
                childrenProperty,
                fieldMappings
            );

            tree.push(newNode);
        });

        return tree;
    }

    mapFields(node, newNode, fieldMappings): void {
        _.forEach(fieldMappings, fieldMapping => {
            if (!fieldMapping['target']) {
                return;
            }

            if (fieldMapping.hasOwnProperty('value')) {
                newNode[fieldMapping['target']] = fieldMapping['value'];
            } else if (fieldMapping['source']) {
                newNode[fieldMapping['target']] = node[fieldMapping['source']];
            } else if (fieldMapping['targetFunction']) {
                newNode[fieldMapping['target']] = fieldMapping['targetFunction'](node);
            }
        });
    }
}
