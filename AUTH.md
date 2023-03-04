# Auth

- on account creation a new token will be generate to the new account.

- This token will be used to authenticate next requests to the server.

- This token will have an expiration date and will have a relationship with the user.

- Everytime a user try to make an action, the token sent will be validated.

- To get another token the user will be able to make a login, using the username and password credentials.

- every request should sent the token.

  - a new token will be generated
