import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { LayoutComponent } from './layout.component';
import { ListComponent } from './Listing/list.component';
import { UserDetailsComponent } from './UserDetails/user-detail.component';
import { UserFormComponent } from './UserForm/user-form.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: '', component: ListComponent },
      { path: 'add', component: UserFormComponent },
      { path: 'edit/:id', component: UserFormComponent },      
      { path: 'view/:id', component: UserDetailsComponent },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UsersRoutingModule { }
