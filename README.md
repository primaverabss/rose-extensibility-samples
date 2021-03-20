# ROSE

In this repository you will find some code samples to accelerate your integration with ROSE and ROSE AS (Accounting Services).

## What is ROSE?

First of all, PRIMAVERA is a company specialized in the development of management solutions (*ERP - Enterprise Resource Planning*) for small, medium and large companies. ROSE is a cloud solution for medium and large companies and ROSE AS is a cloud solution to accounting offices. Both solutions provides a set of extensibility technologies that enable third-party applications to extend or add new features to provide richer solutions to the end customer.

## Repository Organization

This repository provides access to two demo solutions, one is a REST API Console Application and the secound is a Angular Client Application. Both contains examples of API calls to ROSE.

## Before Start
Before start develop you integration, frist some base principles.

### Authorizing Client Applications

ROSE supports the OAuth 2.0 mechanism of authorization for applications, this means that the client application does not operate with the ROSE credentials, instead the client application must obtains an access token from PRIMAVERA IDENTITY SERVER and uses this token when to requests data. Soo the the frist thing before any request to the application is retrive the authorization token.

At the current version, ROSE supported the followings OAuth 2.0 flows:

* Authorization code
* Implicit flow
* Hybrid flow
* Client credentials

### Register your Application

todo.............

## Contributing and Feedback
Everyone is free to contribute to the repository.

Any bugs detected in the code samples can be reported in the *Issues* section of this repository.

## License

Unless otherwise specified, the code samples are released under the [MIT license](https://pt.wikipedia.org/wiki/Licen%C3%A7a_MIT).
