<%@ Application Language="C#" %>

<script runat="server">

    void AddConnectionString(String webConfigPath,String name, String connectionString)
    {
        Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(webConfigPath);
        if (config.ConnectionStrings.ConnectionStrings[name] == null)
        {
            config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(name,
            connectionString));
            config.Save();
        }
        else
        {
            if (config.ConnectionStrings.ConnectionStrings[name].ConnectionString !=
            connectionString)
            {
                config.ConnectionStrings.ConnectionStrings[name].ConnectionString = connectionString;
                config.Save();
            }
        }  
    }
    
    void Application_Start(object sender, EventArgs e) 
    {

        AddConnectionString("~/Task02","reviews",@"Data Source=.\SQLEXPRESS;AttachDbFilename=" +
      Server.MapPath("/Training") + @"\Task02\reviews.mdf;Integrated Security=True;User Instance=True");

        AddConnectionString("~/Task03", "eshop", @"Data Source=.\SQLEXPRESS;AttachDbFilename=" +
            Server.MapPath("/Training") + @"\Task03\eshop.mdf;Integrated Security=True;User Instance=True");
       
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // Code that runs when an unhandled error occurs

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }
       
</script>
