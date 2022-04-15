<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="FiveHead.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>Login</h2>
    <br />
    Username:
            <br />
    <asp:TextBox ID="tb_Username" runat="server" CssClass="form-control" Width="500px"></asp:TextBox>
    <br />
    Password:
            <br />
    <asp:TextBox ID="tb_Password" runat="server" CssClass="form-control" Width="500px" TextMode="Password"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="btn_Login" runat="server" CssClass="btn btn-default" Text="Login" OnClick="btn_Login_Click" />
    &nbsp; &nbsp; Not registered yet? Click <a href="Register.aspx">here</a>.
</asp:Content>
