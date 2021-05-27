export class CustomerDetail{
  partyKey : string ='';
  name: string ='';
  electronicMail: string ='';
  telephone: string ='';
  websiteUrl: string ='';
  streetName: string ='';
  buildingNumber: string ='';
  postalZone: string ='';
  cityName: string ='';
  customAttributes: CustomAttributes = new CustomAttributes();
}

export class CustomAttributes
{
  custom_GPSLAT: string ='';
  custom_GPSLON: string ='';
}
