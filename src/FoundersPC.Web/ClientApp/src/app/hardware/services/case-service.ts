import { Injectable } from "@angular/core"
import { HttpClient} from "@angular/common/http";
import { Case } from "src/app/hardware/models/case";

@Injectable()
export class DataService {
    private url: string = "/api/Hardware/Case";
 
    constructor(private readonly http: HttpClient) { }

    public getCases() {
        return this.http.post(this.url + "/" + "GetAll", { pageSize: 10, pageNumber: 0 });
    }

    public getCase(id: number) {
        return this.http.get(this.url + "/" + id);
    }

    public createProduct(product: Case) {
        return this.http.post(this.url, product);
    }

    public updateProduct(product: Case) {
        return this.http.put(this.url, product);
    }

    public deleteProduct(id: number) {
        return this.http.delete(this.url + "/" + id);
    }
}