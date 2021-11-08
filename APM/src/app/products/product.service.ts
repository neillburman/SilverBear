import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";
import { Observable } from "rxjs";
import { catchError, filter, tap } from 'rxjs/operators'
import { IProduct } from "./product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private productUrl = 'api/products/products.json/'
  constructor(private http: HttpClient) { }
  getProducts(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(this.productUrl)
      .pipe(
          tap(data => console.log('All', JSON.stringify(data))),
          catchError(this.handleError)
       );
  }

  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occured: ${err.error.message}`;
    } else {

      errorMessage = `Server returned code: ${err.status}, error message is : ${err.message}`;
    }

    console.error(errorMessage);

    return throwError(errorMessage);

  }
  


  //getProducts(): IProduct[] {
  //  return [
  //    {
  //      "productId": 2,
  //      "productName": "Garden Cart",
  //      "productCode": "GDN-0023",
  //      "releaseDate": "March 18, 2021",
  //      "description": "15 Gallon Capacity garden cart",
  //      "price": 32.99,
  //      "starRating": 4.2,
  //      "imageUrl": "assets/images/garden_cart.png"
  //    },
  //    {
  //      "productId": 5,
  //      "productName": "Hammer",
  //      "productCode": "TBX-0048",
  //      "releaseDate": "May 21, 2021",
  //      "description": "Curved claw steel hammer",
  //      "price": 8.9,
  //      "starRating": 4.8,
  //      "imageUrl": "assets/images/hammer.png"
  //    },
  //    {
  //      "productId": 6,
  //      "productName": "Saw",
  //      "productCode": "SAW-0048",
  //      "releaseDate": "Jan 21, 2021",
  //      "description": "Deadly Saw",
  //      "price": 5.9,
  //      "starRating": 2.8,
  //      "imageUrl": "assets/images/saw.png"
  //    }
  //  ]
  //}
}
