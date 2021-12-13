<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="aspnetapp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    

    <% if(ErrorMessage == null) { %>
        <h2><%: TitleNew %></h2>
        <h3><%: Description %> </h3>
        <p><%: Details %></p>
    <% }
    else { %>
        <h2>There was an ERROR ;-(</h2>
        <h3><%: ErrorMessage %> </h3>
    <% } %>
</asp:Content>
