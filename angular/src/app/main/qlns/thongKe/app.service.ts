import {
    Injectable
} from '@angular/core';

export class PopulationByRegion {
    region: string;
    val: number;
}

let populationByRegions: PopulationByRegion[] = [{
    region: "Hợp động cộng tác viên",
    val: 4119626293
}, {
    region: "Hợp đồng khoán việc",
    val: 1012956064
}, {
    region: "Hợp động thử việc",
    val: 344124520
}, {
    region: "Hợp đồng lao động",
    val: 590946440
}, {
    region: "Hợp đồng thực tập ",
    val: 727082222
}];

@Injectable()
export class Service {
    getPopulationByRegions(): PopulationByRegion[] {
        return populationByRegions;
    }
}