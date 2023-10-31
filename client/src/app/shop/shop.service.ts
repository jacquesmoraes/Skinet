import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Product } from '../shared/Models/Product';
import { Pagination } from '../shared/Models/Pagination';
import { Brand } from '../shared/Models/brand';
import { Type } from '../shared/Models/Type';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:5001/api/'

  constructor(private http: HttpClient) { }

  getProducts(brandId?: number, typeId?: number){
    let params = new HttpParams();

    if(brandId) params = params.append('brandId', brandId);
    if(typeId) params = params.append('typeId', typeId);
    
    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', {params});
  }
  getBrands(){
    return this.http.get<Brand[]>(this.baseUrl + 'products/brands');
  }
  getTypes(){
    return this.http.get<Type[]>(this.baseUrl + 'products/types');
  }
}
