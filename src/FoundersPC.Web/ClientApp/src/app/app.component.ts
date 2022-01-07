import { Component, OnInit } from "@angular/core";
import { DataService } from "./hardware/services/case-service";
import { Case } from "./hardware/models/case";
 
@Component({
    selector: "app",
    templateUrl: "./app.component.html",
    providers: [DataService]
})
export class AppComponent implements OnInit {

    public product: Case = new Case();   // изменяемый товар
    public products: Case[];             // массив товаров
    public tableMode: boolean = true;    // табличный режим
 
    constructor(private readonly dataService: DataService) { }

    public ngOnInit() {
        this.loadProducts();    // загрузка данных при старте компонента  
    }

    // получаем данные через сервис
    public loadProducts() {
        this.dataService.getCases()
            .subscribe((data: Case[]) => this.products = data ?? new Array<Case>());
    }

    // сохранение данных
    public save() {
        if (this.product.id == null) {
            this.dataService.createProduct(this.product)
                .subscribe((data: Case) => this.products.push(data));
        } else {
            this.dataService.updateProduct(this.product)
                .subscribe(data => this.loadProducts());
        }
        this.cancel();
    }

    public editProduct(p: Case) {
        this.product = p;
    }

    public cancel() {
        this.product = new Case();
        this.tableMode = true;
    }

    public delete(p: Case) {
        this.dataService.deleteProduct(p.id)
            .subscribe(data => this.loadProducts());
    }

    public add() {
        this.cancel();
        this.tableMode = false;
    }
}