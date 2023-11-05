import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/Models/Product';
import { Pagination } from '../shared/Models/Pagination';
import { Brand } from '../shared/Models/brand';
import { Type } from '../shared/Models/Type';
import { ShopParams } from '../shared/Models/shopParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getProducts(ShopParams: ShopParams){
    let params = new HttpParams();

    if(ShopParams.brandId>0) params = params.append('brandId',ShopParams.brandId);
    if(ShopParams.typeId) params = params.append('typeId', ShopParams.typeId);
     params = params.append('sort', ShopParams.sort);
     params = params.append('pageIndex', ShopParams.pageNumber);
     params = params.append('pageSize', ShopParams.pageSize);
     if(ShopParams.search) params = params.append('search', ShopParams.search);
     
    
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {params});
  }
  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }
  getTypes(){
    return this.http.get<Type[]>(this.baseUrl + 'products/types');
  }
}
