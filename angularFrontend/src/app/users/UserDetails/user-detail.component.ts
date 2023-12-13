import { Component, OnInit, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product, User } from '@app/_models';
import { UserService } from '@app/_services';
import { ProductService } from '@app/_services/product.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-user-details',
  templateUrl: './user-detail.component.html',
  styleUrls: ['./user-detail.component.css'],
})
export class UserDetailsComponent implements OnInit, OnDestroy {
  user: User | undefined;
  products: Product[] | undefined;
  
  private subscription: Subscription = new Subscription();

  constructor(private route: ActivatedRoute, private router: Router, 
    private productService: ProductService,
    private userService: UserService) {
      
    }

  ngOnInit(): void {
    this.loadUser();
    this.loadProducts();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
  
  getProductById(productIdToFind: string): string | undefined {
    const foundProduct = this.products?.find(product => product.Id === productIdToFind);
    if (foundProduct) {
      return foundProduct.Name;
    } else {
      return undefined;
    }
  }
 
  private loadProducts() {
 
      this.subscription.add(
        this.productService.getAll().subscribe(
          (products) => {
           
            this.products = products;
            console.log(this.products );
          },
          (error) => console.error('Error fetching products:', error)
        ));
  }

  private loadUser() {
    const userId = this.route.snapshot.paramMap.get('id') || '';
    if(userId!='')
      this.subscription.add(
        this.userService.getUserById(userId).subscribe(
          (user) => (this.user = user),
          (error) => console.error('Error fetching user:', error)
        )
      );
  }
}