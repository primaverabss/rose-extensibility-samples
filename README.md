# ROSE

In this repository you will find some code samples to accelerate your integration with ROSE and ROSE AS (Accounting Services).

First of all, PRIMAVERA is a company specialized in the development of management solutions (*ERP - Enterprise Resource Planning*) for small, medium and large companies. ROSE is a cloud solution for medium and large companies and ROSE AS is a cloud solution to accounting offices. Both solutions provide a set of extensibility technologies that enable third-party applications to extend or add new features to provide richer solutions to the end customer.

## Repository Organization

This repository provides access to two demo solutions, one is a C# NET CORE REST API Console Application and the secound is a Angular Client Application. Both contains examples of API calls to ROSE.

## Before Start
Before start develop you integration, frist some base principles.

### Register your Application

Before you start develop your code you must first register as a ROSE developer and use our App Dashboard to provide information about your app and configure your app settings.

* Go to [App Dashboard](https://apps.primaverabss.com/rose/apps).
* Login with your PRIMAVERA IDENTITY account.
* Select the developer menu, and then click on **Creat App**.
* Provide a application KEY. This is your application ID.
* Select your authorization flow.
* Fill other needed information.
* Authorize your app access to a valid subscription.

> For development propose you don't need publish your App. This is only required when you app goes live.

### Authorizing Client Applications

ROSE supports the OAuth 2.0 mechanism of authorization for applications, this means that the client application does not operate with the ROSE credentials, instead the client application must obtains an access token from PRIMAVERA IDENTITY SERVER and uses this token when to requests data. Soo the the frist thing before any request to the application is retrive the authorization token.

At the current version, ROSE support the followings OAuth 2.0 flows:

* Authorization code
* Implicit flow
* Hybrid flow
* Client credentials

Where a sample to get the authorization token using the client credentials grant type.

```csharp
public async Task<string> GetAccessTokenAsync()
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            client.BaseAddress = new Uri(appBaseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Build the request data (grant type client credentials)

            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));
            postData.Add(new KeyValuePair<string, string>("scope", "rose-api"));
            postData.Add(new KeyValuePair<string, string>("client_id", clientId));
            postData.Add(new KeyValuePair<string, string>("client_secret", clientSecret));

            FormUrlEncodedContent content = new FormUrlEncodedContent(postData);

            // Post the request and get the response

            HttpResponseMessage response = await client.PostAsync(IdentitUriKey, content);
            string jsonString = await response.Content.ReadAsStringAsync();
            object responseData = JsonConvert.DeserializeObject(jsonString);

             return ((dynamic)responseData).access_token;
        }
        catch (Exception ex)
        {
            throw new Exception(string.Format("Error getting token. {0}", ex.Message));
        }
    }
}
```
## WebAPI Documentation
Information about the webapi [here](https://primaverabss.github.io/rose-product-erp/)

## Contributing and Feedback
Everyone is free to contribute to the repository.

Any bugs detected in the code samples can be reported in the *Issues* section of this repository.

## License

Unless otherwise specified, the code samples are released under the [MIT license](https://pt.wikipedia.org/wiki/Licen%C3%A7a_MIT).
