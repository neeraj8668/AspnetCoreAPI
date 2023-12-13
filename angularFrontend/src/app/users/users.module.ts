import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { LayoutComponent } from './layout.component';
import { ListComponent } from './Listing/list.component';
import { UserFormComponent } from './UserForm/user-form.component';
import { UserDetailsComponent } from './UserDetails/user-detail.component';
import { FormsModule } from '@angular/forms';
 
@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        UsersRoutingModule,
        FormsModule
    ],
    declarations: [
        LayoutComponent,
        ListComponent,
        UserFormComponent,
        UserDetailsComponent,
    ]
})
export class UsersModule { }