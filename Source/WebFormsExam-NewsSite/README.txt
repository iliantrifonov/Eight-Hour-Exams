In case the server does not run ( as it is on local DB and connection strings can vary depending on your version and instalation ), please change it to a standart server connection string and run it like that.
I have added some connection strings for you to choose from in the Web.config.

In case I forget, in the webconfig I have :  
<customErrors defaultRedirect="~/ErrorPage.aspx" mode="On" >
</customErrors>

If I forget to uncomment it please be kind :)