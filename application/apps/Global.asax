<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

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
        string sKey = (string)Session["usernamesessionid"];
       




        //HttpContext.Current.Cache.Remove(sKey);


    }

    void Application_PreRequestHandlerExecute(Object sender, EventArgs e)
    {

        if (HttpContext.Current.Session != null)
        {
            if (Session["usernamesessionid"] != null)
            {
                string cacheKey = Session["usernamesessionid"].ToString();
                if ((string)HttpContext.Current.Cache[cacheKey] != Session.SessionID)
                {
                    FormsAuthentication.SignOut();
                    Session.Abandon();
                    Response.Redirect("Default.aspx");
                }

                string sUser = (string)HttpContext.Current.Cache[cacheKey];
            }
        }
    }

</script>
