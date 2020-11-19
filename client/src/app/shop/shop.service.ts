import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IBrand } from '../shared/models/brand';
import { IProduct } from '../shared/models/product';
import { IType } from '../shared/models/productType';
import { map } from 'rxjs/operators';
import { ShopParams } from '../shared/models/shopParams';
import { IPagination } from '../shared/models/pagination';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/';
  constructor(private http: HttpClient) { }

  getProducts(shopParams: ShopParams): Observable<IPagination>
  {
    let params = new HttpParams();
    if (shopParams.typeId !== 0)
    {
      params = params.append('typeId', shopParams.typeId.toString());
    }
    if (shopParams.brandId !== 0)
    {
      params = params.append('brandId', shopParams.brandId.toString());
    }
    if (shopParams.search)
    {
      params = params.append('search', shopParams.search);
    }
    params = params.append('sort', shopParams.sort);
    params = params.append('pageIndex', shopParams.pageNumber.toString());
    params = params.append('pageIndex', shopParams.pageSize.toString());
    // IProduct[] changed to IPagination
    return this.http.get<IPagination>(this.baseUrl + 'product', {observe: 'response', params})
    .pipe( map(Response => Response.body));
  }
  getBrands(): Observable<IBrand[]>
  {
    return this.http.get<IBrand[]>(this.baseUrl + 'product/brands');
  }
  getTypes(): Observable<IType[]>
  {
    return this.http.get<IType[]>(this.baseUrl + 'product/types');
  }
}