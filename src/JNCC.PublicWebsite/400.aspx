<%--
    Catch dodgy requests which would cause the 500.aspx page to error
    Redirect to the 500 page
--%>
<%@ Page validateRequest="false" %>
<% Response.StatusCode = 500; %>
<!-- #include file="500.html" -->