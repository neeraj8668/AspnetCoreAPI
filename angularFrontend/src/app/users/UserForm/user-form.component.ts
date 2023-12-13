import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, FormArray } from '@angular/forms';
import { Subscription, of } from 'rxjs';
import { UserService, AlertService } from '@app/_services';
import { Product, User } from '@app/_models';
import { ProductService } from '@app/_services/product.service';
import { switchMap } from 'rxjs/operators';

@Component({

  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
})

export class UserFormComponent implements OnInit, OnDestroy {
  userForm: FormGroup;
  isEdit = false;
  isSubmitted: boolean = false;
  userId: string = '';
  allproducts: any = [];
  private subscription: Subscription = new Subscription();
  constructor(
    private formBuilder: FormBuilder, private productService: ProductService,
    private userService: UserService,
    private alertService: AlertService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.userForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      productIds: this.formBuilder.array([]),
    });
  }
  ngOnInit(): void {
    this.productService.getAll().subscribe(
      (products) => {
        this.allproducts = products;
        console.log('Products:', this.allproducts);
  
        this.route.params.pipe(
          switchMap((params) => {
            this.userId = params['id'] || '';
            this.isEdit = !!this.userId;
            return this.isEdit ? this.userService.getUserById(this.userId) : of(null);
          })
        ).subscribe(
          (user) => {
            console.log('User:', user);
  
            if (this.isEdit) {
              this.populateUserForm(user);
            } else {
              this.allproducts.forEach((product: any) => {
                this.addProductControl(product, false);
              });
            }
          },
          (error) => {
            this.alertService.error('Error fetching user:', error);
            console.error('Error fetching user:', error);
          }
        );
      },
      (error) => {
        this.alertService.error('Error fetching products:', error);
        console.error('Error fetching products:', error);
      }
    );
  }
// Function to populate user form with details
private populateUserForm(user: User | null): void {
  if (user) {
    this.userForm.patchValue({
      name: user.Name,
      email: user.Email,
    });

    const productIdsFormArray = this.userForm.get('productIds') as FormArray;
    productIdsFormArray.clear();

    this.allproducts.forEach((product: any) => {
      const isSelected = user.ProductIds?.includes(product.Id) || false;
      this.addProductControl(product, isSelected);
    });
  }
}

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  checkProductIsChecked(id: string): boolean {
    const productIdsFormArray = this.userForm.get('productIds') as FormArray;
    return productIdsFormArray.value.indexOf(id) > -1;
  }

  onProductClick(product: Product, event: any) {
    const productIdsFormArray = this.userForm.get('productIds') as FormArray;
    const index = productIdsFormArray.value.indexOf(product.Id);

    if (event.target.checked && index === -1) {
      // If checkbox is checked and the product ID is not in the list, add it
      productIdsFormArray.push(this.formBuilder.control(product.Id));
    } else if (!event.target.checked && index > -1) {
      // If checkbox is unchecked and the product ID is in the list, remove it
      productIdsFormArray.removeAt(index);
    }
    console.log(productIdsFormArray.value);
  }

  get name() {
    return this.userForm.get('name')!;
  }

  get email() {
    return this.userForm.get('email')!;
  }
  

  addProductControl(product: Product, isSelected: boolean): void {
    if (isSelected) {
      const control = this.formBuilder.control(product.Id); //checkbox 
      (this.userForm.get('productIds') as FormArray).push(control);
    }
  }
    

  get isEmailInvalid() {
    const emailControl = this.userForm.get('email');
    return emailControl?.invalid && (emailControl?.dirty || emailControl?.touched);
  }
  // Handle form submission
  onSubmit() {
    this.alertService.clear();
    if (this.userForm.invalid) {
      for (const control of Object.keys(this.userForm.controls)) {
        this.userForm.controls[control].markAsTouched();
      }
      return;
    }

    const user: User = { ...this.userForm.value, id: this.userId };

    if (this.isEdit) {
      this.updateUser(user);
    } else {
      this.createUser(user);
    }
  }
  // Function to update user
  private updateUser(user: User): void {
    this.subscription.add(
      this.userService.updateUser(this.userId || '', user).subscribe(
        () => {
          this.router.navigate(['/users']);
          this.alertService.success('User saved', { keepAfterRouteChange: true });
        },
        (error) => {
          this.alertService.error(error);
          console.error('Error updating user:', error);
        }
      )
    );
  }

  // Function to create a new user
  private createUser(user: User): void {
    this.subscription.add(
      this.userService.createUser(user).subscribe(
        () => {
          this.router.navigate(['/users']);
        },
        (error) => console.error('Error creating user:', error)
      )
    );
  }

}