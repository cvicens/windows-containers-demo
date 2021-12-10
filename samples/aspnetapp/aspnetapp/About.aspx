<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="aspnetapp.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: TitleNew %>.</h2>
    <h3>Your application description page. <%: Description %> </h3>
    <p>Use this area to provide additional information. <%: Details %></p>
    
</asp:Content>
