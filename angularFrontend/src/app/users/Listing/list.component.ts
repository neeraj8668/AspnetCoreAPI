import { Component, OnInit, OnDestroy } from '@angular/core';
import { first } from 'rxjs/operators';
import { Subscription } from 'rxjs';
import { UserService } from '@app/_services';
import { User } from '@app/_models';

@Component({ templateUrl: 'list.component.html' })

export class ListComponent implements OnInit ,OnDestroy {
  users: User[] = [];
  private subscription: Subscription = new Subscription();

    constructor(private userService: UserService) {}
     
    ngOnInit(): void {
        this.loadUsers();
      }
      ngOnDestroy(): void {
        this.subscription.unsubscribe();
      }
    
     loadUsers() {
        this.subscription.add(
          this.userService.getUsers().subscribe(
            (users) => (this.users = users  ),
            (error) => console.error('Error fetching users:', error)
          )
        );
      }
    deleteUser(id: any) {
        const user = this.users!.find(x => x.Id === id);
        this.subscription.add(
          this.userService.deleteUser(id)
            .pipe(first())
            .subscribe(() => this.users = this.users!.filter(x => x.Id !== id))
          );
    }
}