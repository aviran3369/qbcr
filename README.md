1. Install the following nuget packages:
       Microsoft.Owin.Host.SystemWeb
	   Microsoft.Owin.Security.OAuth
	   Microsoft.Owin.Cors
       Microsoft.AspNet.WebApi.Owin
	   Microsoft.AspNet.Identity.Owin 
	   Microsoft.AspNet.Identity.EntityFramework
	   EntityFramework

2. Delete Global.asax Class

3. add Startup.cs to your webApi project, SEE COMMENTS IN THIS FILES !!!!!!

4. add this lines in Register method at App_Start\WebApiConfig.cs:
	  ...
	  var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
      jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
	  ...

5. Copy AccountController.cs from Controllers folder

---------------------------
---------- USING ----------
---------------------------

#Login:
http://localhost:61245/token

headers:
Content-Type: application/x-www-form-urlencoded

body-
grant_type:password
username:aviran
password:password

response:
{
    "access_token": "1nOiaBnP0SYAOEPCupYdeTUP6mq13mCSPtqYd7faKn8cGBC1O5SzxECkBKnoT99qCI6g4iucDfxczky26I6I7zEg4quoA0FPYSMl4rJSSHZt3D8Ze0bnwEq3gq0AWEX31KDuNAeTIjLpEX84mwUG36fjwtH-MO7iWHLU6jMpQwVInBl1cP3ksVf1tMomsfuB08DVj7GOEFNFhwhfa59H65Bhw8cb_fEOP0zLeDQFSR0",
    "token_type": "bearer",
    "expires_in": 86399
}

After login, every request shoud contains Authorization header:
Authorization:Bearer 1nOiaBnP0SYAOEP..... (the access_token from /token response)

***
Get company id: ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c=>c.Type == "cid").Value
Get user id: ((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.FirstOrDefault(c=>c.Type == "uid").Value
