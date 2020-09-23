import { MenuDto } from "@shared/service-proxies/service-proxies";

export class AppMenuItem {
    id = 0;
    name = '';
    permissionName = '';
    icon = '';
    route = '';
    items: AppMenuItem[];
    external: boolean;
    requiresAuthentication: boolean;
    featureDependency: any;
    parameters: {};
    numNoti: number;
    rawSql: string;
    parent: number;
    index: number;
    constructor(
        name: string,
        permissionName: string,
        icon: string,
        route: string,
        items?: AppMenuItem[],
        id?: number,
        parent?: number,
        index?: number,
        numNoti?: number,
        rawSql?: string,
        external?: boolean,
        parameters?: Object,
        featureDependency?: any,
        requiresAuthentication?: boolean
    ) {
        this.name = name;
        this.permissionName = permissionName;
        this.icon = icon;
        this.route = route;
        this.id = id;
        this.parent = parent;
        this.index = index;
        this.numNoti = numNoti;
        this.rawSql = rawSql;
        this.external = external;
        this.parameters = parameters;
        this.featureDependency = featureDependency;

        if (items === undefined) {
            this.items = [];
        } else {
            this.items = items;
        }

        if (this.permissionName) {
            this.requiresAuthentication = true;
        } else {
            this.requiresAuthentication = requiresAuthentication ? requiresAuthentication : false;
        }
    }

    hasFeatureDependency(): boolean {
        return this.featureDependency !== undefined;
    }

    featureDependencySatisfied(): boolean {
        if (this.featureDependency) {
            return this.featureDependency();
        }

        return false;
    }
}
