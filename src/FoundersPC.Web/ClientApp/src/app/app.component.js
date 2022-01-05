var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { Component } from '@angular/core';
import { DataService } from "./hardware/services/case-service";
import { Case } from "./hardware/models/case";
let AppComponent = class AppComponent {
    constructor(dataService) {
        this.dataService = dataService;
        this.product = new Case(); // изменяемый товар
        this.tableMode = true; // табличный режим
    }
    ngOnInit() {
        this.loadProducts(); // загрузка данных при старте компонента  
    }
    // получаем данные через сервис
    loadProducts() {
        this.dataService.getCases()
            .subscribe((data) => this.products = data);
    }
    // сохранение данных
    save() {
        if (this.product.id == null) {
            this.dataService.createProduct(this.product)
                .subscribe((data) => this.products.push(data));
        }
        else {
            this.dataService.updateProduct(this.product)
                .subscribe(data => this.loadProducts());
        }
        this.cancel();
    }
    editProduct(p) {
        this.product = p;
    }
    cancel() {
        this.product = new Case();
        this.tableMode = true;
    }
    delete(p) {
        this.dataService.deleteProduct(p.id)
            .subscribe(data => this.loadProducts());
    }
    add() {
        this.cancel();
        this.tableMode = false;
    }
};
AppComponent = __decorate([
    Component({
        selector: "app",
        templateUrl: "./app.component.html",
        providers: [DataService]
    })
], AppComponent);
export { AppComponent };
//# sourceMappingURL=app.component.js.map