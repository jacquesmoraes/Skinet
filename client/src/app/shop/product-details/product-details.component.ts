import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/Models/Product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
product?: Product;
 
constructor(private shopService: ShopService, private activadedtRoute: ActivatedRoute){}
 
  ngOnInit(): void {
    this.loadProduct();
  }
loadProduct(){
  const id = this.activadedtRoute.snapshot.paramMap.get('id');
  if(id) this.shopService.getProduct(+id).subscribe({
    next: product => this.product = product,
    error: error => console.log(error)
    
  })
}
}