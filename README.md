# Task 1 & 2
Sample api with Mongo db
Its is .net Core API with Mongo DB database 

#Task 1
#API Setup:
API on startup creates default collections for User and products with a record in it.
Seed only works if there is no data in user and product collections.
Collection name and mongo connection is configured in appsetings
API usess serilog to print logs.
API uses fluent validations for validating user input
Swagger is implemented to show documentation of api endpoints
CORS settings is also configured in appsettings.

#API TODO: 
Authentication is to be implemented
GDPR headers in response.

#Front End : 
Its Angular 14 app. with CRUD forms in User module

GDPR consent is on home page once user gives a consent the consent does not show
Once Cookies (cookieConsent) is cleared Consent screen pops up again.
Usermodule has the following components
Listing : users/Listing/list.component
Create/Update: users/UserForm/user.form.component.ts
view : users/userDetails/userDetail.component.ts

custom GDPR headers are inserted from client side
one can add /edit /update/view and delete user. 

User has products collection and product ids is stored in user object

Error intercepter is used to capture api interaction errors 
Alert service to show notifications

It has required and email validations and is using .net core rest api endpoints for CRUD operations

TODO:
Login and authguard/interceptor
#Task 2
 Console app which is checking a binary string as per the criteria.
 The function accepts a binary string as input.
  Check if the binary string is 'good' based on these conditions:
● Equal number of 0's and 1's.
● For every prefix, the number of 1's is not less than the number of 0's.





