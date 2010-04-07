<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<DbShared.ClothingType>>" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Clothes
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h2>Clothes</h2>
    <table>
        <tr>
            <th>
            </th>
            <th>
                Name
            </th>
            <th>
                Description
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%= Html.ActionLink("Edit", "EditClothingType", new { id = item.Id })%>
                |
                <%= Html.ActionLink("Details", "ClothingTypeDetails", new { id = item.Id })%>
                |
                <%= Html.ActionLink("Delete", "DeleteClothingType", new { id = item.Id })%>
            </td>
            <td>
                <%= Html.Encode(item.Name) %>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
        </tr>
        <% } %>
    </table>
    <p>
        <%= Html.ActionLink("Create New", "CreateClothingType")%>
    </p>
</body>
</asp:Content>
