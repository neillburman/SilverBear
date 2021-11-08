import { OnDestroy } from "@angular/core";
import { Component, OnInit } from "@angular/core";
import { Subscription } from "rxjs";
import { IProduct } from "./product";
import { ProductService } from "./product.service";

@Component({
/*  selector: 'pm-products',*/ /*removed as ProductList compenent will no longer be used as a directive*/
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})

export class ProductListComponent  implements OnInit, OnDestroy{
  pageTitle: string = 'Product List';
  imageWidth: number = 50;
  imageMargin: number = 2;
  showImage: boolean = false;
  errorMessage: string = '';
  sub! : Subscription

  private _listFilter: string = '';
  get listFilter(): string {
    return this._listFilter;
  }
  set listFilter(value: string) {
    this._listFilter = value;
    console.log('In setter', value);

    this.filteredProducts = this.performFilter(value);
  }

  filteredProducts: IProduct[] = [];

  products: IProduct[] = [];


  constructor(private productService: ProductService ) {


  }


  performFilter(filterBy: string): IProduct[] {
    filterBy = filterBy.toLocaleLowerCase();
    return this.products.filter((product: IProduct) => product.productName.toLocaleLowerCase().includes(filterBy));

  }

  toggleImage(): void {
    this.showImage = !this.showImage;
  }

  ngOnInit(): void {
   /* console.log('In onInit');*/
   /* this._listFilter = 'cart';*/
    /*this.products = this.productService.getProducts();*/ /*used for hard coded data, */
    /* Calls the productService that returns an observalble, whose notifications enable properties to be populated*/
    /* As we are now binding to the filteredProducts property this has to be included in the notification */
  /*  The subscription is assigned to a varable (sub) so it can be destroyed*/
    this.sub = this.productService.getProducts().subscribe({
      next: products => {
        this.products = products;
        this.filteredProducts = this.products;
      },
      error: err => this.errorMessage = err
    });

    /*this.filteredProducts = this.products;*/
  }
  onRatingClicked(message: string):void {
    this.pageTitle = 'Product List: ' + message;
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

}
