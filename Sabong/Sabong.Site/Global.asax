<%@ Application Language="C#" %>
<%@ Import Namespace="System.Web.Optimization" %>
<%@ Import Namespace="System.Web.Routing" %>
<%@ Import Namespace="Sabong.Business" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e)
    {
        FetchAnnouncement.Instance.Start();
    }

</script>
