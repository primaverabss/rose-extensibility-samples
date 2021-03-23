# ROSE csharp console application

This demo application includes several features that allow you to understand how to make API calls to the logistics and financial areas, like sales, purchases and accounting.

* Orders
* Invoices 
* Customer
* Sales Items
* Journal Entry

## How to Run This Demo?

To run this application you need:

* Visual Studio 2017 our 2019.
* Reference to .NET CORE 2.0 our higher.
* Restore the packages.
* Provide your application *Client Id* and *Secret*. Register your app on the App Dashboard.
* Provide your *AccountKey* and *SubscriptionKey*. You can get this from ROSE application URL.

Change the *constants.cs* contend with you information.

```csharp
namespace RoseSample.Constants
{
    internal class RoseConstants
    {
        internal const string clientId = "<-- Insert where your client id -->";

        internal const string clientSecret = "<-- Insert where your client client secret -->";

        internal const string AccountKey = "<-- Insert where the accountKey -->";

        internal const string SubscriptionKey = "<-- Insert where the subscription key -->";

    }
}
```

## WebAPI Documentation
More information about the webapi [here](https://apidoc.rose.primaverabss.com/)
